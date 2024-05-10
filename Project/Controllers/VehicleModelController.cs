﻿using Microsoft.AspNetCore.Mvc;
using ProjectService.Model;
using ProjectService.Service;

namespace ProjectService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleModelController : ControllerBase
    {
        private readonly IVehicleService<VehicleModel, VehicleModelDTORead, VehicleModelDTOInsert> _vehicleModelService;

        public VehicleModelController(
            IVehicleService<VehicleModel, VehicleModelDTORead, VehicleModelDTOInsert> vehicleModelService)
        {
            _vehicleModelService = vehicleModelService;
        }

        [HttpGet] 
        public async Task<IActionResult> GetAll()
        {
            var response = await _vehicleModelService.GetAll();

            if(response.Success)
            {
                return Ok(response.Data);
            }

            return NotFound(response.Message);
        }

        [HttpPost] 
        public async Task<IActionResult> Create(VehicleModelDTOInsert dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _vehicleModelService.CreateEntity(dto);

            if(response.Success)
            {
                return Ok(StatusCode(StatusCodes.Status201Created, response.Message));
            }
            return BadRequest(response.Message);    

        }
    }
}
