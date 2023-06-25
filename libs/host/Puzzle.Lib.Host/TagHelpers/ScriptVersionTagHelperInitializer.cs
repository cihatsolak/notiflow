namespace Puzzle.Lib.Host.TagHelpers;

/// <summary>
/// Implements the ITagHelperInitializer interface for the ScriptTagHelper to set the AppendVersion property to true.
/// </summary>
public sealed class ScriptVersionTagHelperInitializer : ITagHelperInitializer<ScriptTagHelper>
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
