namespace Notiflow.Panel.Controllers;

public sealed class TextMessageController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}
