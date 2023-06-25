namespace Puzzle.Lib.Host.TagHelpers;

/// <summary>
/// Implements the ITagHelperInitializer interface to initialize the LinkTagHelper with a version tag.
/// </summary>
public sealed class StyleVersionTagHelperInitializer : ITagHelperInitializer<LinkTagHelper>
{
    /// <summary>
    /// Initializes the specified LinkTagHelper with a version tag.
    /// </summary>
    /// <param name="helper">The LinkTagHelper to initialize.</param>
    /// <param name="context">The ViewContext for the current request.</param>
    public void Initialize(LinkTagHelper helper, ViewContext context)
    {
        helper.AppendVersion = true;
    }
}
