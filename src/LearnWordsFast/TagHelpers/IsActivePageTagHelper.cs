using System;
using System.Text;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.AspNet.Razor.Runtime.TagHelpers;

namespace LearnWordsFast.TagHelpers
{
    [TargetElement("menuitem", Attributes = AspControllerAttributeName)]
    [TargetElement("menuitem", Attributes = AspActionAttributeName)]
    [TargetElement("menuitem", Attributes = AspTitleAttributeName)]
    public class IsActivePageTagHelper : TagHelper
    {
        private readonly IUrlHelper urlHelper;
        private const string AspControllerAttributeName = "asp-controller";
        private const string AspActionAttributeName= "asp-action";
        private const string AspTitleAttributeName = "asp-title";

        [HtmlAttributeName(AspControllerAttributeName)]
        public string Controller { get; set; }

        [HtmlAttributeName(AspActionAttributeName)]
        public string Action { get; set; }

        [HtmlAttributeName(AspTitleAttributeName)]
        public string Title { get; set; }

        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public IsActivePageTagHelper(IUrlHelper urlHelper)
        {
            this.urlHelper = urlHelper;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var routeData = ViewContext.RouteData.Values;
            var currentController = routeData["controller"] as string;
            var currentAction = routeData["action"] as string;

            string href = urlHelper.Action(Action, Controller);

            output.TagName = "li";

            var a = new TagBuilder("a");
            a.MergeAttribute("href", href);
            a.MergeAttribute("title", Title);
            a.InnerHtml = Title;

            if (Controller.Equals(currentController, StringComparison.OrdinalIgnoreCase) &&
                Action.Equals(currentAction, StringComparison.OrdinalIgnoreCase))
            {

                if (output.Attributes.ContainsName("class"))
                {
                    var classAttribute = output.Attributes["class"];
                    classAttribute.Value = classAttribute.Value += " active";
                }
                else
                {
                    output.Attributes.Add(new TagHelperAttribute("class", "active"));
                }
            }

            output.Content.SetContent(a.ToString());
        }
    }
}