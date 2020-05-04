using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Project_service.Models;
using Project_service.ViewModels;

namespace Project_service.Service
{
    public interface IVehicleModel
    {
        public Task<VehicleModel> GetVehicleModel(int Id);

        public Task<List<VehicleModel>> GetVehicleModels();

        public Task<VehicleModel> CreateVehicleModel(VehicleModel _vehicleModel);

        public Task<VehicleModel> EditVehicleModel(VehicleModel _vehicleModel);

        public Task<VehicleModel> DeleteVehicleModel(int id);
    }
}
