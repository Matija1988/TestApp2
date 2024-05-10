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
          return  Ok(_vehicleMakeService.GetVehicleMakers()); 
        }

        [HttpPost] 
        public async Task<IActionResult> Create(VehicleMakeDTOInsert dto) 
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _vehicleMakeService.CreateVehicleMake(dto);

            return Ok(StatusCode(StatusCodes.Status201Created));
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update(VehicleMakeDTOInsert dto, int id)
        {
           await _vehicleMakeService.UpdateVehicleMake(dto, id);
            
            return Ok(StatusCode(StatusCodes.Status200OK)); 

        }

        [HttpDelete]
        [Route("DeleteVehicleMake/{id:int}")]

        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID cannot be equal to or lower then 0");
            }
            await _vehicleMakeService.DeleteVehicleMake(id);

            return Ok(StatusCode(StatusCodes.Status200OK));
        }
    }
}
