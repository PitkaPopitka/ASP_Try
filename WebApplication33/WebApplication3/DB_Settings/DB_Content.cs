using Microsoft.EntityFrameworkCore;
using WebApplication3.Models;

namespace WebApplication3.DB_Settings
{
    public class DB_Content : DbContext    
    {
        
        public DbSet<Goods> Goods { get; set; }
        public DbSet<Categories> Categories { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Order> Orders { get; set; }

        public DB_Content(DbContextOptions<DB_Content> options) : base(options) 
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "Admin";
            string userRoleName = "User";
            string moderRoleName = "Moderator";
            string adminName = "firstAdmin";
            string adminPassword = "1stAdmin";
            string username = "firstUser";
            string userPassword = "1stUser";
            string moderName = "firstModer";
            string moderPassword = "1stModer";
            string adminEmail = "";
            string moderEmail = "";
            string userEmail = "";

            Role adminRole = new Role { Id = 1, Name = adminRoleName};
            Role userRole = new Role { Id = 2, Name = userRoleName};
            Role moderRole = new Role { Id = 3, Name = moderRoleName };
            User adminUser = new User { Id = 1, Username = adminName, Password = adminPassword, RoleId = adminRole.Id, Email = adminEmail };
            User user = new User { Id = 2, Username = username, Password = userPassword, RoleId = userRole.Id, Email = userEmail };
            User moder = new User { Id = 3, Username = moderName, Password = moderPassword, RoleId = moderRole.Id, Email = moderEmail };
            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, userRole, moderRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser, user, moder });

            Order firstOrder = new Order { Id = 1, UserId = 1, GoodsId = 1, OrderDate = DateTime.Now };
            modelBuilder.Entity<Order>().HasData(new Order[] {});
            base.OnModelCreating(modelBuilder);
        }
    }
}
