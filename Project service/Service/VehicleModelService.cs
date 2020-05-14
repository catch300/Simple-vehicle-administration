using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Project_service.Models;
using Microsoft.EntityFrameworkCore;
using Project_service.PagingFIlteringSorting;

namespace Project_service.Service
{
    public class VehicleModelService : IVehicleModel
    {
        private readonly VehicleContext db = new VehicleContext();

        public VehicleModelService(VehicleContext _db)
        {
            db = _db;
        }

      

        //GET - VehicleModel
        public async Task<VehicleModel> GetVehicleModel(int? Id)
        {
            var vehicleModel = await db.VehicleModels
               .Include(v => v.Make)
               .FirstOrDefaultAsync(m => m.Id == Id);
            if (db != null)
            {
                return  vehicleModel;
            }

            return null;
        }

        //GETALL - VehicleModels
        public async Task<PaginatedList<VehicleModel>> GetVehicleModels(Sorting sort, Filtering filter, int? page)
        {
            var vehicleModel = from v in db.VehicleModels.Include(v => v.Make)
                                 select v;
            
            if (filter.SearchString != null)
            {
                page = 1;
            }
            else
            {
                filter.SearchString = filter.CurrentFilter;
            }

            if (!string.IsNullOrEmpty(filter.SearchString))
            {
                vehicleModel = vehicleModel.Where(v => v.Make.Name.Contains(filter.SearchString));
            }

            vehicleModel = sort.SortOrder switch
            {
                "name_desc" => vehicleModel.OrderByDescending(x => x.Name),
                "abrv_desc" => vehicleModel.OrderByDescending(x => x.Abrv),
                "abrv_asc" => vehicleModel.OrderBy(x => x.Abrv),
                "make_asc" => vehicleModel.OrderBy(x => x.Make),
                "make_desc" => vehicleModel.OrderByDescending(x => x.Make),
                _ => vehicleModel.OrderBy(x => x.Name),
            };

            int pageSize = 3;
            return await PaginatedList<VehicleModel>.CreateAsync(vehicleModel.AsNoTracking(), page ?? 1, pageSize);

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
