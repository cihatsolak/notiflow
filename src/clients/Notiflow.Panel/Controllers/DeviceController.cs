namespace Notiflow.Panel.Controllers;

public sealed class DeviceController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}
