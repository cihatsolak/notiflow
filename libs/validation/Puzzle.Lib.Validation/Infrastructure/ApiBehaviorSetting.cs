namespace Puzzle.Lib.Validation.Infrastructure;

/// <summary>
/// Represents the settings for API behavior.
/// </summary>
public sealed record ApiBehaviorSetting
{
    /// <summary>
    /// Gets or sets the error message.
    /// </summary>
    public string ErrorMessage { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the application is running in a production environment.
    /// </summary>
    public bool IsProductionEnvironment { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the binding sources for action parameters should be inferred automatically.
    /// If set to false, ASP.NET Core will automatically infer the binding source for parameters (e.g., from query string, route, or body).
    /// If set to true, you must explicitly specify the binding source using attributes like [FromQuery] or [FromBody].
    /// </summary>
    public bool SuppressInferBindingSourcesForParameters { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether ASP.NET Core should automatically validate the model state.
    /// If set to false, the framework will automatically return a 400 Bad Request response if the model state is invalid.
    /// If set to true, you must manually check ModelState and handle invalid models in your code.
    /// </summary>
    public bool SuppressModelStateInvalidFilter { get; set; }
}
