using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using vega.Resources;
using vega.Core;
using Vega.Core.Entities;
using System.Collections.Generic;

namespace vega.Controllers
{
    [Route("api/vehicle")]
    public class VehicleController : Controller
    {
        private readonly IMapper mapper;
        private readonly IVehicleRepository vehicleRepository;
        private readonly IUnitOfWork unitOfWork;

        public VehicleController(IMapper mapper, IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.vehicleRepository = vehicleRepository;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll(VehicleQueryResource filter){
            
            var filters = this.mapper.Map<VehicleQuery>(filter);
            var vehicleList = await this.vehicleRepository.GetAll(filters);
            var mappedList = this.mapper.Map<QueryResultResource<VehicleResource>>(vehicleList);

            return Ok(mappedList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetVehicle(int id)
        {
            var vehicle = await this.vehicleRepository.GetVehicle(id);

            if (vehicle == null)
                return NotFound(id);

            var vehicleResource = this.mapper.Map<VehicleResource>(vehicle);
            return Ok(vehicleResource);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            //var model = await this.context.Model.FindAsync(vehicleResource.ModelId);
            //if (model == null)
            //{
            //    ModelState.AddModelError("ModelId", "Invalid ModelId");
            //    return BadRequest(ModelState);
            //}

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            vehicle.LastUpdate = DateTime.Now;

            this.vehicleRepository.Add(vehicle);
            await this.unitOfWork.CompleteAsync();

            vehicle = await this.vehicleRepository.GetVehicle(vehicle.Id);
            var result = this.mapper.Map<VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                BadRequest(ModelState);

            var vehicle = await this.vehicleRepository.GetVehicle(id);

            if (vehicle == null)
                return NotFound(id);

            mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;

            await this.unitOfWork.CompleteAsync();

            vehicle = await this.vehicleRepository.GetVehicle(vehicle.Id);
            var result = this.mapper.Map<VehicleResource>(vehicle);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await this.vehicleRepository.GetVehicle(id, includeRelated: false);

            if (vehicle == null)
                return NotFound(id);

            this.vehicleRepository.Remove(vehicle);
            await this.unitOfWork.CompleteAsync();
            return Ok();
        }

    }
}