using Microsoft.AspNetCore.Mvc;
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
    }
}
