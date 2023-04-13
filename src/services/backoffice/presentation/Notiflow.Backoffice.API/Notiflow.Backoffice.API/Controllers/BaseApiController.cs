namespace Notiflow.Backoffice.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private ISender _sender = null!;
        protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
    }
}
