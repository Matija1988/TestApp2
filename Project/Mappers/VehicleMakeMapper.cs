using AutoMapper;
using ProjectService.Model;

namespace ProjectService.Mappers
{
    public class VehicleMakeMapper : Profile
    {
        public VehicleMakeMapper()
        {
            CreateMap<VehicleMake, VehicleMakeDTORead>();
        }
        
    }
}
