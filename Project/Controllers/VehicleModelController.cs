﻿using Microsoft.AspNetCore.Mvc;
using ProjectService.Model;
using ProjectService.Service;

namespace ProjectService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleModelController : ControllerBase, IController<VehicleModelDTOInsert>
    {
        private readonly IVehicleService
            <VehicleModel,
            VehicleModelDTORead,
            VehicleModelDTOInsert,
            VehicleModelDTOReadWithoutID> _vehicleModelService;

        public VehicleModelController(
            IVehicleService
            <VehicleModel,
                VehicleModelDTORead,
                VehicleModelDTOInsert,
                VehicleModelDTOReadWithoutID>
            vehicleModelService)
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
        public async Task<IActionResult> CreateEntity(VehicleModelDTOInsert dto)
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

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateEntity(VehicleModelDTOInsert dto, int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _vehicleModelService.UpdateEntity(dto, id);

            if(response.Success)
            {
                return Ok(StatusCode(StatusCodes.Status200OK, response.Message));
            }

            return BadRequest(response.Message);
        }

        [HttpDelete]
        [Route("DeleteVehicleModel/{id:int}")]

        public async Task<IActionResult> DeleteEntity(int id)
        {
            var response =  await _vehicleModelService.DeleteEntity(id);
            
            if(response.Success)
            {
                return StatusCode(StatusCodes.Status200OK, response.Message);
            }
            return BadRequest(response.Message);
        }

        [HttpGet]
        [Route("FindModel/{id:int}")]

        public async Task<IActionResult> GetSingle(int id)
        {
            var response = await _vehicleModelService.GetSingleEntity(id);

            if(response.Success)
            {
                return StatusCode(StatusCodes.Status200OK, response.Data);
            }
            return BadRequest(response.Message);    
        }

        [HttpGet]
        [Route("Paginate/{page:int}")]

        public async Task<IActionResult> GetPagination(int page, string condition)
        {
            var response = await _vehicleModelService.GetPagination(page, condition);

            if (response.Success)
            {
                return StatusCode(StatusCodes.Status200OK, response.Data);
            }
            return BadRequest(response.Message);
        }
    } 
}
