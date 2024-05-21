using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using ProjectMVC.Controllers;
using ProjectService.Controllers;
using ProjectService.Mappers;
using ProjectService.Model;
using ProjectService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectMVC.Test.ControllerTest
{
    public class VehicleMakeControllerTest
    {

        private readonly IVehicleService<VehicleMake, 
            VehicleMakeDTORead, 
            VehicleMakeDTOInsert, 
            VehicleMakeDTOReadWithoutID> _vehicleMakerService;

        private VehicleMakeController _vehicleMakeController;

        private readonly IMapping _mapping;

        public VehicleMakeControllerTest()
        {
            //Dependencies
            _vehicleMakerService = A.Fake<IVehicleService
                <VehicleMake, 
                VehicleMakeDTORead, 
                VehicleMakeDTOInsert, 
                VehicleMakeDTOReadWithoutID>>();

            _mapping = A.Fake<IMapping>();

            //SUT

            _vehicleMakeController = new VehicleMakeController(_vehicleMakerService);
        }

        [Fact]
        public async void VehicleMakeController_GetAll_ReturnsOK()
        {
            //Arrange - What I need?

            List<VehicleMakeDTORead>? entityList = A.Fake<List<VehicleMakeDTORead>>();

            var mapper = await _mapping.VehicleMakerMapReadToDTO();

            var makers = A.Fake<ICollection<VehicleMake>>();

            var response = A.Fake<ServiceResponse<List<VehicleMakeDTORead>>>();

            foreach(var item in makers)
            {
              entityList = (List<VehicleMakeDTORead>)A.CallTo(() => mapper.Map<VehicleMakeDTORead>(item)); 
            }

            response.Data = entityList;

            var controller = new VehicleMakeController(_vehicleMakerService);

            A.CallTo(() => _vehicleMakerService.GetAll()).Returns(response);

            //Act - What do I want to do?

            var result = controller.GetAll();

            //Assert 
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<IActionResult>));

        }

        [Fact]
        public async void VehicleMakeController_Create_ReturnsOK()
        {

            var vehicleMake = A.Fake<ServiceResponse<VehicleMake>>();

            var makerDTOInsert = A.Fake<VehicleMakeDTOInsert>();

            A.CallTo(() => _vehicleMakerService.CreateEntity(makerDTOInsert)).Returns(vehicleMake);

            var controller = new VehicleMakeController(_vehicleMakerService);

            var result = controller.CreateEntity(makerDTOInsert);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<IActionResult>));
           
        }

        [Fact]

        public async void VehicleMakeController_Update_ReturnsOK()
        {
            //Arrange
            int id = 1;
            var vehicleMake = A.Fake<ServiceResponse<VehicleMake>>();

            var makerDTOInsert = A.Fake<VehicleMakeDTOInsert>();

            A.CallTo(() => _vehicleMakerService.UpdateEntity(makerDTOInsert, id)).Returns(vehicleMake);

            var controller = new VehicleMakeController(_vehicleMakerService);

            //Act

            var result =  controller.UpdateEntity(makerDTOInsert, id);

            //Assert

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<IActionResult>));

        }


    }
}

