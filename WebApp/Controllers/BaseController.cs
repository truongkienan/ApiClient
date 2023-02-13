using Microsoft.AspNetCore.Mvc;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class BaseController : Controller
    {
        protected SiteProvider provider = new SiteProvider();
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            provider.Dispose();
        }
    }
}
