using ProjectService.Model;

namespace ProjectService.Service
{
    /// <summary>
    /// Interface (sucelje) za odvajanje ovisnosti funkcija 
    /// vise i nize razine koje koriste Vehicle make mode i DTO
    /// Interface to decuple higher and lower level functions 
    /// that implement VehicleMake model and DTO
    /// T - type, TDO - type data output, TDI - type data input
    /// </summary>
    public interface IVehicleService<T, TDO, TDI>
    {
        Task<ServiceResponse<List<TDO>>> GetAll();

        Task<ServiceResponse<T>> CreateEntity(TDI dto);

        Task<ServiceResponse<T>> UpdateEntity(TDI dto, int id);

        Task<ServiceResponse<T>> DeleteEntity(int id);

       
    }
}
