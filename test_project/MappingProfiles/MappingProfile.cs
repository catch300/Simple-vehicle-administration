using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Project_service.Models;
using test_project.Models.ViewModels;
using Project_service.PagingFIlteringSorting;


namespace test_project.MappingProfiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile( )
        {
            //CreateMap<VehicleMake, VehicleMakeVM>().ReverseMap();

            CreateMap<VehicleMakeVM, VehicleMake>().ReverseMap();
            CreateMap<VehicleModelVM, VehicleModel>().ReverseMap();

           

        }



        //public class PagedListConverter<TSource, TDestination> : ITypeConverter<PaginatedList<VehicleMake>, PaginatedList<VehicleMakeVM>>
        //{
        //    public PaginatedList<TDestination> Convert(PaginatedList<TSource> source, PaginatedList<TDestination> destination, ResolutionContext context)
        //    {
        //        var collection = Mapper.Map<IEnumerable<TSource>, IEnumerable<TDestination>>(source.Value);
        //        return new PaginatedList<TDestination>(collection, source.PageIndex, source.PageSize, source.TotalPages);

        //        //var model = (PaginatedList<VehicleMake>)context.SourceValue;
        //        //var vm = model.Select(m => Mapper.Map<VehicleMake, VehicleMakeVM>(m)).ToList();

        //        //return new PaginatedList<VehicleMakeVM>(vm, model.PageNumber, model.PageSize);
        //    }
        //}
    }
}
