using ProjectService.Data;
using ProjectService.Mappers;
using ProjectService.Model;

namespace ProjectService.Service
{
    public class VehicleModelService : IVehicleService<VehicleModel, VehicleMakeDTORead, VehicleMakeDTOInsert>
    {
        private readonly IMapping _mapping;
        private readonly ApplicationDbContext _context;
        public VehicleModelService(ApplicationDbContext context, IMapping mapping) 
        {
            _mapping = mapping;
            _context = context; 
        }

        public Task<ServiceResponse<VehicleModel>> CreateEntity(VehicleMakeDTOInsert dto)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<VehicleModel>> DeleteEntity(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<List<VehicleMakeDTORead>>> GetAll()
        {
            var response = new ServiceResponse<List<VehicleMakeDTORead>>();
            return null;
        }

        public Task<ServiceResponse<VehicleModel>> UpdateEntity(VehicleMakeDTOInsert dto, int id)
        {
            throw new NotImplementedException();
        }
    }
}
