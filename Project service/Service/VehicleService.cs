using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Project_service.Interface;
using Project_service.Models;
using System.Data.Entity;

namespace Project_service.Service
{
    public class VehicleService : IVehicleMake
    {
        
        //CREATE
        public async Task<bool> CreateVehicleMake(VehicleMake _vehicleMake)
        {
            using VehicleDbContext db = new VehicleDbContext();
            Project_service.Models.VehicleMake vehicleMake = db.VehicleMakes.Where
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

        //DELETE
        public async Task<bool> DeleteVehicleMake(int id)
        {
            using VehicleDbContext db = new VehicleDbContext();
            Project_service.Models.VehicleMake vehicleMake =
            db.VehicleMakes.Where(x => x.Id == id).FirstOrDefault();
            if (vehicleMake != null)
            {
                db.VehicleMakes.Remove(vehicleMake);
            }
            return await db.SaveChangesAsync() >= 1;
        }

        //GET
        public async Task<List<VehicleMake>> GetVehicleMakes( )
        {
            using VehicleDbContext db = new VehicleDbContext();
            return await (from a in db.VehicleMakes.AsNoTracking()
                          select new VehicleMake
                          {
                              Id = a.Id,
                              Name = a.Name,
                              Abrv = a.Abrv
                          }).ToListAsync();
        }

        //EDIT
        //public VehicleMake EditVehicleMake(string id, VehicleMake VehicleMakeItem)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
