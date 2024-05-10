using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage.Json;
using ProjectService.Model;

namespace ProjectService.Mappers
{
    public class MapperConfiguration : IMapping
    {
        
        
        public async Task<Mapper> VehicleMakerMapReadToDTO()
        {
            var mapper = await ReturnReadToDTO();
            return mapper;

        }

        public async Task<Mapper> VehicleMakerUpdateFromDTO()
        {
            var mapper = await ReturnInsertFromDTO();
            return mapper;  
        }

        public async Task<Mapper> VehicleModelMapReadToDTO()
        {
            var mapper = await ReturnModelReadToDTO();
            return mapper;
        }

        private async Task<Mapper> ReturnModelReadToDTO()
        {
            var mappper = new Mapper(
                new AutoMapper.MapperConfiguration(c =>
                c.CreateMap<VehicleModel, VehicleMakeDTORead>()));

            return mappper;
        }

        private async Task<Mapper> ReturnReadToDTO()
        {   
           var mapper = new Mapper(
                new AutoMapper.MapperConfiguration(c =>
                c.CreateMap<VehicleMake, VehicleMakeDTORead>()));
            
            return mapper;
        }

        private async Task<Mapper> ReturnInsertFromDTO()
        {
            var mapper = new Mapper(
               new AutoMapper.MapperConfiguration(c =>
               c.CreateMap<VehicleMakeDTOInsert, VehicleMake>()));

            return mapper; 
        }

        
    }
}
