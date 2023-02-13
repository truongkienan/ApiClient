using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using WebApp.Models;
namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class StatusController : BaseController
    {
        public async Task<IActionResult> Index()
        {
            return View(await provider.Status.GetStatuses());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Status obj)
        {
            int ret = await provider.Status.Add(obj);
            return Redirect(ret);
        }

        IActionResult Redirect(int ret)
        {
            if (ret > 0)
            {
                return Redirect("/dashboard/status");
            }
            return Redirect("/dashboard/error");
        }

        public async Task<IActionResult> Delete(byte id)
        {
            int ret = await provider.Status.Delete(id);
            return Redirect(ret);
        }

        public async Task<IActionResult> Edit(byte id)
        {
            return View(await provider.Status.GetStatusById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Status obj)
        {
            int ret = await provider.Status.Edit(obj);
            return Redirect(ret);
        }
    }
}
