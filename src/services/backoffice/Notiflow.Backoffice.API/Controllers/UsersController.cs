using Notiflow.Lib.Cache.Services;

namespace Notiflow.Backoffice.API.Controllers
{
    public sealed class UsersController : BaseApiController
    {
        private readonly IRedisService _redisService;

        [HttpGet]
        public IActionResult Test()
        {
            
            return Ok();
        }
    }
}
