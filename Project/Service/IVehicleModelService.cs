using ProjectService.Model;

namespace ProjectService.Service
{
    public interface IVehicleModelService
    {
        Task<ServiceResponse<List<VehicleModelDTORead>>> GetVehicleModels();

        Task<ServiceResponse<VehicleModel>> CreateVehicleModel(VehicleModelDTOInsert dto);

        Task<ServiceResponse<VehicleModel>> UpdateVehicleModel(VehicleModelDTOInsert dto, int id);

        Task<ServiceResponse<VehicleModel>> DeleteVehicleModel(int id);
    }
}
