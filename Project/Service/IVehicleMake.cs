using ProjectService.Model;

namespace ProjectService.Service
{
    interface IVehicleMakeService
    {
        Task<ServiceResponse<List<VehicleMakeDTORead>>> GetVehicleMakesAsync();

        Task<ServiceResponse<VehicleMake>> CreateVehicleMake(VehicleMake vehicleMake);

       
    }
}
