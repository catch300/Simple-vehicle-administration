using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Project_service.Models;
using Project_service.PagingFIlteringSorting;

namespace Project_service.Service
{
   public interface IVehicleMakeService
    {
       

        Task<IVehicleMake> GetVehicleMake(int? id);

        Task<IPaginatedList<IVehicleMake>> GetVehicleMakes(Sort sorting, FIlter filtering, int? page);

        Task<IVehicleMake> CreateVehicleMake(VehicleMake _vehicleMake);

        Task<IVehicleMake> EditVehicleMake(VehicleMake _vehicleMake);

        Task<IVehicleMake> DeleteVehicleMake(int id);

    }
}
