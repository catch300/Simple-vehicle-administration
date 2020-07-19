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

        Task<IPaginatedList<IVehicleModel>> GetVehicleModels(ISorting sorting, IFiltering filtering, int? page);

        Task<IVehicleModel> CreateVehicleModel(VehicleModel _vehicleModel);

        Task<IVehicleModel> EditVehicleModel(VehicleModel _vehicleModel);

        Task<IVehicleModel> DeleteVehicleModel(int id);
    }
}
