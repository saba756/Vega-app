using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega_app.Controllers.Resources;
using Vega_app.Models;

namespace Vega_app.Mapping
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Domain to API Resource
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Feature, KeyValuePairResource>();
            CreateMap<Vehicle, VehicleResource>()
               .ForMember(v=> v.Id , opt => opt.Ignore())
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
                 .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));
            //Api Resource to Domain
            CreateMap<VehicleResource, Vehicle>()
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.Features, opt => opt.Ignore())
                              .AfterMap((vr, v) => {
                                  // Remove unselected features
                                  var removedFeatures = v.Features.Where(f => !vr.Features.Contains(f.FeatureId)).ToList();
                                  foreach (var f in removedFeatures)
                                      v.Features.Remove(f);

                                  // Add new features
                                  var addedFeatures = vr.Features.Where(id => !v.Features.Any(f => f.FeatureId == id)).Select(id => new VehicleFeature { FeatureId = id }).ToList();
                                  foreach (var f in addedFeatures)
                                      v.Features.Add(f);
                              });
        }


    }
    }
