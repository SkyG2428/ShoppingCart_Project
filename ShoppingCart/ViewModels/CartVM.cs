using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using ShoppingCart.Models;

namespace ShoppingCart.ViewModels
{
    public class CartVM
    {
        //public int Id { get; set; }
        //public int ProductId { get; set; }
        //[ValidateNever]
        //public Product Product { get; set; }
        //[ValidateNever]
        //public string ApplicationUserId { get; set; }
        //public ApplicationUser ApplicationUser { get; set; }
        //public int Count { get; set; }

        public IEnumerable<Cart> Cart { get; set; } = new List<Cart>();

        public OrderHeader OrderHeader { get; set; }= new OrderHeader();

    }
}
