using ShoppingCart.Data;
using ShoppingCart.Models;

namespace ShoppingCart.Repositories
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public int DecrementCount(Cart cart, int count)
        {
            cart.Count -= count;
            return cart.Count;
        }

        public int IncrementCount(Cart cart, int count)
        {
            cart.Count += count;
            return cart.Count;
        }
    }
}
