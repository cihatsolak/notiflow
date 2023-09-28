namespace Notiflow.Backoffice.API.Controllers;

[AllowAnonymous]
public class LanguagesController : BaseApiController
{
    /// <summary>
    /// Retrieves the list of supported cultures for languages.
    /// </summary>
    /// <returns>The response containing the list of supported cultures.</returns>
    /// <response code="200">Operation successful</response>
    /// <response code="401">Unauthorized action</response>
    /// <response code="404">No supported language found.</response>
    [HttpGet("supported-cultures")]
    [ProducesResponseType(typeof(ApiResponse<SupportedCulturesQueryResult>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<EmptyResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AllLanguages()
    {
        var response = await Sender.Send(new SupportedCulturesQuery());
        return HttpResult.Get(response);
    }
}
