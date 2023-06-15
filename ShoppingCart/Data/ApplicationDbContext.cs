using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShoppingCart.Models;

namespace ShoppingCart.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser> //: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options) 
        {

        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
          public DbSet<ApplicationUser> ApplicationUsers { get; set; }   
          public DbSet<Cart> Carts { get; set; }
          public DbSet<OrderHeader> OrderHeaders { get; set; }
          //public object OrderDetails{ get; internal set; }  
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}
