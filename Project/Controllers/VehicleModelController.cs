using Azure;
using Microsoft.AspNetCore.Mvc;
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
                VehicleModelDTOReadWithoutID> vehicleModelService)
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
            return StatusCode(StatusCodes.Status404NotFound, response.Message);    
        }

        [HttpGet]
        [Route("Paginate/{pageIndex:int}/{pageSize:int}")]

        public async Task<IActionResult> GetPagination(int pageIndex, int pageSize)
        {
            if (pageIndex < 1) 
            { 
                pageIndex = 1; 
            }
            if (pageSize < 1)
            {
                pageSize = 1;
            }


            try
            {
                var response = await _vehicleModelService.GetPagination(pageIndex, pageSize);

                return StatusCode(StatusCodes.Status200OK, response.Source);

            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
            
        }

        [HttpGet]
        [Route("Search/{condition}")]

        public async Task<IActionResult> SearchByNameOrAbrv(string condition)
        {
            if (condition.Length < 2)
            {
                return BadRequest("Minimal character input for valid search is 2");
            }

            var response = await _vehicleModelService.SearchByNameOrAbrv(condition);

            if(response.Success && response.Data is not null)
            {
                return StatusCode(StatusCodes.Status200OK, response.Data);
            }
            return StatusCode(StatusCodes.Status404NotFound, response.Message);
            
        }

    } 
}
