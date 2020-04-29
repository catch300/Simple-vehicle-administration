using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Project_service.Models;
using System.Data.Entity;
using Project_service.ViewModels;

namespace Project_service.Service
{
    public class VehicleService : IVehicleMake
    {
        VehicleContext db = new VehicleContext();

        public VehicleService( VehicleContext _db)
        {
            db = _db;
        }

        //GET - VehicleMakes
        public async Task<List<VehicleMake>> GetVehicleMakes( )
        {
            if (db != null)
            {
                return await (from a in db.VehicleMakes.AsNoTracking()
                              select new VehicleMake
                              {
                                  Id = a.Id,
                                  Name = a.Name,
                                  Abrv = a.Abrv
                              }).ToListAsync();
                
            }
           
                return null;
                 
        }

       

        //CREATE - VehicleMake
        public async Task<bool> CreateVehicleMake(VehicleMake _vehicleMake)
        {
            
            VehicleMake vehicleMake = db.VehicleMakes.Where
            (x => x.Id == _vehicleMake.Id).FirstOrDefault();
            if (vehicleMake == null)
            {
                vehicleMake = new VehicleMake()
                {
                    Id = vehicleMake.Id,
                    Name = vehicleMake.Name,
                    Abrv = vehicleMake.Abrv

                };
                db.VehicleMakes.Add(vehicleMake);

            }
            else
            {
                vehicleMake.Id = _vehicleMake.Id;
                vehicleMake.Name = _vehicleMake.Name;
                vehicleMake.Abrv = _vehicleMake.Abrv;
            }

            return await db.SaveChangesAsync() >= 1;
        }

        //UPDATE - VehicleMake
        public async Task<int> editvehiclemake(int? id, VehicleMake vehiclemakeitem)
        {
            int result = 0;

            if (db != null)
            {
                //Find the id for specific VehicleMake
                var vehicleMake = await db.VehicleMakes.FirstOrDefaultAsync(x => x.Id == id);

                if (vehicleMake != null)
                {
                    //Update that VehicleMake
                    db.VehicleMakes.Update(vehicleMake);

                    //Commit the transaction
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }

        //DELETE - VehicleMake
        public async Task<int> DeleteVehicleMake(int? id)
        {

            int result = 0;

            if (db != null)
            {
                //Find the id for  specific VehicleMake
                var vehicleMake = await db.VehicleMakes.FirstOrDefaultAsync(x => x.Id == id);

                if (vehicleMake != null)
                {
                    //Delete that VehicleMake
                    db.VehicleMakes.Remove(vehicleMake);

                    //Commit the transaction
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }
        //GET - VehicleModel
        public async Task<VehicleViewModel> GetVehicleModel(int? Id)
        {
            if (db != null)
            {
                return await (from p in db.VehicleMakes
                              from c in db.VehicleModels
                              where p.Id == Id
                              select new VehicleViewModel
                              {
                                  Id = p.Id,
                                  Name = p.Name,
                                  Abrv = p.Abrv

                              }).FirstOrDefaultAsync();
            }

            return null;
        }
    }

}


   

