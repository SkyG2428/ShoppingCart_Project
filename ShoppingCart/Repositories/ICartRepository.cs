using ShoppingCart.Models;

namespace ShoppingCart.Repositories
{
    public interface ICartRepository : IRepository<Cart>
    {
        int IncrementCount(Cart Cart, int count);
        int DecrementCount(Cart Cart, int count);
    }
}
