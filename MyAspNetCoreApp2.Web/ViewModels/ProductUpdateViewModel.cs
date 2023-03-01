using System.ComponentModel.DataAnnotations.Schema;

namespace MyAspNetCoreApp2.Web.ViewModels
{
    public class ProductUpdateViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public string Color { get; set; }

        public int? Width { get; set; }
        public int? Height { get; set; }
        [NotMapped]
        public IFormFile Image { get; set; }
        // [ValidateNever]
        [NotMapped]
        public string ImagePath { get; set; }
        public int CategoryId { get; set; }  //*
        public string CategoryName { get; set; } //?
       
    }
}
