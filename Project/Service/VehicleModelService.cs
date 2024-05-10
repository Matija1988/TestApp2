using ProjectService.Data;
using ProjectService.Mappers;

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
    }
}
