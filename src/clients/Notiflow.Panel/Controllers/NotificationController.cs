namespace Notiflow.Panel.Controllers;

public sealed class NotificationController(IRestService restService) : BaseController
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
    public async Task<IActionResult> Send(NotificationInput notificationInput, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(notificationInput);
        }

        var result = await restService.PostResponseAsync<Response>(NOTIFLOW_API, "/backoffice-service/notifications/send", notificationInput, cancellationToken);
        if (result.IsFailure)
        {
            return View(notificationInput);
        }

        return RedirectToAction(nameof(Send));
    }
}
