using Microsoft.EntityFrameworkCore;

namespace MyAspNetCoreApp2.Web.Models
{
    public class AppDbContext : DbContext //DbContext sınıfından miras alsın ve sonra bir constructor oluştur
    {
        //constructor DbContextOptions sınıfı alsın ve bunu seçenklendirelim appdbcontext ile. yazdığım options ta basedeki optionsa yani miras aldığımız dbcontexti constructora gönderelim.optionsta program.cs de doldurulcak. 
        public AppDbContext(DbContextOptions<AppDbContext> options) :base(options)       

        {

        }
        //product tablomu entity olarak adlandıralım. bir dbset oluşturalım ve products sınıfını verelim. ve bunun hangi coonection stringe bağlanacağını program.cs de belirt.
        public DbSet<Product> Products { get; set; }
    }
}
