using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp2.Web.Controllers
{

    public class Product2
    {
        public int Id { get; set; }
        public string Name { get; set; }

    }
    public class OrnekController : Controller
    {
        public IActionResult Index()
        {
            var productList = new List<Product2>()
            {
                new() { Id = 1, Name = "Kalem" },
                new() { Id = 2, Name = "Defter" },
                new() { Id = 3, Name = "Silgi" }

            };
            
            return View(productList); //Eğer view dönersek bunun views katmanında cshtml karşılığı olmalı
        }
        
        public IActionResult Index2() 
        { 
        return View();
        }
        public IActionResult Index3()
        {
            return RedirectToAction("Index", "Ornek");//bir ındex2 sayfasından ındex e dönebilmemiz için redirecttoaction metodu kullanılmalı
        }
        public IActionResult ParametreView(int id) 
        {
         return RedirectToAction("JsonResultParametre", "Ornek", new { id = id });
        }
        public IActionResult JsonResultParametre(int id)
        {
            return Json(new { Id = id });//json döner
        }
        //ACTİON METOTLAR
        public IActionResult ContentResult()
        {
            return Content("ContentResult");//stirng döner
        }
       
        public IActionResult EmptyResult() 
        {
        return new EmptyResult();//boş dönüş tipi
        }

    }
}



//veri taşıma yöntemleri: controllerdan viewe veri taşıma

//1)ViewBag
//ViewBag.name = new List<string>() { "ahmet", "mehmet", "hasan" };
//ViewBag.name = "Asp.Net Core";
//ViewBag.person = new { Id = 1, Name = "ahmet", Age = 23 };

//2)ViewData
//ViewData["Age"] = 30;
//ViewData["names"] =new List<string>() { "ahmet", "mehmet", "hasan" };


//3)TempData : bir action metottan diğer bir action metoda veri taşır.
//TempData["surname"] = "soymaz"; //bunu index2.cshtm e de ekle

//4)ViewModel: Bir tabloyu doldurabileck kadar hacimli datalar taşınır