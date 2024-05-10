using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage.Json;
using ProjectService.Model;

namespace ProjectService.Mappers
{
    /// <summary>
    /// Implementacija IMapping interfacea
    /// Implementation of IMapping interface
    /// </summary>
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

        /// <summary>
        /// Enkapsulacija mapper konfiguracije koja VehicleModel
        /// model mapira na VehicleModelDTORead
        /// Encapsulation for mapper configuration which 
        /// maps VehicleModel model to DTO
        /// </summary>
        /// <returns></returns>

        private async Task<Mapper> ReturnModelReadToDTO()
        {
            var mappper = new Mapper(
                new AutoMapper.MapperConfiguration(c =>
                c.CreateMap<VehicleModel, VehicleMakeDTORead>()));

            return mappper;
        }
        /// <summary>
        /// Enkapsulacija mapper konfiguracije koja VehicleMake
        /// model mapira na VehicleMakeDTORead
        /// Encapsulation for mapper configuration which 
        /// maps VehicleMake model to DTO
        /// </summary>
        /// <returns></returns>

        private async Task<Mapper> ReturnReadToDTO()
        {   
           var mapper = new Mapper(
                new AutoMapper.MapperConfiguration(c =>
                c.CreateMap<VehicleMake, VehicleMakeDTORead>()));
            
            return mapper;
        }
        /// <summary>
        /// Enkapsulacija mapper konfigiracije kojom se ulazni
        /// VehicleMakeDTOInsert mapira u model
        /// Encapsulation of mapper config that takes the 
        /// input DTO and maps it to model
        /// </summary>
        /// <returns></returns>
        private async Task<Mapper> ReturnInsertFromDTO()
        {
            var mapper = new Mapper(
               new AutoMapper.MapperConfiguration(c =>
               c.CreateMap<VehicleMakeDTOInsert, VehicleMake>()));

            return mapper; 
        }

        
    }
}
