using IanByrne.ResearchProject.Shared.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.IO;
using System.Reflection;
using System.Text.Encodings.Web;

namespace IanByrne.ResearchProject.WebApp.Pages
{
    /// <summary>
    /// https://stackoverflow.com/questions/36806630/mvc-6-net-core1-create-custom-tag-helper-for-likert-scale
    /// </summary>
    [HtmlTargetElement("input", Attributes = LikertForAttributeName)]
    public class LikertTagHelper : TagHelper
    {
        private const string LikertForAttributeName = "likert-for";

        [HtmlAttributeName(LikertForAttributeName)]
        public ModelExpression ModelField { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var outerTag = new TagBuilder("div");
            output.MergeAttributes(outerTag);
            output.TagName = outerTag.TagName;
            output.TagMode = TagMode.StartTagAndEndTag;

            // Get Likert Type from attributes
            var likertLabelsAttribute = ModelField
                .Metadata
                .ContainerType
                .GetProperty(ModelField.Name)
                .GetCustomAttribute(typeof(LikertLabelsAttribute)) as LikertLabelsAttribute;

            if (likertLabelsAttribute != null)
            {
                output.PreContent.SetHtmlContent(likertLabelsAttribute.PreContent);
                output.PostContent.SetHtmlContent(likertLabelsAttribute.PostContent);
            }

            // Add the radio buttons
            for (var x = 1; x <= 7; x++)
            {
                var input = new TagBuilder("input");
                input.MergeAttribute("type", "radio");
                input.MergeAttribute("name", ModelField.Name);
                input.MergeAttribute("value", x.ToString());
                input.TagRenderMode = TagRenderMode.SelfClosing;
                output.Content.AppendHtml(input);
            }

            base.Process(context, output);
        }
    }
}
