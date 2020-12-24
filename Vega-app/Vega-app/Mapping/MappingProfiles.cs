using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega_app.Controllers.Resources;
using Vega_app.Models;

namespace Vega_app.Mapping
{
    public class MappingProfiles :Profile
    {
        public MappingProfiles()
        {
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Feature, KeyValuePairResource>();
        }
    }
}
