using Microsoft.AspNetCore.Mvc;
using ProjectService.Model;
using ProjectService.Service;

namespace ProjectService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleMakeController : ControllerBase
    {
        private readonly IVehicleMakeService _vehicleMakeService;
        public VehicleMakeController(IVehicleMakeService vehicleMakeService)
        {
            _vehicleMakeService = vehicleMakeService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _vehicleMakeService.GetVehicleMakers();

            if(response.Success)
            {
                return Ok(response.Data);
            }
            return NotFound(response.Message);
        }

        [HttpPost]
        public async Task<IActionResult> Create(VehicleMakeDTOInsert dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _vehicleMakeService.CreateVehicleMake(dto);

            if(response.Success)
            {
                return Ok(StatusCode(StatusCodes.Status201Created, response.Message));
            }
            return BadRequest(response.Message);
            
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(VehicleMakeDTOInsert dto, int id)
        {
            if (!ModelState.IsValid || id <= 0) 
            {
                return BadRequest("Please check your input! " + ModelState);
            }
           var response =  await _vehicleMakeService.UpdateVehicleMake(dto, id);
            
            if(response.Success)
            {
                return Ok(StatusCode(StatusCodes.Status200OK, response.Message));
            } 
            return BadRequest(response.Message);
        }

        [HttpDelete]
        [Route("DeleteVehicleMake/{id:int}")]

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID cannot be equal to or lower then 0");
            }
           var response =  await _vehicleMakeService.DeleteVehicleMake(id);

            if(response.Success)
            {
                return Ok(StatusCode(StatusCodes.Status200OK));
            }
            return BadRequest(response.Message);

        }
    }
}
