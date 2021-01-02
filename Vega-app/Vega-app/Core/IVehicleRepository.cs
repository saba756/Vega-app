using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vega_app.Core.Models;
using Vega_app.Models;

namespace Vega_app.Core
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id, bool includeRelated = true);
        void Add(Vehicle vehicle);
        void Remove(Vehicle vehicle);
        Task<QueryResult<Vehicle>> GetVehicle(VehicleQuery vehicleQuery);
    }
}
