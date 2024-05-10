using Microsoft.AspNetCore.Http.HttpResults;
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

        public async Task<ServiceResponse<VehicleModel>> CreateEntity(VehicleModelDTOInsert dto)
        {
            var response = new ServiceResponse<VehicleModel>();

            try
            {
                await _context.VehicleModels.AddAsync(await MapDTOToModel(dto, new VehicleModel()));
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Message = "Entity added successfuly";

                return response;
            } 
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Task failed!!!";
                throw new Exception(ex.Message);
            }
        }

        private async Task<VehicleModel> MapDTOToModel(VehicleModelDTOInsert dto, VehicleModel vehicleModel)
        {
            
            var vehicleMakers = await _context.VehicleMakers.FindAsync(dto.MakeId) 
                ?? throw new Exception("Entity with id " + dto.MakeId + " not found in database!!!");

            vehicleModel.Name = dto.Name;
            vehicleModel.Abrv = dto.Abrv;
            vehicleModel.MakeId = dto.MakeId;

            return vehicleModel;

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
