using AutoMapper;
using System.Linq;
using vega.Core.Entities;
using vega.Resources;
using Vega.Core.Entities;

namespace vega.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain to Model
            CreateMap<Make, MakeResource>();
            CreateMap<Make, KeyValuePairResource>();
            CreateMap<Model, KeyValuePairResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Photo, PhotoResource>();
            CreateMap<VehicleQuery, VehicleQueryResource>();
            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));
            CreateMap<Vehicle, SaveVehicleResource>()
            .ForMember(v => v.Contact, opt => opt.MapFrom(v => new ContactResource() { Email = v.ContactEmail, Name = v.ContactName, Phone = v.ContactPhone }))
            .ForMember(v => v.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));
            CreateMap<Vehicle, VehicleResource>()
            .ForMember(v => v.Make, opt => opt.MapFrom(v => v.Model.Make))
           .ForMember(v => v.Contact, opt => opt.MapFrom(v => new ContactResource() { Email = v.ContactEmail, Name = v.ContactName, Phone = v.ContactPhone }))
           .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => new KeyValuePairResource() { Id = vf.Feature.Id, Name = vf.Feature.Name })));


            //Model to Domain
            CreateMap<VehicleQueryResource, VehicleQuery>();
            CreateMap<SaveVehicleResource, Vehicle>()
            .ForMember(c => c.Id, opt => opt.Ignore())
            .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
            .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
            .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
            .ForMember(v => v.Features, opt => opt.Ignore())
            .AfterMap((vehicleResource, vehicle) =>
            {
                //var removedFeatures = new List<VehicleFeature>();
                //vehicle.Features.ToList().ForEach(item =>
                //{
                //    if (!vehicleResource.Features.Contains(item.FeatureId))
                //        removedFeatures.Add(item);
                //});
                //removedFeatures.ForEach(item => vehicle.Features.Remove(item));

                vehicle.Features.Where(f => !vehicleResource.Features.Contains(f.FeatureId)).ToList()
                .ForEach(removedFeature => vehicle.Features.Remove(removedFeature));

                vehicleResource.Features
                .Where(item => !vehicle.Features.Any(f => f.FeatureId == item))
                .Select(newFeature => new VehicleFeature() { FeatureId = newFeature }).ToList()
                .ForEach(newFeature =>
                {
                    vehicle.Features.Add(newFeature);
                });

            });

        }
    }

}
