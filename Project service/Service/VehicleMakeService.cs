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
    public class VehicleMakeService : IVehicleMakeService
    {
        private readonly VehicleContext _db = new VehicleContext();


        public VehicleMakeService( VehicleContext db)
        {         
            _db = db;

        }

       

        //GET - VehicleMake
        public async Task<IVehicleMake> GetVehicleMake(int? id )
        {
            if (_db != null)
            {
                return await _db.VehicleMakes.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);; 
            }

            return null;


        }
        //GETALL - VehicleMakes
        public async Task<IPaginatedList<IVehicleMake>> GetVehicleMakes(Sort sorting, FIlter filtering, int? page)
        {

            var vehicleMake = from v in _db.VehicleMakes
                              select v;

            if (filtering.SearchString != null)
            {
                page = 1;
            }
            else
            {
                filtering.SearchString = filtering.CurrentFilter;
            }
            
            if (!string.IsNullOrEmpty(filtering.SearchString))
            {
                vehicleMake = vehicleMake.Where(v=> v.Name.Contains(filtering.SearchString)
                                                || v.Abrv.Contains(filtering.SearchString));
            }
            vehicleMake = sorting.SortOrder switch
            {
                "name_desc" => vehicleMake.OrderByDescending(x => x.Name),
                "abrv_desc" => vehicleMake.OrderByDescending(x => x.Abrv),
                "abrv_asc" => vehicleMake.OrderBy(x => x.Abrv),
                _ => vehicleMake.OrderBy(x => x.Name),
            };
            IPaginatedList<IVehicleMake> paginatedList = new PaginatedList<IVehicleMake>();
            int pageSize = 3;
            
           return await paginatedList.CreateAsync(vehicleMake.AsNoTracking(), page ?? 1, pageSize);
        }

        //CREATE - VehicleMake
        public async Task<IVehicleMake> CreateVehicleMake(VehicleMake _vehicleMake)
        {
            _db.VehicleMakes.Add(_vehicleMake);
            await _db.SaveChangesAsync();
            return _vehicleMake;

        }

        //UPDATE - VehicleMake
        public async Task<IVehicleMake> EditVehicleMake(VehicleMake _vehicleMake)
        {
            var vehicleMake = _db.VehicleMakes.Attach(_vehicleMake);
            vehicleMake.State = EntityState.Modified;
            await _db.SaveChangesAsync();
            return _vehicleMake;
        }

        //DELETE - VehicleMake
        public async Task<IVehicleMake> DeleteVehicleMake(int id)
        {
            VehicleMake vehicleMake = _db.VehicleMakes.Find(id);

            if (vehicleMake != null)
            {
                _db.Remove(vehicleMake);
               await _db.SaveChangesAsync();
            }
            return vehicleMake;

           
        }

        
    }

}


   

