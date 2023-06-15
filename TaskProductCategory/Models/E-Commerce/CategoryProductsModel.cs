namespace TaskProductCategory.Models.E_Commerce
{
    public class CategoryProductsModel
    {
        
        public int CategoryId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public string ProductDescription { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }


    }
}
