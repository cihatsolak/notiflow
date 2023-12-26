namespace Notiflow.Panel.Controllers;

public sealed class EmailController(IRestService restService) : BaseController
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
    public async Task<IActionResult> Send(EmailInput input, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(input);
        }

        var result = await restService.PostResponseAsync<Response>(NOTIFLOW_API, "/backoffice-service/emails/send", input, cancellationToken);
        if (result.IsFailure)
        {
            return View(input);
        }

        return RedirectToAction(nameof(Send));
    }
}
