using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Project_service.Models;
using Project_service.PagingFIlteringSorting;

namespace test_project.Models.ViewModels
{
    public class  VehicleMakeVM
    {

        public VehicleMakeVM(int id, string name, string abrv)
        {
            Id = id;
            Name = name;
            Abrv = abrv;
        }

        public int Id { get; set; }      
        public string Name { get; set; }
        public string Abrv { get; set; }

    }
}
