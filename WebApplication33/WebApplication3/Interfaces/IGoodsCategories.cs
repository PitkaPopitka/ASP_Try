using WebApplication3.Models;

namespace WebApplication3.Interfaces
{
    public interface IGoodsCategories
    {
        IEnumerable<Categories> Categories { get; }
    }
}
