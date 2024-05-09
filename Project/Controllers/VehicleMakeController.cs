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
    }
}
