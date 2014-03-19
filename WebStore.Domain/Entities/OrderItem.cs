using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace WebStore.Domain.Entities
{

    public class OrderItem
    {

        [HiddenInput(DisplayValue = false)]
        public int OrderItemID { get; set; }


        [MaxLength(50)]
        [Required(ErrorMessage = "Please enter a product name")]
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal Price { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Please specify a category")]
        public string Category { get; set; }

        [MaxLength(255)]
        [Required(ErrorMessage = "Please specify image location")]
        public string ImageURL { get; set; }

        [Required(ErrorMessage = "Please specify if a special")]
        public bool Special { get; set; }
        
        [HiddenInput(DisplayValue = false)]
        public int Seller { get; set; }
 
        [HiddenInput(DisplayValue = false)]
        public int Buyer { get; set; }
        
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public int Quantity { get; set; }

        
        
        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedAt { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime UpdatedAt { get; set; }

           
        public virtual Order OrderItemOrder { get; set; }
        public virtual Product OrderItemProduct { get; set; }

    }    
}
