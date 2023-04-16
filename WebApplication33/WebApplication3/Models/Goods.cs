namespace WebApplication3.Models
{
    public class Goods
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public bool InStorage { get; set; }
        public int CategoryId { get; set; }
        public virtual Categories Category { get; set; }
    }
}
