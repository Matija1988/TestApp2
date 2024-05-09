using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ninject.Infrastructure.Language;
using ProjectService.Data;
using ProjectService.Mappers;
using ProjectService.Model;
using System.Runtime.CompilerServices;

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

        public async Task<ServiceResponse<VehicleMake>> CreateVehicleMake(VehicleMakeDTOInsert dto)
        {
            var response = new ServiceResponse<VehicleMake>();
            try
            {
                _context.VehicleMakers.Add(await MapDTOToModel(dto, new VehicleMake()));
                _context.SaveChanges();

                response.Message = "Entity added successfuly";

                return response;
 
            } 
            catch (Exception ex)
            {
                response.Message = "Task failed!!!";
                throw new Exception(ex.Message);
                
            }

        }

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
                _context.SaveChanges();
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

        

        

        public async Task<ServiceResponse<List<VehicleMakeDTORead>>> GetVehicleMakers()
        {
           var list =  _context.VehicleMakers.ToList() 
                ?? throw new Exception("No data in database!!!");

            ServiceResponse<List<VehicleMakeDTORead>> response = new ServiceResponse<List<VehicleMakeDTORead>>();

            response.Data = await ReturnMappedList(list);

           return response;
        
        }

        private async Task<VehicleMake> MapDTOToModel(VehicleMakeDTOInsert dto, VehicleMake entity)
        {
            entity.Name = dto.Name;
            entity.Abrv = dto.Abrv;

            return entity;
        }


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
