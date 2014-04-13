using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace WebStore.Domain.Entities
{

    public class Customer
    {

        [HiddenInput(DisplayValue = false)]
        public int CustomerID { get; set; }

        //[HiddenInput(DisplayValue = false)]
        [Required(ErrorMessage = "Please enter a UserID")]
        public int  UserID { get; set; }

        [MaxLength(50)]
        [Required(ErrorMessage = "Please enter a first name")]
        public string FirstName { get; set; }

        [MaxLength(30)]
        [Required(ErrorMessage = "Please enter a last name")]
        public string LastName { get; set; }

        [MaxLength(120)]
        [Required(ErrorMessage = "Please enter a company name")]
        public string Company { get; set; }

        [MaxLength(225)]
        public string Email { get; set; }

        [MinLength(4), MaxLength(15)]
        public string Phone { get; set; }


        [HiddenInput(DisplayValue = false)]
        public DateTime CreatedAt { get; set; }

        [HiddenInput(DisplayValue = false)]
        public DateTime UpdatedAt { get; set; }


        //public virtual UserProfile UserID { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }

}
