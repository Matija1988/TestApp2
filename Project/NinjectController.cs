using Ninject.Modules;
using ProjectService.Controllers;
using ProjectService.Mappers;
using ProjectService.Model;
using ProjectService.Service;

namespace ProjectService
{
    public class NinjectController : NinjectModule
    {
        /// <summary>
        /// Stavio sam ovaj kod ovdje i ostavio klasu za eventualni rad u buducnosti
        /// Ninject na .NET 8 ne stvara NinjectWebComon
        /// </summary>

        //IKernel kernel = new StandardKernel(new NinjectController());
        //MapperConfiguration mapper = kernel.Get<MapperConfiguration>();
        //VehicleMakeService vehicleMakeService = kernel.Get<VehicleMakeService>();
        //VehicleModelService vehicleModelService = kernel.Get<VehicleModelService>();


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
