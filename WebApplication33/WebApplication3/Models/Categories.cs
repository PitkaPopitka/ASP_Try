namespace WebApplication3.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Goods> Goods { get; set; }
    }
}
