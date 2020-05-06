using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Project_service.Models;
using System.Data.Entity;
using cloudscribe.Pagination.Models;



namespace Project_service.Service
{
    public class VehicleModelService : IVehicleModel
    {
        private readonly VehicleContext db = new VehicleContext();

        public VehicleModelService(VehicleContext _db)
        {
            db = _db;
        }

        public IEnumerable<VehicleModel> SortVehicleMakes(IEnumerable<VehicleModel> _vehiclemodel)
        {
            return _vehiclemodel.OrderBy(x => x.Name);
        }

        //FILTERING
        public IEnumerable<VehicleModel> SearcVehicleMakes(string SearchString)
        {
            return db.VehicleModels.Where(x => x.Name.Contains(SearchString));
        }


        //PAGING
        public async Task<PagedResult<VehicleModel>> Paging(int pageNumber, int pageSize)
        {

            int Exclude = (pageSize * pageNumber) - pageSize;
            var VehicleModel = from b in db.VehicleModels select b;

            var VehicleModelCount = VehicleModel.CountAsync();


            //PAGINATION
            var result = new PagedResult<VehicleModel>
            {
                Data = await db.VehicleModels.AsNoTracking().ToListAsync(),
                TotalItems = await VehicleModelCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        


        //GET - VehicleModel
        public async Task<VehicleModel> GetVehicleModel(int Id)
        {
            if (db != null)
            {
                return await db.VehicleModels.FindAsync(Id);
            }

            return null;
        }

        //GETALL - VehicleModels
        public async Task<List<VehicleModel>> GetVehicleModels( )
        {
            if (db != null)
            {
                return await db.VehicleModels.ToListAsync();

            }
            return null;

        }



        //CREATE - VehicleModel
        public async Task<VehicleModel> CreateVehicleModel(VehicleModel _vehicleModel)
        {

            db.VehicleModels.Add(_vehicleModel);
            await db.SaveChangesAsync();
            return _vehicleModel;
        }

        //UPDATE - VehicleModel
        public async Task<VehicleModel> EditVehicleModel(VehicleModel _vehicleModel)
        {
            var vehicleModel = db.VehicleModels.Attach(_vehicleModel);
            vehicleModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await db.SaveChangesAsync();
            return _vehicleModel;
        }

        //DELETE - VehicleModel
        public async Task<VehicleModel> DeleteVehicleModel(int id)
        {

            VehicleModel vehicleModel = db.VehicleModels.Find(id);
            if (vehicleModel != null)
            {
                db.VehicleModels.Remove(vehicleModel);
                await db.SaveChangesAsync();
            }
            return vehicleModel;
        }
    }
}
