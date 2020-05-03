using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Project_service.Models;
using Project_service.ViewModels;

namespace Project_service.Service
{
    interface IVehicleModel
    {
        public Task<VehicleViewModel> GetVehicleModel(int? Id);

        public Task<List<VehicleModel>> GetVehicleModels();

        public Task<bool> CreateVehicleModel(VehicleModel _vehicleModel);

        public Task<int> EditVehicleModel(int? id);

        public Task<int> DeleteVehicleModel(int? id);
    }
}
