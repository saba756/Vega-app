using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega_app.Extensions;

namespace Vega_app.Core.Models
{
    public class VehicleQuery : IQueryObject
    {
        public int ? MakeId { get; set; }
        public string SortBy { get; set; }
        public bool IsSortAscending { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}
