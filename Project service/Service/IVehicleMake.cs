using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Project_service.Models;

namespace Project_service.Service
{
   public interface IVehicleMake
    {

        public Task<VehicleMake> GetVehicleMake(int id);

        public Task<List<VehicleMake>> GetVehicleMakes();

        public Task<VehicleMake> CreateVehicleMake(VehicleMake _vehicleMake);

        public Task<VehicleMake> EditVehicleMake(VehicleMake _vehicleMake);

        public Task<VehicleMake> DeleteVehicleMake(int id);

    }
}
