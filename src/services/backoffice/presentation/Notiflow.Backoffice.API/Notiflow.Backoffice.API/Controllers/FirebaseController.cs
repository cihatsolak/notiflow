namespace Notiflow.Backoffice.API.Controllers
{
    public sealed class FirebaseController : BaseApiController
    {
        [ProducesResponseType(200)]
        [ProducesResponseType(400, Type = typeof(HttpValidationProblemDetails))]
        [HttpGet]
        public IActionResult Send()
        {
            
            return Ok();
        }
    }
}
