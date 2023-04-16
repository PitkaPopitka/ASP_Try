using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult AboutIndex()
        {
            ViewBag.Title = "About";
            return View();
        }
    }
}
