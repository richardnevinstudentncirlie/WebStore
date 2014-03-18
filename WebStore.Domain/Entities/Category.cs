using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace WebStore.Domain.Entities
{

    public class Category
    {

        [HiddenInput(DisplayValue = false)]
        public int CategoryID { get; set; }

        [MaxLength(50), Required(ErrorMessage = "Please specify a category")]
        public string CategoryCode { get; set; }

        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedAt { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime UpdatedAt { get; set; }

    }
}
