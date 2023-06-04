namespace Notiflow.Backoffice.API.Controllers;

[AllowAnonymous]
public class LanguagesController : BaseApiController
{
    /// <summary>
    /// Lists information about the languages supported by the application
    /// </summary>
    /// <response code="200">Operation successful</response>
    /// <response code="401">Unauthorized action</response>
    /// <response code="404">No supported language found.</response>
    [HttpGet("supported-cultures")]
    [ProducesResponseType(typeof(Response<SupportedCulturesQueryResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Response<EmptyResponse>), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> AllLanguages()
    {
        var response = await Sender.Send(new SupportedCulturesQuery());
        return CreateGetResultInstance(response);
    }
}
