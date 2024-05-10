using Microsoft.EntityFrameworkCore;
using ProjectService.Data;
using ProjectService.Mappers;
using ProjectService.Model;

namespace ProjectService.Service
{
    public class VehicleModelService : IVehicleService<VehicleModel, VehicleModelDTORead, VehicleModelDTOInsert>
    {
        private readonly IMapping _mapping;
        private readonly ApplicationDbContext _context;
        public VehicleModelService(ApplicationDbContext context, IMapping mapping) 
        {
            _mapping = mapping;
            _context = context; 
        }

        public Task<ServiceResponse<VehicleModel>> CreateEntity(VehicleModelDTOInsert dto)
        {
            throw new NotImplementedException();
        }

        public Task<ServiceResponse<VehicleModel>> DeleteEntity(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<VehicleModelDTORead>>> GetAll()
        {
            var response = new ServiceResponse<List<VehicleModelDTORead>>();

            var list = await _context.VehicleModels.Include(vm => vm.Make).ToListAsync();

            if(list is null)

            {
                response.Success = false;
                response.Message = "No data in database!!!";
                return response;
            }

            response.Data = await ReturnMappedList(list);
            
            return response;
        }

        
        public Task<ServiceResponse<VehicleModel>> UpdateEntity(VehicleModelDTOInsert dto, int id)
        {
            throw new NotImplementedException();
        }

        private async Task<List<VehicleModelDTORead>?> ReturnMappedList(List<VehicleModel> list)
        {
            var entityList = new List<VehicleModelDTORead>();
            var _mapper = await _mapping.VehicleModelMapReadToDTO();

            foreach (var entity in list)
            {
                entityList.Add(_mapper.Map<VehicleModelDTORead>(entity));
            }

            return entityList;
        }

    }
}
