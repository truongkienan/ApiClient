using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class UploadController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(IFormFile f, Info obj)
        {
            if (ModelState.IsValid)
            {
                obj.FileName = await provider.Upload.Upload(f, obj);
                return View(obj);
            }
            ModelState.AddModelError("msg", "Please enter data valid");
            return View(obj);
        }
    }
}
