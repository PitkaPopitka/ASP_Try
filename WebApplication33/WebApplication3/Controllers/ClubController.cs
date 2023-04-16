using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
    public class ClubController : Controller
    {
        public IActionResult ClubIndex()
        {
            ViewBag.Title = "Club";
            return View();
        }
    }
}
