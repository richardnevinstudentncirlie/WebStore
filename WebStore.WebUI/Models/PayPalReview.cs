using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.WebUI.Models
{
    public class PayPalReview
    {
        public string OrderID { get; set; }
        public string OrderDate { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Email { get; set; }
        public string TotalAmount { get; set; }
    }
}