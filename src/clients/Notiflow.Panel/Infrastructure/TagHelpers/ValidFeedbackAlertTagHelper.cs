namespace Notiflow.Panel.Infrastructure.TagHelpers;

[HtmlTargetElement("valid-feedback")]
public sealed class ValidFeedbackAlertTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagMode = TagMode.StartTagAndEndTag;
        output.TagName = null;
        output.Content.SetHtmlContent("<div class='valid-feedback'>İyi Görünüyor!</div>");
    }
}
