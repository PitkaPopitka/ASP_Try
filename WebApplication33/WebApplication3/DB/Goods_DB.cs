using Microsoft.EntityFrameworkCore;
using WebApplication3.DB_Settings;
using WebApplication3.Interfaces;
using WebApplication3.Models;

namespace WebApplication3.DB
{
    public class Goods_DB : IGoods
    {
        private readonly DB_Content db_content;

        public Goods_DB(DB_Content db_content) 
        {
            this.db_content = db_content;
        }

        public IEnumerable<Goods> Goods => db_content.Goods.Include(c => c.Category);

        public IEnumerable<Goods> GoodsInStorage => db_content.Goods.Where(s => s.InStorage).Include(c => c.Category);

        public Goods getGoods(int GoodsId) => db_content.Goods.FirstOrDefault(p => p.Id == GoodsId);
    }
}
