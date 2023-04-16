using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
    public class PrivacyController : Controller
    {
        public IActionResult PrivacyIndex()
        {
            ViewBag.Title = "Privacy";
            return View();
        }
    }
}
