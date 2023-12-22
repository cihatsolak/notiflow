namespace Notiflow.Panel.Controllers;

public sealed class TextMessageController(IRestService restService) : Controller
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
    public async Task<IActionResult> Send(TextMessageInput input, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(input);
        }

        await restService.PostResponseAsync<>

        return View();
    }
}
