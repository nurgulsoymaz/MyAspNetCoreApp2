using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MyAspNetCoreApp2.Web.Filters;
using MyAspNetCoreApp2.Web.Models;
using MyAspNetCoreApp2.Web.ViewModels;
using System.Diagnostics;


namespace MyAspNetCoreApp2.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AppDbContext _context;

        private readonly IMapper _mapper;
       //private readonly IFileProvider _fileProvider;




        //public HomeController(AppDbContext context)
        //{
        //    _context = context;
        //}

        public HomeController(ILogger<HomeController> logger, AppDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
           //_fileProvider = fileProvider;

        }

        public IActionResult Index()
        {
            var products = _context.Products.OrderByDescending(x => x.Id).Select(x => new ProductPartialViewModel()
            {
                Id = x.Id,
                Name = x.Name,
                Price = x.Price,
                Stock = x.Stock,
                //File= x.File,
               

                

            }).ToList();

            ViewBag.productListPartialViewModel = new ProductListPartialViewModel()
            {
                Products = products //ProductListPartialViewModeldeki dataları çektim
            };


            return View();
        }
        //[CustomExceptionFilter]
        public IActionResult Privacy()
        {

            //throw new Exception("veritanı ile ilgili bir hata meydana geldi");



            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel errorViewModel)
        {
            errorViewModel.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

            return View(errorViewModel);
        }


        public IActionResult Visitor()
        {

            return View();
        }


        [HttpPost]

        public IActionResult SaveVisitorComment(VisitorViewModel visitorViewModel)
        {
            try
            {
                var visitor = _mapper.Map<Visitor>(visitorViewModel);
                visitor.Created = DateTime.Now;
                _context.Visitors.Add(visitor);
                _context.SaveChanges();

                TempData["result"] = "Yorum kaydedilmiştir";

                //tip güvenli dönmek istersen
                return RedirectToAction(nameof(HomeController.Visitor));

            }
            catch (Exception)
            {
                TempData["result"] = "Yorum kaydedilirken bir hata oluştu";

                //tip güvenli dönmek istersen
                return RedirectToAction(nameof(HomeController.Visitor));
         
            }
           

            //var name = HttpContext.Request.Form["Name"].ToString();
            //var comment = HttpContext.Request.Form["Comment"].ToString();
            //var created = DateTime.Parse(HttpContext.Request.Form["Created"].ToString());

            //  Visitor newVisitor =new Visitor() { Name = name, Comment = comment, Created=created };
            //  _context.Visitors.Add(newVisitor);
            //  _context.SaveChanges();




        }

    }
}