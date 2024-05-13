using AutoMapper;

namespace ProjectService.Mappers
{
    /// <summary>
    /// Interface za implementaciju AutoMappera
    /// Interface for Automapper implementation
    /// </summary>
    public interface IMapping
    {
       Task<Mapper> VehicleMakerMapReadToDTO();

        Task<Mapper> VehicleMakerUpdateFromDTO();

        Task<Mapper> VehicleModelMapReadToDTO();

        Task<Mapper> VehicleModelInsertFromDTO();

    }
}
