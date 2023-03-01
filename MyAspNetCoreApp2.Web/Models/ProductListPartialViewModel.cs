
namespace MyAspNetCoreApp2.Web.Models
{
    public class ProductListPartialViewModel
    { 
    
    public List<ProductPartialViewModel> Products { get; set; }
   
    
    }
    public class ProductPartialViewModel 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
       // public IFormFile Image { get; set; }
        public string ImagePath { get; set; }
        // public IFormFile File { get; set; }
    }
}
