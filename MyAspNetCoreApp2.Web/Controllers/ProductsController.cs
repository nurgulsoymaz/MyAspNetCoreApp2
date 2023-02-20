using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp2.Web.Models;

namespace MyAspNetCoreApp2.Web.Controllers
{
   
    public class ProductsController : Controller
    {
        //products.controller da dataları veri tabanından çekeceğim için appdbcontext nesnesine ihtiyacım var

        private AppDbContext _context; //bir _context nesnesi aldık

        private readonly ProductRepository _productRepository;
        
        //appdbcontexti constructorda geçelim
        public ProductsController(AppDbContext context)

        {
            //DI CONTAİNER 
            //dependency injection pattern 
            _productRepository = new ProductRepository();

            //bir classın ihtiyaç duyduğu nesneyi (AppDbContext) constructordan alıyorsa dependency injection denir.yani design pattern aracılığıyla bu nesneyi alıyoruz. Bizim bu yöntemde nesneyi constructorda geçip nesne örneği almamızı sağlayan yapıyada DI Container denir.

            _context= context; //nesne örneği üretilmiş bir dbcontext

            //CONROLLERA data kaydedelim
            //if(!_context.Products.Any())
            //{
            //    _context.Products.Add(new Product() { Name = "Kalem 1", Price = 100, Stock = 100, Color = "Red", Height = 10, Width = 20 });
            //    _context.Products.Add(new Product() { Name = "Kalem 2", Price = 100, Stock = 200, Color = "Yellow", Height = 20, Width = 20 });
            //    _context.Products.Add(new Product() { Name = "Kalem 3", Price = 100, Stock = 300, Color = "Blue", Height = 20, Width = 20 });

            //    _context.SaveChanges();

            //}


            if (!_productRepository.GetAll().Any()) //Data yoksa false data varsa true döner

            {
           
            }
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            //var product = _context.Products.First();

            return View(products);
        }
        public IActionResult Remove(int id)
        {
            var product = _context.Products.Find(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            TempData["status"] = "Ürün başarıyla silindi !!!";
            //_productRepository.Remove(id);
            return RedirectToAction("Index");
            
        }
        [HttpGet] //Add sayfasına gittiğimde karşıma çıkan formu getirir. getlerin sayfası olur ama postun sayfası olmaz
        public IActionResult Add() 
        { 

            return View();
        }

        //ADD.cshtml deki butonu çalıştırmak için httppost yapıp yeni metot yazmalıyız
        [HttpPost]  //Add sayfasına gittiğimde butona bastığımda verileri kaydeden metot. 
        //3.yöntem (best practice method)
        public IActionResult Add(Product newProduct)
        {
            //1. yöntem
            //var name = HttpContext.Request.Form["Name"].ToString();
            //var price = decimal.Parse(HttpContext.Request.Form["Price"].ToString());
            //var stock = int.Parse(HttpContext.Request.Form["Stock"].ToString());
            //var color = HttpContext.Request.Form["Color"].ToString();
            //var width = int.Parse(HttpContext.Request.Form["Width"].ToString());
            //var height = int.Parse(HttpContext.Request.Form["Height"].ToString());

            //2.yöntem
           // public IActionResult Add(string Name, decimal Price, int Stock, string Color, int Width, int Height)
           //Product newProduct = new Product() { Name = Name, Price = Price, Color = Color, Stock = Stock, Width = Width, Height = Height };

            _context.Products.Add(newProduct);

            _context.SaveChanges();

            //BİR SAYFADAN DİĞER SAYFAYA VERİ TAŞIYACAĞIM İÇİN TEMPDATA KULLANDIM
            TempData["status"] = "Ürün başarıyla eklendi:)"; // bunu index.cshtml de div ile göster

            return RedirectToAction("Index");
        }

        //update işlemini ürün id sine göre yaptık ve bütün ürünleri view katmanına çektik
        [HttpGet]
        public IActionResult Update(int id)
        {
            var product = _context.Products.Find(id);

            return View(product);
        }
        [HttpPost]

        public IActionResult Update(Product updateProduct)
        {
            _context.Products.Update(updateProduct);
            _context.SaveChanges();

            //BİR SAYFADAN DİĞER SAYFAYA VERİ TAŞIYACAĞIM İÇİN TEMPDATA KULLANDIM
            TempData["status"] = "Ürün başarıyla güncellendi :)"; // bunu index.cshtml de div ile göster

           return RedirectToAction("Index");
        }

    }
}
