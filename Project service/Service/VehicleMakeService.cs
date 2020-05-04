using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Project_service.Models;
using System.Data.Entity;
using Project_service.ViewModels;
using cloudscribe.Pagination.Models;

namespace Project_service.Service
{
    public class VehicleMakeService : IVehicleMake
    {
        private readonly VehicleContext db = new VehicleContext();

        public VehicleMakeService( VehicleContext _db)
        {
            db = _db;
        }

        //SORTING, FILTERING, PAGING
        public PagedResult<VehicleMake> SortFilterPage(int pageNumber, int pageSize, string sortOrder, string filter)
        {
            
            int Exclude = (pageSize * pageNumber) - pageSize;
            var VehicleMake = from b in db.VehicleMakes select b;

            var VehicleMakeCount = VehicleMake.Count();
            
            //FILTER by name
            if (!String.IsNullOrEmpty(filter))
            {
                VehicleMake = VehicleMake.Where(b => b.Name.Contains(filter));
                VehicleMakeCount = VehicleMake.Count();
            }

            //SORTING by name
            switch (sortOrder)
            {
                case "name_desc":
                    VehicleMake = VehicleMake.OrderByDescending(b => b.Name);
                    break;
                default:
                case "name_asc":
                    VehicleMake = VehicleMake.OrderBy(b => b.Name);
                    break;

            }

            VehicleMake = VehicleMake.Skip(Exclude).Take(pageSize);

            //PAGINATION
            var result = new PagedResult<VehicleMake>
            {
                Data = db.VehicleMakes.AsNoTracking().ToList(),
                TotalItems = VehicleMakeCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }


        //GET - VehicleMake
        public async Task<VehicleMake> GetVehicleMake(int id )
        {
            if (db != null)
            {
                return await db.VehicleMakes.FindAsync(id); 
            }

            return null;


        }
        //GETALL - VehicleMakes
        public async Task<List<VehicleMake>> GetVehicleMakes()
        {
            if (db != null)
            {
                return await db.VehicleMakes.ToListAsync();
                
            }
                return null;
                 
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
            vehicleMake.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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


   

