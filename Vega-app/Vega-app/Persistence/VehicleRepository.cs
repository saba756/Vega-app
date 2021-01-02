using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Vega_app.Core;
using Vega_app.Core.Models;
using Vega_app.Extensions;
using Vega_app.Models;

namespace Vega_app.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext context;

        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;
        }

        public void Add(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
        }

        public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await context.Vehicles.FindAsync(id);
            return await context.Vehicles.
               Include(v => v.Features).
               ThenInclude(vf => vf.Feature).
               Include(v => v.Model).
               ThenInclude(m => m.Make).
               SingleOrDefaultAsync(f => f.Id == id);
        }

        public async Task<QueryResult<Vehicle>> GetVehicle(VehicleQuery queryObj)
        {
            var result = new QueryResult<Vehicle>();

            var query = context.Vehicles.
                   Include(v => v.Model).
                   ThenInclude(m => m.Make).
                   Include(v => v.Features).
                   ThenInclude(vf => vf.Feature).AsQueryable();
            if (queryObj.MakeId.HasValue)
            {
                query = query.Where(v => v.Model.MakeId == queryObj.MakeId.Value);
            }
            var columnMap = new Dictionary<string, Expression<Func<Vehicle, object>>>
            {
                ["make"] = v=> v.Model.Make.Name,
                ["model"] = v => v.Model.Name,
                ["contactName"] = v => v.ContactName,
                ["id"] = v => v.Id
            };

            query = query.ApplyOrdering(queryObj, columnMap);
            result.TotalItems =  await query.CountAsync();
            query = query.ApplyPaging(queryObj);
            result.Items=  await query.ToListAsync();
            return result;
        }
        //private IQueryable<Vehicle>ApplyOrdering(VehicleQuery queryObj,IQueryable<Vehicle> query, Dictionary<string, Expression<Func<Vehicle, object>>> columnMap)
        //{
        //    if (queryObj.IsSortAscending)
        //        return query.OrderBy(columnMap[queryObj.SortBy]);
        //    else
        //        return query.OrderByDescending(columnMap[queryObj.SortBy]);

        //}

        public void Remove(Vehicle vehicle)
        {
            context.Vehicles.Remove(vehicle);
        }
    }
}
