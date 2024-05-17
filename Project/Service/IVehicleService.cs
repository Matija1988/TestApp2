using ProjectService.Model;

namespace ProjectService.Service
{
    /// <summary>
    /// Interface (sucelje) za servisne klase zaduzene 
    /// za pruzanje informacija rutama u kontrolerima
    /// Interfaces for service classes intended to provide 
    /// data for controllers 
    /// T - type, TDO - type data output, TDI - type data input, 
    /// TDO2 - type data output without ID 
    /// (to be used to fetch data for entity the id is known)
    /// </summary>
    public interface IVehicleService<T, TDO, TDI, TDO2>
    {
        Task<ServiceResponse<List<TDO>>> GetAll();

        Task<ServiceResponse<TDO2>> GetSingleEntity(int id);

        Task<ServiceResponse<T>> CreateEntity(TDI dto);

        Task<ServiceResponse<T>> UpdateEntity(TDI dto, int id);

        Task<ServiceResponse<T>> DeleteEntity(int id);

        Task<ServiceResponse<List<TDO>>> GetPagination(int page, string condition ="");





    }
}
