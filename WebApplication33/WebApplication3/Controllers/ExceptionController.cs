using Microsoft.AspNetCore.Mvc;

namespace WebApplication3.Controllers
{
    public class ExceptionController : Controller
    {
        public IActionResult Exception()
        {
            return View();
        }

        public IActionResult InProgress() 
        {
            return View();
        }
    }
}
