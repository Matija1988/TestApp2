using AutoMapper;
using ProjectService.Model;

namespace ProjectService.Service
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly IMapper _mapper;
        public VehicleMakeService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ServiceResponse<VehicleMake>> CreateVehicleMake(VehicleMake vehicleMake)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<VehicleMakeDTORead>>> GetVehicleMakesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
