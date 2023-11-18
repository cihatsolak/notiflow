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
        return View(PrepareTextMessageInput());
    }

    [HttpPost]
    public IActionResult Send(TextMessageInput input)
    {
        if (!ModelState.IsValid)
        {
            return View(PrepareTextMessageInput(input));
        }

        return View();
    }

    private TextMessageInput PrepareTextMessageInput(TextMessageInput input = null)
    {
        if (input == null)
        {
            input = new();
        }

        for (int i = 0; i < 5; i++)
        {
            input.AvailableCustomers.Add(new SelectListItem
            {
                Text = $"Cihat Solak {i}",
                Value = i.ToString(),
                Selected = input.SelectedCustomerIds.Exists(selectedCustomerId => selectedCustomerId == i)
            });
        }

        return input;
    }
}
