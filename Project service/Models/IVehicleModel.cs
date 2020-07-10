using System;
using System.Collections.Generic;
using System.Text;

namespace Project_service.Models
{
    public interface IVehicleModel
    {
        public int Id { get; set; }

        public VehicleMake Make { get; set; }
        public int MakeId { get; set; }

        public string Name { get; set; }

        public string Abrv { get; set; }
    }
}
