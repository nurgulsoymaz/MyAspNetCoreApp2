using Microsoft.AspNetCore.Mvc.Rendering;
using MyAspNetCoreApp2.Web.Models;
using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.FileProviders;


namespace MyAspNetCoreApp2.Web.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public string Color { get; set; }

        public int? Width { get; set; }
        public int? Height { get; set; }
        public bool IsPublish { get; set; }
     
        public IFormFile Image { get; set; }  
       // [ValidateNever]
        public string ImagePath { get; set; }
        public int CategoryId { get; set; }  //*
        public string CategoryName { get; set; } //?
        public DateTime? PublishDate { get; set; }
        public string Expire { get; set; }
        public string Description { get; set; }

    }
}
