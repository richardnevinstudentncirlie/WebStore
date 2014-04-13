using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
namespace WebStore.Domain.Entities
{

    public class Address
    {
        [HiddenInput(DisplayValue = false)]
        public int AddressID { get; set; }

        
        [MaxLength(50)]
        [Required(ErrorMessage = "Please enter an address")]
        public string StreetLine1 { get; set; }

        [MaxLength(50)]
        public string StreetLine2 { get; set; }

        [MaxLength(50)]
        public string StreetLine3 { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Please enter a city name")]
        public string City { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Please enter a postal code")]
        public string PostalCode { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Please enter a county")]
        public string County { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Please enter a country")]
        public string Country { get; set; }

        
        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedAt { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime UpdatedAt { get; set; }

        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public virtual Customer AddressCustomerID { get; set; }
    }
}
