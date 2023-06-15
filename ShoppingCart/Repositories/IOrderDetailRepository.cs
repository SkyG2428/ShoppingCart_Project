using ShoppingCart.Models;

namespace ShoppingCart.Repositories
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        void Update(OrderDetail orderDetail);
    }
}
