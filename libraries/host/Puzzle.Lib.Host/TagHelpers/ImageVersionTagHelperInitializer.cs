namespace Puzzle.Lib.Host.TagHelpers
{
    /// <summary>
    /// Initializes the ImageTagHelper to append version to the image source URL.
    /// </summary>
    public sealed class ImageVersionTagHelperInitializer : ITagHelperInitializer<ImageTagHelper>
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

}
