namespace Notiflow.Panel.Infrastructure.TagHelpers;

[HtmlTargetElement("form-action-buttons")]
public sealed class FormActionButtonsTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        StringBuilder builder = new();
        builder.AppendLine("<div class='card-footer d-flex justify-content-end py-6 px-9'>");
        builder.AppendLine("<button type='reset' class='btn btn-white btn-active-light-primary me-2'>Vazgeç</button>");
        builder.AppendLine("<button type='submit' class='btn btn-primary'>Kaydet</button>");
        builder.AppendLine("</div>");

        output.TagMode = TagMode.StartTagAndEndTag;
        output.Content.SetHtmlContent(builder.ToString());
    }
}
