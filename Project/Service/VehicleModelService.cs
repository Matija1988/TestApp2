using ProjectService.Data;
using ProjectService.Mappers;
using ProjectService.Model;

namespace ProjectService.Service
{
    public class VehicleModelService : IVehicleModelService
    {
        private readonly IMapping _mapping;
        private readonly ApplicationDbContext _context;
        public VehicleModelService(ApplicationDbContext context, IMapping mapping) 
        {
            _mapping = mapping;
            _context = context; 
        }

        public Task<ServiceResponse<VehicleModel>> CreateVehicleModel(VehicleModelDTOInsert dto)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<VehicleModel>> DeleteVehicleModel(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<VehicleModelDTORead>>> GetVehicleModels()
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<VehicleModel>> UpdateVehicleModel(VehicleModelDTOInsert dto, int id)
        {
            throw new NotImplementedException();
        }
    }
}
