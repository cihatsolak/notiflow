namespace Notiflow.Panel.Controllers;

public sealed class TextMessageController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Send()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Send(TextMessageInput input)
    {
        if (!ModelState.IsValid)
        {
            return View(input);
        }

        return View();
    }
}
