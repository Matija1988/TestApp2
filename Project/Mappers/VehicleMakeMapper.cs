using AutoMapper;
using Microsoft.EntityFrameworkCore.Storage.Json;
using ProjectService.Model;

namespace ProjectService.Mappers
{
    public class VehicleMakeMapper : IMapping
    {
        
        public Mapper MapperInitReadToDTO()
        {
            return new Mapper(
                new MapperConfiguration(c =>
                c.CreateMap<VehicleMake, VehicleMakeDTORead>()));
        }

        public Mapper MapperInsertUpdateFromDTO()
        {
           return new Mapper(
               new MapperConfiguration(c =>
               c.CreateMap<VehicleMakeDTOInsert, VehicleMake>()));
        }
    }
}
