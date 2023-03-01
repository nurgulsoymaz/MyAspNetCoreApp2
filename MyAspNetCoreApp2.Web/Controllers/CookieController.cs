using Microsoft.AspNetCore.Mvc;

namespace MyAspNetCoreApp2.Web.Controllers
{
    public class CookieController : Controller
    {
        public IActionResult CookieCreate()
            //response ile cookie kaydedildi
        {
            HttpContext.Response.Cookies.Append("background-color", "red", new CookieOptions() //cookie ekledim veiçinde bir değer tuttum("background-color", "red")
            {
                Expires = DateTime.Now.AddDays(2) //cookienin süresini 2 gün yaptım
            });

            return View();
        }

        //request ile cookie okundu
        public IActionResult CookieRead()
        {
           var bgcolor =  HttpContext.Request.Cookies["background-color"];

            ViewBag.bgColor = bgcolor;

            return View();
        }
    }
}
