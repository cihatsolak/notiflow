namespace Notiflow.Panel.Infrastructure.TagHelpers;

[HtmlTargetElement("page-title")]
public sealed class PageTitleTagHelper : TagHelper
{
    private const string PAGE_TITLE = "Title";

    [ViewContext]
    public ViewContext ViewContext { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagMode = TagMode.StartTagAndEndTag;
        output.TagName = null;
        output.Content.SetHtmlContent($"<div class='card-title fs-3 fw-bolder'>{ViewContext.ViewData[PAGE_TITLE]}</div>");
    }
}
