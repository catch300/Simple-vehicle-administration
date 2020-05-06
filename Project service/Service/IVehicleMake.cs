using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using cloudscribe.Pagination.Models;
using Project_service.Models;

namespace Project_service.Service
{
   public interface IVehicleMake
    {
        IEnumerable<VehicleMake> SortVehicleMakes(IEnumerable<VehicleMake> _vehiclemake);
        IEnumerable<VehicleMake> SearcVehicleMakes(string SearchString);

        Task<PagedResult<VehicleMake>> Paging(int pageNumber, int pageSize);

        Task<VehicleMake> GetVehicleMake(int id);

        Task<List<VehicleMake>> GetVehicleMakes();

        Task<VehicleMake> CreateVehicleMake(VehicleMake _vehicleMake);

        Task<VehicleMake> EditVehicleMake(VehicleMake _vehicleMake);

        Task<VehicleMake> DeleteVehicleMake(int id);

    }
}
