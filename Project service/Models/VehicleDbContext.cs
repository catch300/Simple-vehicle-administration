using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Project_service.Models
{
    public class VehicleDbContext : DbContext
    {
        public VehicleDbContext( )
        {
        }

        public VehicleDbContext(DbContextOptions<VehicleDbContext> options) : base(options)
        {

        }
        public DbSet<VehicleMake> VehicleMakes { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
    }
       
}

