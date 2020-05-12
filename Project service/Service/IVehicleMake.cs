using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Project_service.Models;
using Project_service.Paging;

namespace Project_service.Service
{
   public interface IVehicleMake
    {
       

        Task<VehicleMake> GetVehicleMake(int? id);

        Task<PaginatedList<VehicleMake>> GetVehicleMakes(string sortOrder, string currentFilter, string searchString, int? page);

        Task<VehicleMake> CreateVehicleMake(VehicleMake _vehicleMake);

        Task<VehicleMake> EditVehicleMake(VehicleMake _vehicleMake);

        Task<VehicleMake> DeleteVehicleMake(int id);

    }
}
