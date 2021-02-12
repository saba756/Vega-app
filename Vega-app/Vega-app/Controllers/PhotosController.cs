using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Vega_app.Controllers.Resources;
using Vega_app.Core;
using Vega_app.Core.Models;

namespace Vega_app.Controllers
{
    [Route("/api/vehicles/{vehicleId}/photos")]
    public class PhotosController : Controller
    {
        private readonly IHostingEnvironment host;
        private readonly IVehicleRepository vehicleRepositor;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly PhotoSettings photoSettings;
      
        public PhotosController(IHostingEnvironment host , IVehicleRepository vehicleRepositor, IUnitOfWork unitOfWork, IMapper mapper, IOptionsSnapshot<PhotoSettings> options)
        {
            photoSettings = options.Value;
            this.host = host;
            this.vehicleRepositor = vehicleRepositor;
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Upload(int vehicleid , IFormFile file)
        {
            var vehicle =  await vehicleRepositor.GetVehicle(vehicleid, includeRelated: false);
            if (vehicle == null)
                return NotFound();
            if (file == null) return BadRequest("Null file");
            if (file.Length == 0) return BadRequest("Empty File");
            if (file.Length > photoSettings.MaxBytes) return BadRequest("Max file size exceeded");
            if (!photoSettings.IsSupported(file.FileName)) return BadRequest("Invalid file type");
            var uploadFolderPath=  Path.Combine(host.WebRootPath, "upload");
            if (!Directory.Exists(uploadFolderPath))
                Directory.CreateDirectory(uploadFolderPath);
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath= Path.Combine(uploadFolderPath, fileName);
            using( var stream = new FileStream(filePath , FileMode.Create))
            {
                await file.CopyToAsync(stream);
            };
            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);
            await unitOfWork.CompleteAsync();
            return Ok(mapper.Map<Photo, PhotoResource>(photo));
         
        }
    }
}
