using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Controllers;

namespace WebApp.Areas.Dashboard.Controllers
{
    [Area("dashboard")]
    public class MemberController : BaseController
    {
        [Authorize]
        public async Task<IActionResult> Index()
        {
            string token = User.FindFirstValue("JwtToken");
            return View(await provider.Member.GetMembers(token));
        }
    }
}
