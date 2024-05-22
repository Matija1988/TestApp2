using Microsoft.EntityFrameworkCore;
using Ninject;
using Ninject.Infrastructure.Language;
using ProjectService.Data;
using ProjectService.Mappers;
using ProjectService.Model;

namespace ProjectService.Service
{
    /// <summary>
    /// Implementacija IVehicleMakeService
    /// Implementation of IVehicleMakeService
    /// </summary>
    public class VehicleMakeService
        : IVehicleService<VehicleMake, VehicleMakeDTORead, VehicleMakeDTOInsert, VehicleMakeDTOReadWithoutID>
    {
        private readonly IMapping _mapping;
        private readonly ApplicationDbContext _context;

        public VehicleMakeService(ApplicationDbContext context, IMapping mapping)
        {
            _context = context;
            _mapping = mapping;
        }

        /// <summary>
        /// Stvara novi unos u bazu podataka putem ulaznog DTO
        /// Creates a new db entry via input DTO
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        public async Task<ServiceResponse<VehicleMake>> CreateEntity(VehicleMakeDTOInsert dto)
        {
            return await ReturnCreatedEntity(dto);

        }

        /// <summary>
        /// Uzima ulazni DTO i uneseni int koji predstavlja kljuc unosa kojeg zelimo promijeniti
        /// Takes input DTO and int id that represents a Primary key of the entry we wish to update
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<ServiceResponse<VehicleMake>> UpdateEntity(VehicleMakeDTOInsert dto, int id)
        {
            return await ReturnUpdatedEntity(dto, id);
        }

        /// <summary>
        /// Uzima ulazni int koji predstavlja kljuc objekta u bazi podataka 
        /// pronalazi ga i brise iz baze 
        /// Takes input int that represents a objects primary key 
        /// finds the object in DB and deletes it
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<ServiceResponse<VehicleMake>> DeleteEntity(int id)
        {

            var response = new ServiceResponse<VehicleMake>();

            var EntityFromDB = await _context.VehicleMakers.FindAsync(id);

            if (EntityFromDB is null)
            {
                response.Success = false;
                response.Message = "No entity with id " + id + " found in database!!!";
                return response;
            }

            _context.Remove(EntityFromDB);
            await _context.SaveChangesAsync();

            response.Success = true;
            response.Message = "Entity deleted!!!";

            return response;

        }


        /// <summary>
        /// Salje listu DTO VehicleMake u HTTP GET rutu kontrolera
        /// Sends DTO list to HTTP GET route
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ServiceResponse<List<VehicleMakeDTORead>>> GetAll()
        {
            var response = new ServiceResponse<List<VehicleMakeDTORead>>();

            var list = await _context.VehicleMakers.ToListAsync();

            if (list is null)
            {
                response.Success = false;
                response.Message = "No data in database!!!";
                return response;
            }

            response.Data = await ReturnMappedList(list);

            return response;

        }

        /// <summary>
        /// Prima int koji predstavlja primarni kljuc entiteta i vraca entitet
        /// Takes an int that represents the Primarty key of an entity and returns 
        /// the entity
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<ServiceResponse<VehicleMakeDTOReadWithoutID>> GetSingleEntity(int id)
        {
            var response = new ServiceResponse<VehicleMakeDTOReadWithoutID>();

            var maker = await _context.VehicleMakers.FindAsync(id);

            if (id <= 0 || maker is null)
            {
                response.Success = false;
                response.Message = "Vehicle maker with id " + id + " not found in database!!! " +
                                    "Id cannot be equal to or less than 0!!!";
                return response;
            }

            response.Success = true;
            response.Data = await ReturnSingleDTORead(maker);

            return response;

        }
        

        /// <summary>
        /// Stranicenje u backendu, radi u swaggeru
        /// Pagination in backend, works in Swagger
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PaginatedView<VehicleMakeDTORead>> GetPagination(int pageIndex, int pageSize)
        {
            var _mapper = await _mapping.VehicleMakerMapReadToDTO();
            try
            {

                var data = _context.VehicleMakers.ToListAsync();

                var items = _mapper.Map<List<VehicleMakeDTORead>>(data);

                return await PaginatedView<VehicleMakeDTORead>.PaginateAsync(items, pageIndex, pageSize);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Pretraga po nazivu ili skracenici
        /// Search by Name or Abbreviation
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public async Task<ServiceResponse<List<VehicleMakeDTORead>>> SearchByNameOrAbrv(string condition)
        {

            var items = await _context.VehicleMakers.Where(a => EF.Functions.Like(a.Name.ToLower(), "%" + condition + "%")
                || EF.Functions.Like(a.Abrv.ToLower(), "%" + condition + "%"))
                .OrderBy(a => a.Abrv)
                .ToListAsync();

            var response = new ServiceResponse<List<VehicleMakeDTORead>>();

            if (items is null || items.Count < 1)
            {
                response.Success = false;
                response.Message = "Entity matching name or abbreviation '" + condition + "' not found in database!!!";
                return response;
            }
            else
            {
                response.Data = await ReturnMappedList(items);

                return response;
            }
        }


        /// <summary>
        /// Uzima jedan entitet, mapira ga u DTO i vraca 
        /// Takes a single entity, maps it into a DTO then returns it
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private async Task<VehicleMakeDTOReadWithoutID> ReturnSingleDTORead(VehicleMake entity)
        {
            var _mapper = await _mapping.VehicleMakerDataOfUpdatedEntity();

            return _mapper.Map<VehicleMakeDTOReadWithoutID>(entity);
        }



        /// <summary>
        /// Uzima listu modela, vraca listu DTO
        /// Takes model list, returns DTO list
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private async Task<List<VehicleMakeDTORead>> ReturnMappedList(List<VehicleMake> list)
        {
            var entityList = new List<VehicleMakeDTORead>();
            var _mapper = await _mapping.VehicleMakerMapReadToDTO();

            foreach (var item in list)
            {
                entityList.Add(_mapper.Map<VehicleMakeDTORead>(item));
            }

            return entityList;

        }

        /// <summary>
        /// Uzima DTO i stvara novi unos u bazi podataka
        /// Takes inpt DTO and creates new entry in DB
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        private async Task<ServiceResponse<VehicleMake>> ReturnCreatedEntity(VehicleMakeDTOInsert dto)
        {
            var response = new ServiceResponse<VehicleMake>();

            try
            {
                await _context.VehicleMakers.AddAsync(await MapDTOToModel(dto, new VehicleMake()));
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Message = "Vehicle maker added successfuly";

                return response;

            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Task failed in VehicleMakeService ReturnCreatedEntity!!!" + ex.Message;
                return response;
            }
        }

        /// <summary>
        /// Stavlja podatke iz DTO u model na odgovarajuce atribute
        /// Sets data from DTO in the model at appropriate attributes
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="entity"></param>
        /// <returns></returns>

        private async Task<VehicleMake> MapDTOToModel(VehicleMakeDTOInsert dto, VehicleMake entity)
        {
            entity.Name = dto.Name;
            entity.Abrv = dto.Abrv;

            return entity;
        }

        /// <summary>
        /// Uzima ulazni DTO i int koji predstavlja primarni kljuc entiteta u bazi podataka 
        /// mijenja ih i vraca u javnu metodu
        /// Takes input DTO and int which represents a primary key of an entity in DB 
        /// changes is and returns it to the public method
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="id"></param>
        /// <returns></returns>

        private async Task<ServiceResponse<VehicleMake>> ReturnUpdatedEntity(VehicleMakeDTOInsert dto, int id)
        {
            var response = new ServiceResponse<VehicleMake>();

            var MakerFromDb = await _context.VehicleMakers.FindAsync(id);

            if (MakerFromDb is null)
            {
                response.Success = false;
                response.Message = "Vehicle maker with id " + id + " not found in database!!!";
                return response;
            }
            try
            {
                MakerFromDb.Name = dto.Name;
                MakerFromDb.Abrv = dto.Abrv;

                _context.Update(MakerFromDb);
                await _context.SaveChangesAsync();
                response.Success = true;
                response.Message = "Entity updated";

                return response;

            }
            catch
            {
                response.Success = false;
                response.Message = "Task failed in VehicleMakeService ReturnUpdatedEntity!!!";

                return response;
            }
        }


    }
}
