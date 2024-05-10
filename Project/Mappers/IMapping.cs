using AutoMapper;

namespace ProjectService.Mappers
{
    public interface IMapping
    {
       Task<Mapper> VehicleMakerMapReadToDTO();

        Task<Mapper> VehicleMakerUpdateFromDTO();

        Task<Mapper> VehicleModelMapReadToDTO();


    }
}
