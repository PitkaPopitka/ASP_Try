using Microsoft.AspNetCore.Mvc;
using WebApplication3.DB_Settings;
using WebApplication3.Interfaces;
using WebApplication3.ViewModels;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;
using Microsoft.IdentityModel.Tokens;
using System.Linq;
using Microsoft.AspNet.Identity;
using System.Security.Claims;

namespace WebApplication3.Controllers
{
    public class GoodsController : Controller
    {
        private readonly IGoods allGoods;
        private readonly IGoodsCategories allCategories;
        private readonly DB_Content _gds;

        public GoodsController(IGoods Igoods, IGoodsCategories IgoodsCategories, DB_Content gds) 
        {
            allGoods = Igoods;
            allCategories = IgoodsCategories;
            _gds = gds;
        }

        public IActionResult GoodsList() 
        {
            try
            {
                var gdsList = _gds.Goods.Select(g => new {
                    Name = g.Name,
                    Description = g.Description,
                    ImageUrl = g.ImageUrl,
                    Price = g.Price,
                    CategoryId = g.CategoryId,
                    InStorage = g.InStorage
                }).Where(g => g.InStorage).Distinct().ToList();
                ViewBag.Title = "ASP shop";
                if (gdsList.IsNullOrEmpty())
                {
                    return View("~/Views/Shared/ExceptionPage.cshtml");
                }
                return View(gdsList);
            }
            catch (Exception)
            {
                return RedirectToAction("Exception", "Exception");
            }
        }

        public IActionResult CurrentItem(string name) 
        {
            /*string query = $"SELECT * FROM Goods WHERE Name = '{name}' ";
            var currentGds = _gds.Goods.FromSqlRaw(query).ToList();
            if (currentGds.IsNullOrEmpty())
            {
                return RedirectToAction("Exception", "Exception");
            }
            ViewBag.Title = currentGds[0].Name;
            return View(currentGds[0]);*/
            return RedirectToAction("InProgress", "Exception");
        }

        public IActionResult CartAdd(string name) 
        {
            return RedirectToAction("InProgress", "Exception");
        }
    }
}
