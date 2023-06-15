using ShoppingCart.Models;

namespace ShoppingCart.Repositories
{
    public interface ICategoryRepository:IRepository<Category>
    {
        void Update(Category category); 
    }
}
