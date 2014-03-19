using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace WebStore.Domain.Entities
{

    public class Order
    {
        [HiddenInput(DisplayValue = false)]
        public int OrderID { get; set; }


        [Required(ErrorMessage = "Please specify an order date")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "Please specify a shipping date")]
        public DateTime ShipDate { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal TotalOrder { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a positive number")]
        public int ProductCount { get; set; }


        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedAt { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime UpdatedAt { get; set; }


        public virtual Customer CustomerOrder { get; set; }
        public virtual Address ShipToAddress { get; set; }
        public virtual Address BillToAddress { get; set; }

        public virtual ICollection<OrderItem> OrdersItems { get; set; }
    }

}
