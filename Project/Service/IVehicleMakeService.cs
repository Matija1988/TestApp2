using ProjectService.Model;

namespace ProjectService.Service
{
    /// <summary>
    /// Interface (sucelje) za odvajanje ovisnosti funkcija 
    /// vise i nize razine koje koriste Vehicle make mode i DTO
    /// Interface to decuple higher and lower level functions 
    /// that implement VehicleMake model and DTO
    /// </summary>
    public interface IVehicleMakeService
    {
        Task<ServiceResponse<List<VehicleMakeDTORead>>> GetVehicleMakers();

        Task<ServiceResponse<VehicleMake>> CreateVehicleMake(VehicleMakeDTOInsert dto);

        Task<ServiceResponse<VehicleMake>> UpdateVehicleMake(VehicleMakeDTOInsert dto, int id);

        Task<ServiceResponse<VehicleMake>> DeleteVehicleMake(int id);

       
    }
}
