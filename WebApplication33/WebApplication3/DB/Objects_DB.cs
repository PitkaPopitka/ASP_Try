using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using WebApplication3.DB_Settings;
using WebApplication3.Models;

namespace WebApplication3.DB
{
    public class Objects_DB
    {
        public static void Initial(DB_Content content) 
        {
            if (!content.Categories.Any())
            {
                content.AddRange
                    (
                        new Categories 
                        { 
                            Name = "Мониторы",
                        },
                        new Categories 
                        { 
                            Name = "Накопители"
                        }
                    );
            }

            if (!content.Goods.Any())
            {
                content.AddRange(
                    new Goods
                    {
                        Name = "MSI Pro MP241X",
                        Description = "1920x1080 (FullHD)@75 Гц, VA, 3000 : 1, 250 Кд/м², 178°/178°, HDMI, VGA (D-Sub)",
                        ImageUrl = "/img/mon1.jpg",
                        Price = 8999,
                        InStorage = true,
                        Category = Ctgrs["Мониторы"],
                        CategoryId = 1
                    },

                    new Goods
                    {
                        Name = "Samsung 970 EVO Plus",
                        Description = "PCI-E 3.x x4, чтение - 3500 Мбайт/сек, запись - 3300 Мбайт/сек, 3 бит MLC (TLC), NVM Express",
                        ImageUrl = "/img/ssd1.jpg",
                        Price = 9499,
                        InStorage = false,
                        Category = Ctgrs["Накопители"],
                        CategoryId = 2
                    }
                );
            }
            content.SaveChanges();
        }

        private static Dictionary<string, Categories> category;
        public static Dictionary<string, Categories> Ctgrs
        {
            get
            {
                if (category == null)
                {
                    var categoriesList = new Categories[]
                    {
                        new Categories { Name = "Мониторы" },
                        new Categories { Name = "Накопители"}
                    };
                    category = new Dictionary<string, Categories>();
                    foreach (Categories i in categoriesList)
                    {
                        category.Add(i.Name, i);
                    }
                }
                return category;
            }
        }
    }
}
