using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Project_service.Models;

namespace Project_service.ViewModels
{
    public class VehicleViewModel
    {
        public  VehicleMake VehicleMake { get; set; }

        public IEnumerable<VehicleModel> VehicleModel { get; set; }
        
    }
}
