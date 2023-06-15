using ShoppingCart.Models;

namespace ShoppingCart.Repositories
{
    public interface IProductRepository :IRepository<Product>
    {
        void Update(Product product);   
    }
}
