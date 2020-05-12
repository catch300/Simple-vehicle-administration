using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Project_service.Models;
using Project_service.Paging;

namespace Project_service.Service
{
    public interface IVehicleModel
    {

      
        Task<VehicleModel> GetVehicleModel(int? Id);

        Task<PaginatedList<VehicleModel>> GetVehicleModels(string sortOrder, string currentFilter, string searchString, int? page);

        Task<VehicleModel> CreateVehicleModel(VehicleModel _vehicleModel);

        Task<VehicleModel> EditVehicleModel(VehicleModel _vehicleModel);

        Task<VehicleModel> DeleteVehicleModel(int id);
    }
}
