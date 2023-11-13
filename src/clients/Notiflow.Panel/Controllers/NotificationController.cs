namespace Notiflow.Panel.Controllers;

public sealed class NotificationController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}
