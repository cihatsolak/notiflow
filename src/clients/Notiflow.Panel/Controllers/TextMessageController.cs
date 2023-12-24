namespace Notiflow.Panel.Controllers;

public sealed class TextMessageController(IRestService restService) : BaseController
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
    public async Task<IActionResult> Send(TextMessageInput textMessageInput, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(textMessageInput);
        }

        var result = await restService.PostResponseAsync<Response>(NOTIFLOW_API, "/backoffice-service/text-messages/send", textMessageInput, cancellationToken);
        if (result.IsFailure)
        {
            return View(textMessageInput);
        }

        return RedirectToAction(nameof(Send));
    }
}
