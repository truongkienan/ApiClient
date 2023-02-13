using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Distance(string addrSrc, string addrDes)
        {
            using (HttpClient client =new HttpClient())
            {
                HttpResponseMessage message = await client.GetAsync($"https://api.mapbox.com/directions/v5/mapbox/driving/{addrSrc};{addrDes}.json?access_token=pk.eyJ1IjoiMjA4ODAyNzgiLCJhIjoiY2w4bzBvZWQ1MGlpcDNvcGJwMWcya3RzdiJ9.3lsgrTQgHB13Jb-00-d5WQ");
                if (message.IsSuccessStatusCode)
                {
                    return Json(await message.Content.ReadAsStringAsync());
                }
                return Json(null);
            }
        }
        public IActionResult GetCustomers(string id)
        {
            return Json(provider.Member.GetCustomerByDriver(id));
        }

        [Authorize(Roles="driver")]
        public IActionResult GetDrivers()
        {
            return Json(provider.Member.GetDrivers());
        }
        [Authorize(Roles = "customer")]
        public IActionResult Customer()
        {
            Member member = provider.Member.GetDriverByCustomer(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (member!=null)
            {
                return View(member);
            }
            return Redirect("/");
        }
        public IActionResult SetLocation(Member obj)
        {
            return Json(provider.Member.UpdateLocation(obj));
        }
        [Authorize(Roles ="admin")]
        public IActionResult Admin()
        {            
            List<Member> list = provider.Member.GetDrivers();
            foreach(var item in list)
            {
                item.Customers = provider.Member.GetCustomerByDriver(item.Id);
            }
            return View(list);
        }
        public IActionResult Driver()
        {
            //2 cach load Map
            //C1 C#
            //C2 ajax
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}