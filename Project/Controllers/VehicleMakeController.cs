using Microsoft.AspNetCore.Mvc;
using ProjectService.Model;
using ProjectService.Service;

namespace ProjectService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleMakeController : ControllerBase
    {
        private readonly IVehicleService<VehicleMake, VehicleMakeDTORead, VehicleMakeDTOInsert> _vehicleMakeService;
        public VehicleMakeController(
            IVehicleService<VehicleMake, VehicleMakeDTORead, VehicleMakeDTOInsert> vehicleMakeService)
        {
            _vehicleMakeService = vehicleMakeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _vehicleMakeService.GetAll();

            if(response.Success)
            {
                return Ok(response.Data);
            }
            return NotFound(response.Message);
        }

        [HttpGet]
        [Route("FindMakerByID/{id:int}")]
        public async Task<IActionResult> GetSingle(int id)
        {
            var response = await _vehicleMakeService.GetSingleEntity(id);

            if(response.Success)
            { 
                return Ok(response.Data); 
            }
            return NotFound(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> CreateEntity(VehicleMakeDTOInsert dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _vehicleMakeService.CreateEntity(dto);

            if(response.Success)
            {
                return Ok(StatusCode(StatusCodes.Status201Created, response.Message));
            }
            return BadRequest(response.Message);
            
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateEntity(VehicleMakeDTOInsert dto, int id)
        {
            if (!ModelState.IsValid || id <= 0) 
            {
                return BadRequest("Please check your input! " + ModelState);
            }
           var response =  await _vehicleMakeService.UpdateEntity(dto, id);
            
            if(response.Success)
            {
                return Ok(StatusCode(StatusCodes.Status200OK, response.Message));
            } 
            return BadRequest(response.Message);
        }

        [HttpDelete]
        [Route("DeleteVehicleMake/{id:int}")]

        public async Task<IActionResult> DeleteEntity(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID cannot be equal to or lower then 0");
            }
           var response =  await _vehicleMakeService.DeleteEntity(id);

            if(response.Success)
            {
                return Ok(StatusCode(StatusCodes.Status200OK));
            }
            return BadRequest(response.Message);

        }
    }
}
