using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Project_service.Models;
using Project_service.PagingFIlteringSorting;

namespace Project_service.Service
{
    public interface IVehicleModelService
    {

      
        Task<IVehicleModel> GetVehicleModel(int? Id);

        Task<PaginatedList<IVehicleModel>> GetVehicleModels(Sorting sort, Filtering filter,  int? page);

        Task<IVehicleModel> CreateVehicleModel(VehicleModel _vehicleModel);

        Task<IVehicleModel> EditVehicleModel(VehicleModel _vehicleModel);

        Task<IVehicleModel> DeleteVehicleModel(int id);
    }
}
