using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    public class AuthController : BaseController
    {
        [Authorize()]
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel obj)
        {
            int ret = await provider.Member.Add(obj);
            if (ret > 0)
            {
                return Redirect("/auth/login");
            }
            return Redirect("/error");
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/auth/login");
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel obj)
        {
            ResponseLogin response = await provider.Member.Login(obj);

            if (response != null)
            {
                Member member = response.Member;
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, obj.Usr),
                    new Claim(ClaimTypes.Role, member.Role),
                    new Claim(ClaimTypes.Email, member.Email),
                    new Claim(ClaimTypes.NameIdentifier, member.Id),
                    new Claim("JwtToken", response.Token)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                ClaimsPrincipal principal = new ClaimsPrincipal(claimsIdentity);
                AuthenticationProperties properties = new AuthenticationProperties
                {
                    IsPersistent = obj.Rem
                };
                await HttpContext.SignInAsync(principal, properties);
                string token = Helper.RandomString(32);
                HttpContext.Response.Cookies.Append("AccessToken", token, new CookieOptions { Path = "/", Expires = DateTime.Now.AddDays(30) });
                member.AccessToken = token;
                provider.Member.UpdateToken(member);
                return Redirect($"/home/{member.Role}");
            }
            return View(obj);            
        }
    }
}
