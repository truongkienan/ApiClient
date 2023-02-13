using Microsoft.AspNetCore.Mvc;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return View(await ProviderAliasAttribute.Journey.GetStatisticJourneys());
        }
    }
}
