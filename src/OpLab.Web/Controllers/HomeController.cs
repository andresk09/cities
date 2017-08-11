using Microsoft.AspNetCore.Mvc;
using OpLab.API.Application;

namespace OpLab.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Error()
        {
            return View();
        }
    }
}
