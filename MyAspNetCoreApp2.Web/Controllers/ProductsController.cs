using AutoMapper;
using MyAspNetCoreApp2.Web.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyAspNetCoreApp2.Web.Models;
using MyAspNetCoreApp2.Web.ViewModels;
using MyAspNetCoreApp2.Web.Filters;
using Microsoft.Extensions.FileProviders;
using Microsoft.EntityFrameworkCore;

namespace MyAspNetCoreApp2.Web.Controllers
{
    //[Route("[controller]/[action]")]
    public class ProductsController : Controller
    {
        //products.controller da dataları veri tabanından çekeceğim için appdbcontext nesnesine ihtiyacım var

        private AppDbContext _context; //bir _context nesnesi aldık

        //private readonly IMapper _mapper;

       private readonly IFileProvider _fileProvider;

        private readonly ProductRepository _productRepository;

        private readonly IMapper _mapper;
        
        //appdbcontexti constructorda geçelim
        public ProductsController(AppDbContext context, IMapper mapper,IFileProvider fileProvider ) 
            
        {
            //DI CONTAİNER 
            //dependency injection pattern 
            _productRepository = new ProductRepository();

            //bir classın ihtiyaç duyduğu nesneyi (AppDbContext) constructordan alıyorsa dependency injection denir.yani design pattern aracılığıyla bu nesneyi alıyoruz. Bizim bu yöntemde nesneyi constructorda geçip nesne örneği almamızı sağlayan yapıyada DI Container denir.

            _context= context; //nesne örneği üretilmiş bir dbcontext
            _mapper=mapper;
           _fileProvider= fileProvider;


            if (!_productRepository.GetAll().Any()) //Data yoksa false data varsa true döner

            {
           
            }
        }
        //[CacheResourceFilter]
        public IActionResult Index()
        {
            List<ProductViewModel> products = _context.Products.Include(x => x.Category).Select(x => new ProductViewModel()

            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock,
                Width= x.Width,
                Height= x.Height,
                CategoryName = x.Category.Name,
                Color = x.Color,
                Description = x.Description,
                Expire = x.Expire,
                ImagePath = x.ImagePath,
                IsPublish = x.IsPublish,
                PublishDate = x.PublishDate


            }).ToList(); 

            return View(products);
           
        }
        //attribute routink kullancaksak
        //[Route("[controller]/[action]/{page}/{pagesize}")]
        public IActionResult Pages(int page, int pageSize)
        {
            //page=1 pagesize=3 => ilk 3 kayıt
            //page=2 pagesize=3 => ikinci 3 kayıt
            //page= pagesize=3 => üçüncü 3 kayıt

            var products= _context.Products.Skip((page-1)*pageSize).Take(pageSize).ToList();

            ViewBag.page = page;
            ViewBag.pageSize = pageSize;

            return View(_mapper.Map<List<Product>>(products));
        }
          

        [ServiceFilter(typeof(NotFoundFilter))] //NotFoundFilter ServiceFilter ile kullanılır ve ayrıca DI containera eklenir
        //attribute routink kullancaksak
        //[Route("[controller]/[action]/{id}")]
        public IActionResult GetById(int id)
        {
            var product = _context.Products.Find(id);

            return View(_mapper.Map<Product>(product));
        }

