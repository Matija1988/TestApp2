using AutoMapper;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using ProjectService.Data;
using ProjectService.Mappers;
using ProjectService.Model;
using ProjectService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectService.Test.RepositoryTest
{
    public class VehicleMakeTest
    {
        private readonly IMapping _mapping;

        [Fact]

        public async void VehicleMaker_Service_SeearchByNameOrAbrv_ReturnsTaskServiceResponseListVehicleMakeDTORead()
        {
            var name = "Mitsubishi";

            var dbContext = await GetDbContext();

            var mapper = _mapping;

            var vehicleMakerRepo = new VehicleMakeService(dbContext, mapper);

            var result = vehicleMakerRepo.SearchByNameOrAbrv(name);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<ServiceResponse<List<VehicleMakeDTORead>>>));

        }

        [Fact]
        public async void VehicleMaker_Service_GetSingleEntity_ReturnsTaskServiceResponseVehicleMakeDTORead()
        {
            int id = 1;

            var dbContext = await GetDbContext();
            var mapper = _mapping;
            var vehicleMakerRepo = new VehicleMakeService(dbContext, mapper);
            
            var result = vehicleMakerRepo.GetSingleEntity(id);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<ServiceResponse<VehicleMakeDTOReadWithoutID>>));

        }

        private async Task<ApplicationDbContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new ApplicationDbContext(options);

            databaseContext.Database.EnsureCreated();

            if(await databaseContext.VehicleMakers.CountAsync() <= 0 )
            {
                for(int i = 1; i <= 10; i++)
                {
                    databaseContext.VehicleMakers.Add(
                        new Model.VehicleMake()
                        {
                            Id = i,
                            Name = "Mitsuhishi Motor Corporation",
                            Abrv = "Mitsubishi"
                            
                        });

                    await databaseContext.SaveChangesAsync();
                }
            }

            return databaseContext;

        }

    }
}
