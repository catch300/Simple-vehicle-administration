using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Project_service.Models;

namespace Project_service.Service
{
    interface IVehicleMake
    {

        public Task<VehicleMake> GetVehicleMake(int id);

        public Task<List<VehicleMake>> GetVehicleMakes();

        public Task<bool> CreateVehicleMake(VehicleMake _vehicleMake);

        public Task<int> EditVehicleMake(int id);

        public Task<int> DeleteVehicleMake(int id);

    }
}
