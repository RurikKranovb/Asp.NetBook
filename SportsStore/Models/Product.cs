using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models
{
    public class Product
    {
        public int? ProductId { get; set; }

        [Required(ErrorMessage = "Please enter a product name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Please enter a product description")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Please enter a product price")]
        [Range(0.01, double.MaxValue)]
        public decimal? Price { get; set; }

        [Required(ErrorMessage = "Please enter a product category")]
        public string? Category { get; set; }

    }
}
