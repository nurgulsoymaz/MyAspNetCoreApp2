using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using AutoMapper;


namespace MyAspNetCoreApp2.Web.Models
{
  

    public class Visitor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public DateTime Created { get; set; }
    }
}