        [ServiceFilter(typeof(NotFoundFilter))]
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
            //eğer expire ı hem int hem string tutmak istersem;
            //ViewBag.Expire = new Dictionary<string, int>()
            //{
            // { "1 ay", 1}
            // }; //add.cshtml dede foreach döngüsünde as den sonra Dictionary<string, int ekle ve @item.Key yap.
            ViewBag.Expire = new List<string>() { "2 ay", "4 ay", "6 ay", "8 ay" };

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new() { Data="Blue", Value="Blue"},
                 new() { Data="Red", Value="Red"},
                  new() { Data="Yellow", Value="Yellow"},
                   new() { Data="Dark", Value="Dark"},
                    new() { Data="Gray", Value="Gray"},
                     new() { Data="White", Value="White"},
                      new() { Data="Orange", Value="Orange"},
                       new() { Data="Green", Value="Green"},
                        new() { Data="Brown", Value="Brown"},
                         new() { Data="Purple", Value="Purple"},
                          new() { Data="Pink", Value="Pink"}

            }, "Value", "Data");

            var categories = _context.Category.ToList();

            ViewBag.categorySelect = new SelectList(categories, "Id", "Name");

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

            //requried validation için ;
            IActionResult result = null;


            if (ModelState.IsValid)   //validation kontrol noktası           

            {
                try
                {
                    var product = _mapper.Map<Product>(newProduct);

                    if (newProduct.Image!=null && newProduct.Image.Length>0)
                    {
                        var root = _fileProvider.GetDirectoryContents("wwwroot");

                        var images = root.First(x => x.Name == "images");

                        var randomImageName = Guid.NewGuid() + Path.GetExtension(newProduct.Image.FileName);

                        var path = Path.Combine(images.PhysicalPath, randomImageName);

                        using var stream = new FileStream(path, FileMode.Create);

                        newProduct.Image.CopyTo(stream);

             

                        product.ImagePath = randomImageName;
                    }
                   

                _context.Products.Add(product);

                _context.SaveChanges();

                //BİR SAYFADAN DİĞER SAYFAYA VERİ TAŞIYACAĞIM İÇİN TEMPDATA KULLANDIM
                TempData["status"] = "Ürün başarıyla eklendi:)"; // bunu index.cshtml de div ile göster

                return RedirectToAction("Index");
                }
                catch (Exception) 
                {
                    result = View();
                }

            }

            else
            {
        
              result= View();
                
            }
            var categories = _context.Category.ToList();//1

            ViewBag.categorySelect = new SelectList(categories, "Id", "Name");//1

            ViewBag.Expire = new List<string>() { "2 ay", "4 ay", "6 ay", "8 ay" };

            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>()
            {
                new() { Data="Blue", Value="Blue"},
                 new() { Data="Red", Value="Red"},
                  new() { Data="Yellow", Value="Yellow"},
                   new() { Data="Dark", Value="Dark"},
                    new() { Data="Gray", Value="Gray"},
                     new() { Data="White", Value="White"},
                      new() { Data="Orange", Value="Orange"},
                       new() { Data="Green", Value="Green"},
                        new() { Data="Brown", Value="Brown"},
                         new() { Data="Purple", Value="Purple"},
                          new() { Data="Pink", Value="Pink"}

            }, "Value", "Data");
            return result;
        }



        [ServiceFilter(typeof(NotFoundFilter))]

        //update işlemini ürün id sine göre yaptık ve bütün ürünleri view katmanına çektik

        [HttpGet]
        public IActionResult Update(int id)
        {

            var product = _context.Products.Find(id);

            var categories = _context.Category.ToList();//2

            ViewBag.categorySelect = new SelectList(categories, "Id", "Name", product.CategoryId);//2


            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>() {

                 new() { Data="Blue", Value="Blue"},
                 new() { Data="Red", Value="Red"},
                  new() { Data="Yellow", Value="Yellow"},
                   new() { Data="Dark", Value="Dark"},
                    new() { Data="Gray", Value="Gray"},
                     new() { Data="White", Value="White"},
                      new() { Data="Orange", Value="Orange"},
                       new() { Data="Green", Value="Green"},
                        new() { Data="Brown", Value="Brown"},
                         new() { Data="Purple", Value="Purple"},
                          new() { Data="Pink", Value="Pink"}



             }, "Value", "Data", product.Color);


            return View(_mapper.Map<ProductUpdateViewModel>(product));
        }

        [HttpPost]
        public IActionResult Update(ProductUpdateViewModel updateProduct)
        {


            ViewBag.ColorSelect = new SelectList(new List<ColorSelectList>() {

                 new() { Data="Blue", Value="Blue"},
                 new() { Data="Red", Value="Red"},
                  new() { Data="Yellow", Value="Yellow"},
                   new() { Data="Dark", Value="Dark"},
                    new() { Data="Gray", Value="Gray"},
                     new() { Data="White", Value="White"},
                      new() { Data="Orange", Value="Orange"},
                       new() { Data="Green", Value="Green"},
                        new() { Data="Brown", Value="Brown"},
                         new() { Data="Purple", Value="Purple"},
                          new() { Data="Pink", Value="Pink"}



             }, "Value", "Data", updateProduct.Color);


           

            if (updateProduct.Image != null && updateProduct.Image.Length > 0)
            {
                var root = _fileProvider.GetDirectoryContents("wwwroot");

                var images = root.First(x => x.Name == "images");

                var randomImageName = Guid.NewGuid() + Path.GetExtension(updateProduct.Image.FileName);

                var path = Path.Combine(images.PhysicalPath, randomImageName);

                using var stream = new FileStream(path, FileMode.Create);

                updateProduct.Image.CopyTo(stream);



                updateProduct.ImagePath = randomImageName;

              
            }

            var product = _mapper.Map<Product>(updateProduct);

            _context.Products.Update(product);

            _context.SaveChanges();



            //BİR SAYFADAN DİĞER SAYFAYA VERİ TAŞIYACAĞIM İÇİN TEMPDATA KULLANDIM
            TempData["status"] = "Ürün başarıyla güncellendi :)"; // bunu index.cshtml de div ile göster

            var categories = _context.Category.ToList();//3

            ViewBag.categorySelect = new SelectList(categories, "Id", "Name", updateProduct.CategoryId);//3

            return RedirectToAction("Index");
        }


        [AcceptVerbs("GET", "POST")] //HEM GET HEM POST OLABİLİR ATTRİBUTÜ
        public IActionResult HasProductName(string Name)
        {
            var anyProduct = _context.Products.Any(x => x.Name.ToLower() == Name.ToLower());
            if(anyProduct)
            {
                return Json("Kaydetmeye çalıştığınız ürün ismi veri tabanında bulunmaktadır");
            }
            else
            {
                return Json(true);
            }
        }

    }
}
