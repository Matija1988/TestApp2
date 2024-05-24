using Ninject.Modules;
using ProjectService.Controllers;
using ProjectService.Mappers;
using ProjectService.Model;
using ProjectService.Service;

namespace ProjectService
{
    public class NinjectController : NinjectModule
    {
        

        public override void Load()
        {
            Bind<IMapping>().To<MapperConfiguration>();
         //   Bind<MapperConfiguration>().ToSelf();

            Bind<IVehicleService<VehicleMake, VehicleMakeDTORead, VehicleMakeDTOInsert, VehicleMakeDTOReadWithoutID>>()
                .To
                <VehicleMakeService>().InTransientScope();
 

            Bind<IVehicleService
                <VehicleModel, VehicleModelDTORead, VehicleModelDTOInsert, VehicleModelDTOReadWithoutID>>()
                .To
                <VehicleModelService>();
            Bind<VehicleModelService>().ToSelf();

            Bind<IController<VehicleMakeDTOInsert>>().To<VehicleMakeController>();
            Bind<VehicleMakeController>().ToSelf();

        }
    }
}
