﻿using ProjectService.Model;

namespace ProjectService.Service
{
    public interface IVehicleMakeService
    {
        Task<ServiceResponse<List<VehicleMakeDTORead>>> GetVehicleMakers();

        Task<ServiceResponse<VehicleMake>> CreateVehicleMake(VehicleMakeDTOInsert dto);

        Task<ServiceResponse<VehicleMake>> UpdateVehicleMake(VehicleMakeDTOInsert dto, int id);

        Task<ServiceResponse<VehicleMake>> DeleteVehicleMake(int id);

       
    }
}
