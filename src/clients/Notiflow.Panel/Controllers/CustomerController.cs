namespace Notiflow.Panel.Controllers;

public sealed class CustomerController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }
}
