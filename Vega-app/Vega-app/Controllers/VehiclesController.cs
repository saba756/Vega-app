using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vega_app.Controllers.Resources;
using Vega_app.Core;
using Vega_app.Core.Models;
using Vega_app.Models;
using Vega_app.Persistence;

namespace Vega_app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : Controller
    {
        private readonly IMapper mapper;
        private readonly IVehicleRepository vehicleRepository;
        private readonly IUnitOfWork unitOfWork;

        public VehiclesController(IMapper mapper,IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork)
        {
            this.mapper = mapper;
            this.vehicleRepository = vehicleRepository;
            this.unitOfWork = unitOfWork;
        }
        [HttpPost("vehicles")]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleResource vehicleResource)
        {
           
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);

            vehicle.LastUpdate = DateTime.Now;
            vehicleRepository.Add(vehicle);
            await unitOfWork.Complete();
            vehicle = await vehicleRepository.GetVehicle(vehicle.Id);
            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var vehicle = await vehicleRepository.GetVehicle(id);
            if (vehicle == null)
                return NotFound();
            mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource, vehicle);
            vehicle.LastUpdate = DateTime.Now;
            await unitOfWork.Complete();
            vehicle = await vehicleRepository.GetVehicle(vehicle.Id);
            var result = mapper.Map<Vehicle, VehicleResource>(vehicle);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await vehicleRepository.GetVehicle(id , false);
            if (vehicle == null)
                return NotFound();
            vehicleRepository.Remove(vehicle);
            await unitOfWork.Complete();
            return Ok(id);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle= await vehicleRepository.GetVehicle(id);
            if (vehicle == null)
                return NotFound();
            var vehicleResource = mapper.Map<Vehicle,VehicleResource>(vehicle);
            return Ok(vehicleResource);
        }
        [HttpGet]
        public async Task<QueryResultResources<VehicleResource>> GetVehicle([FromQuery] VehicleQueryResource filterResource)
        {
            var filter = mapper.Map<VehicleQueryResource, VehicleQuery>(filterResource);
            var queryResult = await vehicleRepository.GetVehicle(filter);
            
            return mapper.Map<QueryResult<Vehicle>,QueryResultResources<VehicleResource>>(queryResult);
          
        }
    }
}