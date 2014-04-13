using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
namespace WebStore.Domain.Entities
{

    public class Order
    {
        [HiddenInput(DisplayValue = false)]
        public int OrderID { get; set; }


        //[Required(ErrorMessage = "Please specify an order date")]
        public DateTime OrderDate { get; set; }

        //[Required(ErrorMessage = "Please specify a shipping date")]
        public DateTime ShipDate { get; set; }

        //[Required]
        //[Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        public decimal TotalOrder { get; set; }

        //[Required]
        //[Range(1, int.MaxValue, ErrorMessage = "Please enter a positive number")]
        public int ProductCount { get; set; }


        [MaxLength(50)]
        //[Required(ErrorMessage = "Please enter a first name")]
        public string FirstName { get; set; }

        [MaxLength(30)]
        //[Required(ErrorMessage = "Please enter a last name")]
        public string LastName { get; set; }

        [MaxLength(120)]
        //[Required(ErrorMessage = "Please enter a company name")]
        public string Company { get; set; }

        [MaxLength(225)]
        public string Email { get; set; }

        [MaxLength(50)]
        //[Required(ErrorMessage = "Please enter an address")]
        public string StreetLine1 { get; set; }

        [MaxLength(50)]
        public string StreetLine2 { get; set; }

        [MaxLength(50)]
        public string StreetLine3 { get; set; }

        [MaxLength(50)]
        //[Required(ErrorMessage = "Please enter a city name")]
        public string City { get; set; }

        [MaxLength(50)]
        //[Required(ErrorMessage = "Please enter a postal code")]
        public string PostalCode { get; set; }

        [MaxLength(50)]
        //[Required(ErrorMessage = "Please enter a county")]
        public string County { get; set; }

        [MaxLength(50)]
        //[Required(ErrorMessage = "Please enter a country")]
        public string Country { get; set; }

        [MaxLength(225)]
        public string PaymentConfirmation { get; set; }


        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedAt { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime UpdatedAt { get; set; }


        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer OrderCustomerID { get; set; }

        public int ShipToAddressID { get; set; }
        [ForeignKey("ShipToAddressID")]
        public virtual Address OrderShipToAddressID { get; set; }

        public int BillToAddressID { get; set; }
        [ForeignKey("BillToAddressID")]
        public virtual Address OrderBillToAddressID { get; set; }

        public virtual ICollection<OrderItem> OrdersItems { get; set; }
    }

}
