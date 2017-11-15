using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using vega.Core;
using vega.Resources;
using Vega.Core.Entities;
using Vega.Core.Interfaces;

namespace vega.Controllers
{
    [Route("/api/vehicle/{vehicleId}/photos")]
    public class PhotoController : Controller
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly IPhotoRepository photoRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly PhotoSettings photoSettings;

        private IHostingEnvironment host { get; }

        public PhotoController(IHostingEnvironment host,
        IVehicleRepository vehicleRepository,
        IPhotoRepository photoRepository,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IOptionsSnapshot<PhotoSettings> options)
        {
            this.host = host;
            this.vehicleRepository = vehicleRepository;
            this.photoRepository = photoRepository;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.photoSettings = options.Value;
        }

        [HttpGet]
        public async Task<IActionResult> GetPhotos(int vehicleId)
        {
            var photos = await this.photoRepository.GetPhotos(vehicleId);
            return Ok(this.mapper.Map<IEnumerable<PhotoResource>>(photos));
        }


        [HttpPost]
        public async Task<IActionResult> Upload(int vehicleId, IFormFile file)
        {
            var vehicle = await this.vehicleRepository.GetVehicle(vehicleId, includeRelated: false);
            if (vehicle == null)
                return NotFound();

            if (file == null) return BadRequest("Null file");
            if (file.Length == 0) return BadRequest("Empty file");
            if (file.Length > this.photoSettings.MaxBytes) return BadRequest("Maximum file size is 10mb.");
            if (!this.photoSettings.IsSupported(file.FileName)) return BadRequest("Invalid file type.");

            var uploadFolderPath = Path.Combine(this.host.WebRootPath + "/uploads");
            if (!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);

            var filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadFolderPath, filename);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new Photo() { FileName = filename };
            vehicle.Photos.Add(photo);
            await this.unitOfWork.CompleteAsync();

            return Ok(this.mapper.Map<PhotoResource>(photo));

        }
    }
}