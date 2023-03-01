using Microsoft.AspNetCore.Mvc.Rendering;
using MyAspNetCoreApp2.Web.Models;
using System.Reflection;
using AutoMapper;


namespace MyAspNetCoreApp2.Web.ViewModels
{
    public class VisitorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public DateTime Created { get; set; }

        public string Date=>Created.ToShortDateString();
    }
    
       
    }
