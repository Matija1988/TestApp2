using AutoMapper;

namespace ProjectService.Mappers
{
    public interface IMapping
    {
        Mapper MapperInitReadToDTO();

        Mapper MapperInsertUpdateFromDTO();


    }
}
