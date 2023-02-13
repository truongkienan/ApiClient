using Microsoft.AspNetCore.Mvc;
using WebApp.Controllers;
using WebApp.Models;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class AuthController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
