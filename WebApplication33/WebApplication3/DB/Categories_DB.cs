using WebApplication3.DB_Settings;
using WebApplication3.Interfaces;
using WebApplication3.Models;

namespace WebApplication3.DB
{
    public class Categories_DB : IGoodsCategories
    {
        private readonly DB_Content db_content;

        public Categories_DB(DB_Content db_content)
        {
            this.db_content = db_content;
        }

        public IEnumerable<Categories> Categories => db_content.Categories;
    }
}
