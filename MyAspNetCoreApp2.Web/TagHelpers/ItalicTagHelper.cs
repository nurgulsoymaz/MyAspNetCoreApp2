using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MyAspNetCoreApp2.Web.TagHelpers
{
    public class ItalicTagHelper : TagHelper
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            //<i>nurgül</i>
            output.PreContent.SetHtmlContent("<i>");
            output.PostContent.SetHtmlContent("</li>");
        }
    }
}
