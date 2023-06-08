using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication3.Models
{
    public class User 
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public List<Order> Orders { get; set; }
        public User() 
        {
            Orders = new List<Order>();
        }
    }

    public class Role 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; }
        public Role() 
        {
            Users = new List<User>();
        }
    }

    public class Order 
    {
        public int Id { get; set; }
        public int GoodsId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
