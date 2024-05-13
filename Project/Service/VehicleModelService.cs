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
            return await ReturnCreatedEntity(dto);
        }

        public async Task<ServiceResponse<VehicleModel>> UpdateEntity(VehicleModelDTOInsert dto, int id)
        {
            return await ReturnUpdatedEntity(dto, id);
        }

       

        public async Task<ServiceResponse<VehicleModel>> DeleteEntity(int id)
        {
            var response = new ServiceResponse<VehicleModel>();

            var EntityFromDB = await _context.VehicleModels.FindAsync(id);

            if (EntityFromDB is null || id <= 0)
            {
                response.Success = false;
                response.Message = "No vehicle model with id " + id + " found in database!!! " +
                    "Id cannot be equal to or less than 0!!!";
                return response;
            }

            _context.Remove(EntityFromDB);
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Entity deleted!!!";

            return response;
        }

        public async Task<ServiceResponse<List<VehicleModelDTORead>>> GetAll()
        {
            var response = new ServiceResponse<List<VehicleModelDTORead>>();

            var list = await _context.VehicleModels.Include(vm => vm.Make).ToListAsync();

            if (list is null)

            {
                response.Success = false;
                response.Message = "No data in database!!!";
                return response;
            }

            response.Data = await ReturnMappedList(list);

            return response;
        }

        public async Task<ServiceResponse<VehicleModelDTORead>> GetSingleEntity(int id)
        {
            var response = new ServiceResponse<VehicleModelDTORead>();

            var model = await _context.VehicleModels.Include(vm => vm.Make).FirstOrDefaultAsync(v => v.Id == id);

            if (model is null || id <= 0)
            {
                response.Success = false;
                response.Message = "Vehicle model with id " + id + " not found in database!!! " +
                    "Id cannot be equal to or less than 0!!!";
                return response;
            }

            response.Success = true;
            response.Data = await ReturnSingleDTORead(model);

            return response;
        }

        public Task<ServiceResponse<List<VehicleModelDTORead>>> GetPagination(int page, string condition = "")
        {
            return null;
        }

        private async Task<VehicleModelDTORead> ReturnSingleDTORead(VehicleModel entity)
        {
            var _mapper = await _mapping.VehicleModelMapReadToDTO();

            return _mapper.Map<VehicleModelDTORead>(entity);
        }

        private async Task<VehicleModel> MapDTOToModel(VehicleModelDTOInsert dto, VehicleModel vehicleModel)
        {

            var vehicleMakers = await _context.VehicleMakers.FindAsync(dto.MakeId)
                ?? throw new Exception("Vehicle maker with id " + dto.MakeId + " not found in database!!!");

            vehicleModel.Name = dto.Name;
            vehicleModel.Abrv = dto.Abrv;
            vehicleModel.MakeId = dto.MakeId;

            return vehicleModel;

        }
        private async Task<ServiceResponse<VehicleModel>> ReturnUpdatedEntity(VehicleModelDTOInsert dto, int id)
        {
            var response = new ServiceResponse<VehicleModel>();
            var entityFromDB = await _context.VehicleModels.FindAsync(id);
            var makerFromDB = await _context.VehicleMakers.FindAsync(dto.MakeId);

            if (entityFromDB is null || makerFromDB is null)
            {
                response.Success = false;
                response.Message = "No entity with id " + id + " found in database!!!";
                return response;
            }

            entityFromDB.Name = dto.Name;
            entityFromDB.Abrv = dto.Abrv;
            entityFromDB.Make = makerFromDB;

            _context.Update(entityFromDB);
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Entity updated";

            return response;
        }
        private async Task<ServiceResponse<VehicleModel>> ReturnCreatedEntity(VehicleModelDTOInsert dto)
        {
            var response = new ServiceResponse<VehicleModel>();

            try
            {
                await _context.VehicleModels.AddAsync(await MapDTOToModel(dto, new VehicleModel()));
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Message = "Vehicle model added successfuly";

                return response;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Task failed!!!";
                throw new Exception(ex.Message);
            }
        }

        private async Task<List<VehicleModelDTORead>> ReturnMappedList(List<VehicleModel> list)
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
