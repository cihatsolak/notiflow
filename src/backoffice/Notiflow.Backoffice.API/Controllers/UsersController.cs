namespace Notiflow.Backoffice.API.Controllers
{
    public sealed class UsersController : BaseApiController
    {
        [HttpGet]
        public IActionResult Test()
        {
            return Ok();
        }
    }
}
