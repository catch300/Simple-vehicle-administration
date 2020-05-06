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
    public class VehicleMakeService : IVehicleMake
    {
        private readonly VehicleContext db = new VehicleContext();

        public VehicleMakeService( VehicleContext _db)
        {
            db = _db;
        }

        //SORTING
        public IEnumerable<VehicleMake> SortVehicleMakes(IEnumerable<VehicleMake> _vehiclemake)
        {
            return _vehiclemake.OrderBy(x => x.Name);
        }

        //FILTERING
        public IEnumerable<VehicleMake> SearcVehicleMakes(string SearchString)
        {
            return db.VehicleMakes.Where(x => x.Name.Contains(SearchString));
        }


        //PAGING
        public async Task<PagedResult<VehicleMake>> Paging(int pageNumber, int pageSize)
        {
            
            int Exclude = (pageSize * pageNumber) - pageSize;
            var VehicleMake = from b in db.VehicleMakes select b;

            var VehicleMakeCount = VehicleMake.CountAsync();
            
            
            //PAGINATION
            var result = new   PagedResult<VehicleMake>
            {
                Data = await db.VehicleMakes.AsNoTracking().ToListAsync(),
                TotalItems =await VehicleMakeCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return  result;
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


   

