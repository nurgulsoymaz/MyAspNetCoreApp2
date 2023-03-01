using MyAspNetCoreApp2.Web.ViewModels;
using MyAspNetCoreApp2.Web.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MyAspNetCoreApp2.Web.TagHelpers
{
   // [HtmlTargetElement("thumbnail")]// tag helperın adını costom olarak değiştirebiliirm
    public class ImageThumbnail:TagHelper
    {

        //public object ImageSrc { get; set; }

        //public override void Process(TagHelperContext context, TagHelperOutput output)
        //{
        //    //<img/>
        //    output.TagName = "img";

        //    string fileName = ImageSrc.Split(".")[0];

        //    string fileExtensions = Path.GetExtension(ImageSrc);//.jpg verir

        //    output.Attributes.SetAttribute("src", $"{fileName}-100x100{fileExtensions}");
        //}
    }
}
