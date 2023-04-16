using WebApplication3.Models;

namespace WebApplication3.Interfaces
{
    public interface IGoods
    {
        IEnumerable<Goods> Goods { get; }
        IEnumerable<Goods> GoodsInStorage { get; }
        Goods getGoods(int GoodsId);
    }
}
