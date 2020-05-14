using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Project_service.Models;
using Project_service.PagingFIlteringSorting;

namespace Project_service.Service
{
    public interface IVehicleModel
    {

      
        Task<VehicleModel> GetVehicleModel(int? Id);

        Task<PaginatedList<VehicleModel>> GetVehicleModels(Sorting sort, Filtering filter,  int? page);

        Task<VehicleModel> CreateVehicleModel(VehicleModel _vehicleModel);

        Task<VehicleModel> EditVehicleModel(VehicleModel _vehicleModel);

        Task<VehicleModel> DeleteVehicleModel(int id);
    }
}
