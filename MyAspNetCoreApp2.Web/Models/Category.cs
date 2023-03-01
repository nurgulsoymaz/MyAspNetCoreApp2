namespace MyAspNetCoreApp2.Web.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //EF Core da bir entity ile diğer entity arasında bir ilişki kurmak istersek navigation property gibi özel propertylerden faydalanırız. bu properti diğer entitylere referans olur

        public List<Product> products { get; set; }
    }
}
