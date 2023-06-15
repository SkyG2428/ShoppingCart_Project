namespace ShoppingCart.Repositories
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }

         ICartRepository Cart { get; }
         IApplicationUserRepository ApplicationUser { get; }
          IOrderHeaderRepository OrderHeader { get; }
         IOrderDetailRepository OrderDetail { get; }

        void Save();
    }
}
