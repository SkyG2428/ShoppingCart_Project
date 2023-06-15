using ShoppingCart.Data;
using ShoppingCart.ViewModels;
//using ShoppingCart.Models;

namespace ShoppingCart.Repositories
{
    public class UnitOfWork:IUnitOfWork
    {
        private ApplicationDbContext _context;
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set;}
        public ICartRepository Cart { get; set; }
        public IApplicationUserRepository ApplicationUser { get; set; }
            public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }
        
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category =new CategoryRepository(context);
            Product =new ProductRepository(context);
            Cart =new CartRepository(context);
            ApplicationUser =new ApplicationUserRepository(context);
            OrderDetail =new OrderDetailRepository(context);
            OrderHeader =new OrderHeaderRepository(context);
        }

        public void Save() 
        { 
            _context.SaveChanges();
        }

    }
}
