namespace Notiflow.Panel.Controllers;

public sealed class EmailController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}
