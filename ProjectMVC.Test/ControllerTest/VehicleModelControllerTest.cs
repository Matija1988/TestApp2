using ProjectService.Model;
using ProjectService.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectService.Test.ControllerTest
{
    public class VehicleModelControllerTest
    {
        private readonly IVehicleService<
            VehicleModel,
            VehicleModelDTORead,
            VehicleModelDTOInsert,
            VehicleModelDTOReadWithoutID> _vehicleModelService;

    }
}
