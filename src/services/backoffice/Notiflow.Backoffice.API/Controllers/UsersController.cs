using Notiflow.Lib.Cache.Services;
using Notiflow.Lib.Database.Abstract;

namespace Notiflow.Backoffice.API.Controllers
{
    public sealed class UsersController : BaseApiController
    {
        private readonly IRedisService _redisService;
        private readonly IEfEntityRepository<> efEntityRepository;

        [HttpGet]
        public IActionResult Test()
        {

            efEntityRepository.GetAsync()
            return Ok();
        }
    }
}
