using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Project_service.Models;
using Microsoft.EntityFrameworkCore;



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
        public async Task<List<VehicleModel>> GetVehicleModels( )
        {
            if (db != null)
            {
                var vehicleContext = db.VehicleModels.Include(v => v.Make);
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
