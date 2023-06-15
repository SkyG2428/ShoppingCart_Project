using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProductADO.Models.Products
{
    public class ProductModule
    {
        [Key]
        [DisplayName("ID")]
        public int ProductId { get; set; }
        [Required]
        [DisplayName("Product")]
        public string ProductName { get; set; }

        [DisplayName("Description")]
        public string ProductDescription { get; set; }
        [Required]
        [DisplayName("Price")]
        public decimal ProductPrice { get; set; }
        [Required]
        [DisplayName("Quantity")]
        public int ProductQuantity { get; set; }
        public string Remark { get; set; }

    }
}
