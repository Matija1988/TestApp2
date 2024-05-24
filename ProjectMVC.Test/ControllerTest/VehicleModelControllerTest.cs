using FakeItEasy;
using FluentAssertions;
using Xunit;
using ProjectService.Controllers;
using ProjectService.Model;
using ProjectService.Service;
using ProjectService.Mappers;
using Microsoft.AspNetCore.Mvc;

namespace ProjectService.Test.ControllerTest
{
    public class VehicleModelControllerTest
    {
        private readonly IVehicleService<
            VehicleModel,
            VehicleModelDTORead,
            VehicleModelDTOInsert,
            VehicleModelDTOReadWithoutID> _vehicleModelService;

        private readonly VehicleModelController _vehicleModelController;

        private readonly IMapping _mapping;

        public VehicleModelControllerTest()
        {
            _vehicleModelService = A.Fake<IVehicleService<VehicleModel,
                VehicleModelDTORead,
                VehicleModelDTOInsert,
                VehicleModelDTOReadWithoutID>>();

            _mapping = A.Fake<IMapping>();

            _vehicleModelController = new VehicleModelController(_vehicleModelService);

        }

        [Fact]
        public async void VehicleModelController_GetAll_ReturnsOK()
        {
            List<VehicleModelDTORead> entityList = A.Fake<List<VehicleModelDTORead>>();
            var mapper = await _mapping.VehicleModelMapReadToDTO();
            var models = A.Fake<ICollection<VehicleModel>>();
            var response = A.Fake<ServiceResponse<List<VehicleModelDTORead>>>();
            foreach(var item in models)
            {
                entityList = (List<VehicleModelDTORead>)A.CallTo(() => mapper.Map<VehicleModelDTORead>(item));
            }
            response.Data = entityList;

            var controller = new VehicleModelController(_vehicleModelService);

            var result = controller.GetAll();

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<IActionResult>));

        }

        [Fact]
        public async void VehicleModelController_Create_ReturnsOK()
        {
            var mapper = await _mapping.VehicleModelInsertFromDTO();
            var vehicleModel = A.Fake<ServiceResponse<VehicleModel>>();
            var modelDTOInsert = A.Fake<VehicleModelDTOInsert>();
            
            A.CallTo(() => _vehicleModelService.CreateEntity(modelDTOInsert)).Returns(vehicleModel);

            var controller = new VehicleModelController(_vehicleModelService);

            var result =  controller.CreateEntity(modelDTOInsert);

            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<IActionResult>));

        }

        [Fact]
        public async void VehicleModelController_Update_ReturnsOK()
        {
            int id = 1;
            var mapper = await _mapping.VehicleModelInsertFromDTO();
            var vehicleModel = A.Fake<ServiceResponse<VehicleModel>>();
            var modelDTOInsert = A.Fake<VehicleModelDTOInsert>();

            A.CallTo(() => _vehicleModelService.UpdateEntity(modelDTOInsert, id));

            var controller = new  VehicleModelController(_vehicleModelService);

            var result =  controller.UpdateEntity(modelDTOInsert, id);


            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Task<IActionResult>));

        }

    }
}
