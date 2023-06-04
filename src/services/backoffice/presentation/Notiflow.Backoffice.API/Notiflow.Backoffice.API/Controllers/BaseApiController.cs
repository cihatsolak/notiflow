using Microsoft.Extensions.Localization;
using Notiflow.Backoffice.Infrastructure.Localize;

namespace Notiflow.Backoffice.API.Controllers;

[Route("api/[controller]")]
public class BaseApiController : ResultController
{
    private ISender _sender = null!;
    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();


    private IStringLocalizer<Resource> _localizer = null!;
    protected IStringLocalizer<Resource> Localizer => _localizer ??= HttpContext.RequestServices.GetRequiredService<IStringLocalizer<Resource>>();

    
    protected override IActionResult CreateGetResultInstance<T>(Response<T> response)
    {
        response.Message = Localizer[response.Code.ToString()];

        return base.CreateGetResultInstance(response);
    }
}