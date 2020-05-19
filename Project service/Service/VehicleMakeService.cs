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
    public class VehicleMakeService : IVehicleMake
    {
        private readonly VehicleContext db = new VehicleContext();

        public VehicleMakeService( VehicleContext _db)
        {
            db = _db;
        }

       

        //GET - VehicleMake
        public async Task<VehicleMake> GetVehicleMake(int? id )
        {
            if (db != null)
            {
                return await db.VehicleMakes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);; 
            }

            return null;


        }
        //GETALL - VehicleMakes
        public async Task<PaginatedList<VehicleMake>> GetVehicleMakes(Sorting sort, Filtering filter,  int? page)
        {

            var vehicleMake = from v in db.VehicleMakes
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
                vehicleMake = vehicleMake.Where(v=> v.Name.Contains(filter.SearchString)
                                                || v.Abrv.Contains(filter.SearchString));
            }
            vehicleMake = sort.SortOrder switch
            {
                "name_desc" => vehicleMake.OrderByDescending(x => x.Name),
                "abrv_desc" => vehicleMake.OrderByDescending(x => x.Abrv),
                "abrv_asc" => vehicleMake.OrderBy(x => x.Abrv),
                _ => vehicleMake.OrderBy(x => x.Name),
            };

            int pageSize = 3;
           return  await PaginatedList<VehicleMake>.CreateAsync(vehicleMake.AsNoTracking(), page ?? 1, pageSize);
        }

        //CREATE - VehicleMake
        public async Task<VehicleMake> CreateVehicleMake(VehicleMake _vehicleMake)
        {
            db.VehicleMakes.Add(_vehicleMake);
            await db.SaveChangesAsync();
            return _vehicleMake;

        }

        //UPDATE - VehicleMake
        public async Task<VehicleMake> EditVehicleMake(VehicleMake _vehicleMake)
        {
            var vehicleMake = db.VehicleMakes.Attach(_vehicleMake);
            vehicleMake.State = EntityState.Modified;
            await db.SaveChangesAsync();
            return _vehicleMake;
        }

        //DELETE - VehicleMake
        public async Task<VehicleMake> DeleteVehicleMake(int id)
        {
            VehicleMake vehicleMake = db.VehicleMakes.Find(id);

            if (vehicleMake != null)
            {
                db.Remove(vehicleMake);
               await db.SaveChangesAsync();
            }
            return vehicleMake;

           
        }

        
    }

}


   

