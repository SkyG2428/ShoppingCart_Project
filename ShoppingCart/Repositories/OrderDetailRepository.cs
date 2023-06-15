using ShoppingCart.Data;
using ShoppingCart.Models;

namespace ShoppingCart.Repositories
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private ApplicationDbContext _context;

        public OrderDetailRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(OrderDetail orderDetail)
        {
            _context.OrderDetails.Update(orderDetail);
            //var categoryDB=_context.Categories.FirstOrDefault(x=>x.Id==);
            //if (categoryDB!=null)
            //{
            //    categoryDB.Name = category.Name;
            //    categoryDB.DisplayOrder=category.DisplayOrder;
            //}
        }
    }
}
