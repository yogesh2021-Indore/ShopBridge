using System.ComponentModel.DataAnnotations;

namespace ShopBridge.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Please enter name of the product")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Please enter product Description")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Please enter product price")]
        public decimal Price { get; set; }
        public int? NumberOfItems { get; set; }
    }
}
