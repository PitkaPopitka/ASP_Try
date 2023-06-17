using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using WebApplication3.DB;
using WebApplication3.DB_Settings;
using WebApplication3.Models;
using WebApplication3.ViewModels;

namespace WebApplication3.Controllers
{
    public class AdminController : Controller
    {
        private readonly DB_Content _gds, context;
        private static Dictionary<string, Categories> Ctgrs;

        public AdminController(DB_Content gds, DB_Content _context) 
        {
            _gds = gds;
            context = _context;
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

        public IActionResult Index()
        {
            try
            {
                if(User.IsInRole("Admin") || User.IsInRole("Moderator"))
                {
                    ViewBag.Title = User.FindFirstValue(ClaimTypes.Role);
                    return View();
                }
                return RedirectToAction("AccessDenied", "Exception");
            }
            catch (Exception)
            {
                return RedirectToAction("Exception", "Exception");
            }
        }

        
        [HttpPost]
        public IActionResult AddItem(AddItemViewModel model) 
        {
            try
            {
                if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
                {
                    string ctgr = $"SELECT Id FROM Categories WHERE Name LIKE {model.Category}";
                    var ctgrId = _gds.Categories.FromSqlRaw(ctgr).ToString();
                    string ctgr2 = $"SELECT Name FROM Categories WHERE Name LIKE {model.Category}";
                    var ctgrName = _gds.Categories.FirstOrDefault(c => c.Name == model.Category);
                    if (ctgrId == null) 
                    {
                        var newCtgr = new Categories
                        {
                            Name = ctgr2
                        };
                    }
                    var goods = new Goods
                    {
                        Name = model.Name,
                        Description = model.Description,
                        ImageUrl = model.ImageUrl,
                        Price = model.Price,
                        InStorage = model.InStorage,
                        CategoryId = ctgrId[0],
                        Category = ctgrName
                    };
                    if (goods == null)
                    {
                        return RedirectToAction("AccessDenied", "Exception");
                    }
                    _gds.Goods.Add(goods);
                    _gds.SaveChanges();
                    return View("~/Views/Admin/Index.cshtml");
                }
                return RedirectToAction("AccessDenied", "Exception");
            }
            catch (Exception)
            {
                return RedirectToAction("Exception", "Exception");
            }
        }

        public IActionResult GoodsList() 
        {
            try
            {
                if (User.IsInRole("Admin") || User.IsInRole("Moderator"))
                {
                    var lst = from g in _gds.Goods
                          where g.InStorage == true
                          group g by g.Name into grp
                          select new { Name = grp.Key, Price = grp.Select(g => g.Price), Count = grp.Count() };
                    ViewBag.Title = "Goods List";
                    return View(lst.ToList());
                }
                return RedirectToAction("AccessDenied", "Exception");
            }
            catch (Exception)
            {
                return RedirectToAction("Exception", "Exception");
            }
        }

        public IActionResult NewModer() 
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddModer(RegisterViewModel model) 
        {
            try
            {
                if (User.IsInRole("Admin"))
                {
                    User user = await context.Users.FirstOrDefaultAsync(u => u.Username == model.Username);
                    if (user == null)
                    {
                        user = new User { Username = model.Username, Password = model.ConfirmPassword, Email = model.Email };
                        Role userRole = await context.Roles.FirstOrDefaultAsync(r => r.Name == "Moderator");
                        if (userRole != null)
                        {
                            user.Role = userRole;
                        }
                        context.Users.Add(user);
                        await context.SaveChangesAsync();
                        return RedirectToAction("NewModer", "Admin");
                    }
                    else
                    {
                        return RedirectToAction("NewModer", "Admin");
                    }
                }
                return RedirectToAction("AccessDenied", "Exception");
            }
            catch (Exception)
            {
                return RedirectToAction("Exception", "Exception");
            }
        }
    }
}
