using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using cloudscribe.Pagination.Models;
using Project_service.Models;

namespace Project_service.Service
{
    public interface IVehicleModel
    {

        IEnumerable<VehicleModel> SortVehicleMakes(IEnumerable<VehicleModel> _vehiclemake);
        IEnumerable<VehicleModel> SearcVehicleMakes(string SearchString);

        Task<PagedResult<VehicleModel>> Paging(int pageNumber, int pageSize);

        public Task<VehicleModel> GetVehicleModel(int? Id);

        public Task<List<VehicleModel>> GetVehicleModels();

        public Task<VehicleModel> CreateVehicleModel(VehicleModel _vehicleModel);

        public Task<VehicleModel> EditVehicleModel(VehicleModel _vehicleModel);

        public Task<VehicleModel> DeleteVehicleModel(int id);
    }
}
