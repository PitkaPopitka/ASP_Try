using WebApplication3.Models;

namespace WebApplication3.ViewModels
{
    public class GoodsListViewModel
    {
        public IEnumerable<Goods> getGoods { get; set; }
        public string currentCategory { get; set; }
    }
}
