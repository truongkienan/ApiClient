using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class JourneyController : BaseController
    {
        public async Task< IActionResult> Index()
        {
            ViewBag.statuses = new SelectList(await provider.Status.GetStatuses(),"StatusId", "StatusName");
            return View(provider.Journey.GetJourneys());
        }
        [HttpPost]
        public IActionResult Detail(int id)
        {
            return Json(provider.Journey.GetJourneyById(id));
        }
        public IActionResult Edit(Journey obj)
        {
            provider.Journey.UpdateStatus(obj);
            return Redirect("/journey");
        }
    }
}
