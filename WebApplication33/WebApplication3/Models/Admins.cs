using Microsoft.AspNet.Identity.EntityFramework;

namespace WebApplication3.Models
{
    public class Admins 
    {
        public int Id { get; set; }
        public string AdminName { get; set; }
        public string Password { get; set; }
    }
}
