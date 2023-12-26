namespace Notiflow.Panel.Controllers;

public sealed class DeviceController(IRestService restService) : BaseController
{
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(DeviceInput deviceInput, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return View(deviceInput);
        }

        var response = await restService.PostResponseAsync<Response<int>>(NOTIFLOW_API, "/backoffice-service/devices/add", deviceInput, cancellationToken);
        if (response.IsFailure)
        {
            return View(deviceInput);
        }

        return RedirectToAction(nameof(Edit), new { response.Data });
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
    {
        var response = await restService.GetResponseAsync<Response<DeviceInput>>(NOTIFLOW_API, $"/backoffice-service/devices/{id}/detail", cancellationToken);
        if (response.IsFailure)
        {
            return RedirectToAction(nameof(Index));
        }

        return View(response.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(DeviceInput deviceInput, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction(nameof(Index));
        }

        var response = await restService.PutApiResponseAsync<Response>(NOTIFLOW_API, "/backoffice-service/devices/update", deviceInput, cancellationToken);
        if (response.IsFailure)
        {
            ModelState.AddModelError(string.Empty, response.Message);
        }

        return RedirectToAction(nameof(Edit), new { deviceInput.Id });
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var response = await restService.DeleteApiResponseAsync<Response>(NOTIFLOW_API, $"/backoffice-service/devices/delete/{id}", cancellationToken);
        if (response.IsFailure)
        {
            ModelState.AddModelError(string.Empty, response.Message);
        }

        return RedirectToAction(nameof(Index));
    }
}
