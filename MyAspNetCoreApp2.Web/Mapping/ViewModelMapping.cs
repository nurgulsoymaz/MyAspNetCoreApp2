using AutoMapper;
using MyAspNetCoreApp2.Web.Models;
using MyAspNetCoreApp2.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;



namespace MyAspNetCoreApp2.Web.Mapping
{
    public class ViewModelMapping:Profile
    {
        public ViewModelMapping()
        {
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Visitor, VisitorViewModel>().ReverseMap();
            CreateMap<Product, ProductUpdateViewModel > ().ReverseMap();

        }
    }
}
