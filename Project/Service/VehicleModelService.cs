using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ProjectService.Data;
using ProjectService.Mappers;
using ProjectService.Model;

namespace ProjectService.Service
{
    public class VehicleModelService 
        : IVehicleService<VehicleModel, VehicleModelDTORead, VehicleModelDTOInsert, VehicleModelDTOReadWithoutID>
    {
        private readonly IMapping _mapping;
        private readonly ApplicationDbContext _context;
        public VehicleModelService(ApplicationDbContext context, IMapping mapping) 
        {
            _mapping = mapping;
            _context = context; 
        }


        /// <summary>
        /// Stvara novi unos u bazi podataka putem ulaznog DTO
        /// Creates a new DB entry via input DTO
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>

        public async Task<ServiceResponse<VehicleModel>> CreateEntity(VehicleModelDTOInsert dto)
        {
            return await ReturnCreatedEntity(dto);
        }

        /// <summary>
        /// Prima int koji predstavlja primarni kljuc entiteta u bazi podataka 
        /// pronalazi taj entitet i mijenja njegove podatke sukladno ulaznom DTO
        /// Takes an int that represents a primary key of an entity in DB
        /// finds the entity and changes its data to match the input DTO
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<ServiceResponse<VehicleModel>> UpdateEntity(VehicleModelDTOInsert dto, int id)
        {
            return await ReturnUpdatedEntity(dto, id);
        }

       
        /// <summary>
        /// Prima int koji predstavlja primarni kljuc entiteta u bazi podataka
        /// pronalazi da i brise 
        /// Takes an int that represent a primary key of an entity in DB
        /// finds it and deletes it
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

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


        /// <summary>
        /// Mapira sve entitete koji se nalaze u bazi podataka u DTO i 
        /// vraca ih. Ako je baza podataka prazna vraca odgovarajucu poruku.
        /// Maps entities in DB into DTO and returns them. 
        /// If the DB is empty returns appropriate message
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponse<List<VehicleModelDTORead>>> GetAll()
        {
            var response = new ServiceResponse<List<VehicleModelDTORead>>();

            var list = await _context.VehicleModels.Include(vm => vm.Make).ToListAsync();

            if (list is null)

            {
                response.Success = false;
                response.Message = "No data in database!!! Check if there are any entries in DB or the validity " +
                    " of your connection strings!!!";
                return response;
            }

            response.Data = await ReturnMappedList(list);

            return response;
        }

        /// <summary>
        /// Prima int koji predstavlja primarni kljuc entiteta u bazi podataka
        /// pronalazi ga i vraca DTO
        /// Finds and int that represents a primary key of an entity in DB
        /// finds it and return DTO
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<ServiceResponse<VehicleModelDTOReadWithoutID>> GetSingleEntity(int id)
        {
            var response = new ServiceResponse<VehicleModelDTOReadWithoutID>();

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

        /// <summary>
        /// Pretraga po nazivu ili skracenici
        /// Search by name or abbreviation
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>

        public async Task <ServiceResponse<List<VehicleModelDTORead>>> SearchByNameOrAbrv(string condition)
        {
            
            var items = await _context.VehicleModels.Include(a=> a.Make)
                .Where(a => EF.Functions.Like(a.Name.ToLower(), "%" + condition + "%")
                || EF.Functions.Like(a.Abrv.ToLower(), "%" + condition + "%")
                || EF.Functions.Like(a.Make.Abrv.ToLower(), "%" + condition + "%"))
                .OrderBy(a => a.Make.Abrv)
                .ToListAsync();

            
            var response = new ServiceResponse<List<VehicleModelDTORead>>();

            if (items is null || items.Count < 1)
            {
                response.Success = false;
                response.Message = "Entity with name or abbreviation '" + condition + "' not found in database!!!";
                return response;
            }

            response.Data = await ReturnMappedList(items);

            return response;

        }

        /// <summary>
        /// Stranicenje u backendu, int pageIndex predstavlja broj stranice
        /// int pageSize broj prikazanih unosa po stranici
        /// Pagination in backend, int pageIndex represents a page number,
        /// int pageSize represent nubmer of entries per page
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>

        public async Task <PaginatedView<VehicleModelDTORead>> GetPagination(int pageIndex, int pageSize)
        {
 
            var response = await Pagination(pageIndex, pageSize);

            return response;

        }


        private async Task <PaginatedView<VehicleModelDTORead>> Pagination(int pageIndex, int pageSize)
        {
            var _mapper = await _mapping.VehicleModelMapReadToDTO();
            try
            {

                var data = _context.VehicleModels.Include(vm => vm.Make).ToList();

                var items = _mapper.Map<List<VehicleModelDTORead>>(data);

                return await PaginatedView<VehicleModelDTORead>.PaginateAsync(items, pageIndex, pageSize);
            }
            catch (Exception)
            {

                throw;
            }
        }

        
        private async Task<VehicleModelDTOReadWithoutID> ReturnSingleDTORead(VehicleModel entity)
        {
            var _mapper = await _mapping.VehicleModelDataOfUpdatedEntity();

            return _mapper.Map<VehicleModelDTOReadWithoutID>(entity);
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
