using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;
using System.Security.Claims;
using WebApplication3.DB_Settings;
using WebApplication3.Models;
using WebApplication3.ViewModels;

namespace WebApplication3.Controllers
{
    public class AccountController : Controller
    {
        private readonly DB_Content context;
        public AccountController(DB_Content _context) 
        {
            context = _context;
        }

        public IActionResult LoginPage()
        {
            ViewBag.Title = "Login";
            return View();
        }

        public IActionResult RegisterPage()
        {
            ViewBag.Title = "Register";
            return View();
        }

        private async Task Authenticate(User user) 
        {
            var claims = new List<Claim> 
            { 
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Role.Name)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultNameClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        private async Task signOut() 
        {
            await HttpContext.SignOutAsync();
        }

        public async Task<IActionResult> SignOut() 
        {
            await signOut();
            return RedirectToAction("GoodsList", "Goods");
        }

        [HttpPost]
        public async Task<IActionResult> Register(LoginViewModel model) 
        {
            try
            {
                User user = await context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
                if (user == null) 
                {
                    user = new User { Username = model.Username, Password = model.Password };
                    Role userRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "User");
                    if (userRole != null) 
                    {
                        user.Role = userRole;
                    }
                    context.Users.Add(user);
                    await context.SaveChangesAsync();
                    await Authenticate(user);
                    return RedirectToAction("GoodsList", "Goods");
                }
                else
                {
                    return RedirectToAction("LoginPage", "Account");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Exception", "Exception");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            try
            {
                User? user = await context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Username == model.Username && u.Password == model.Password);
                if (user != null)
                {
                    await Authenticate(user);
                    return RedirectToAction("GoodsList", "Goods");
                }
                return RedirectToAction("LoginPage", "Account");
            }
            catch (Exception)
            {
                return RedirectToAction("Exception", "Exception");
            } 
        }
    }
}
