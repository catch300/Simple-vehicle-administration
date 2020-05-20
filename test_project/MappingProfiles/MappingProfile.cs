using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Project_service.Models;
using test_project.Models.ViewModels;


namespace test_project.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile( )
        {      
            CreateMap<VehicleMakeVM, VehicleMake>().ReverseMap();

            CreateMap<VehicleModelVM, VehicleModel>().ReverseMap();
            

        }

    }
}
