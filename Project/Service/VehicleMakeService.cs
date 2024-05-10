using AutoMapper;

using Ninject.Infrastructure.Language;
using ProjectService.Data;
using ProjectService.Mappers;
using ProjectService.Model;

namespace ProjectService.Service
{
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly IMapping _mapping;
        private readonly ApplicationDbContext _context;
        
        public VehicleMakeService(ApplicationDbContext context,  IMapping mapping)
        {
            _context = context;
            _mapping = mapping;
        }

        /// <summary>
        /// Stvara novi unos u bazu podataka putem ulaznog DTO
        /// Creates a new db entry wila input DTO
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>

        public async Task<ServiceResponse<VehicleMake>> CreateVehicleMake(VehicleMakeDTOInsert dto)
        {
            var response = new ServiceResponse<VehicleMake>();
            try
            {
                await _context.VehicleMakers.AddAsync(await MapDTOToModel(dto, new VehicleMake()));
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

        /// <summary>
        /// Uzima ulazni DTO i uneseni int koji predstavlja kljuc unosa kojeg zelimo promijeniti
        /// Takes input DTO and int id that represent the key of the entry we wish to update
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="id"></param>
        /// <returns></returns>

        public async Task<ServiceResponse<VehicleMake>> UpdateVehicleMake(VehicleMakeDTOInsert dto, int id)
        {
            var response = new ServiceResponse<VehicleMake>();

            var MakerFromDb = await _context.VehicleMakers.FindAsync(id);

            if(MakerFromDb.Equals(null)) 
            {
                response.Success = false;
                response.Message = "Entity with id " + id + " not found in database";
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

            } catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Update failed" + ex.Message;

                return response;
            }

        }

        public async Task<ServiceResponse<VehicleMake>> DeleteVehicleMake(int id)
        {

            var response = new ServiceResponse<VehicleMake>();

            try
            {

                var EntityFromDB = await _context.VehicleMakers.FindAsync(id);

                _context.Remove(EntityFromDB);
                await _context.SaveChangesAsync();

                response.Success = true;
                response.Message = "Entity deleted!!!";

                return response;

            } catch (Exception ex) 
            {
                response.Success = false;
                response.Message = "Task failed!!! " + ex.Message;
                return response;
            }
        } 
 

        /// <summary>
        /// Salje listu DTO VehicleMake u HTTP GET rutu kontrolera
        /// Sends DTO list to HTTP GET route
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<ServiceResponse<List<VehicleMakeDTORead>>> GetVehicleMakers()
        {
           var list =  _context.VehicleMakers.ToList() 
                ?? throw new Exception("No data in database!!!");

            ServiceResponse<List<VehicleMakeDTORead>> response = new ServiceResponse<List<VehicleMakeDTORead>>();

            response.Data = await ReturnMappedList(list);

           return response;
        
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
        /// Uzima listu modela, vraca listu DTO
        /// Takes model list, returns DTO list
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        private async Task<List<VehicleMakeDTORead>> ReturnMappedList(List<VehicleMake> list)
        {
            var entityList = new List<VehicleMakeDTORead>();
            var _mapper = _mapping.MapperInitReadToDTO();

            foreach (var item in list)
            {
               entityList.Add( _mapper.Map<VehicleMakeDTORead>(item));
            }

            return entityList;  

        }

    }
}
