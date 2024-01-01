namespace Puzzle.Lib.Host;

/// <summary>
/// Initializes the ImageTagHelper to append version to the image source URL.
/// </summary>
internal sealed class ImageVersionTagHelperInitializer : ITagHelperInitializer<ImageTagHelper>
{
    /// <summary>
    /// Initializes the ImageTagHelper to append version to the image source URL.
    /// </summary>
    /// <param name="helper">The ImageTagHelper to initialize.</param>
    /// <param name="context">The ViewContext.</param>
    public void Initialize(ImageTagHelper helper, ViewContext context)
    {
        helper.AppendVersion = true;
    }
}

/// <summary>
/// Implements the ITagHelperInitializer interface for the ScriptTagHelper to set the AppendVersion property to true.
/// </summary>
internal sealed class ScriptVersionTagHelperInitializer : ITagHelperInitializer<ScriptTagHelper>
{
    /// <summary>
    /// Initializes a ScriptTagHelper instance by setting the AppendVersion property to true.
    /// </summary>
    /// <param name="helper">The ScriptTagHelper instance to be initialized.</param>
    /// <param name="context">The ViewContext for the helper.</param>
    public void Initialize(ScriptTagHelper helper, ViewContext context)
    {
        helper.AppendVersion = true;
    }
}

/// <summary>
/// Implements the ITagHelperInitializer interface to initialize the LinkTagHelper with a version tag.
/// </summary>
internal sealed class StyleVersionTagHelperInitializer : ITagHelperInitializer<LinkTagHelper>
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
