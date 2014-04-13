using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using WebStore.Domain.Abstract;
using WebStore.Domain.Entities;
using WebStore.WebUI.Models;
using WebStore.WebUI.HtmlHelpers;

namespace WebStore.WebUI.Controllers {


    public class CartController : Controller {
        private IProductRepository repository;
        private IOrderRepository orderRepository;
        private IOrderItemRepository orderItemRepository;
        private IOrderProcessor orderProcessor;

        public CartController(IProductRepository repo, IOrderRepository orderRepo, IOrderItemRepository orderItemRepo, IOrderProcessor proc) {
            repository = repo;
            orderRepository = orderRepo;
            orderItemRepository = orderItemRepo;
            orderProcessor = proc;
        }
        

        public ViewResult Index(Cart cart, string returnUrl) {
            return View(new CartIndexViewModel {
                ReturnUrl = returnUrl,
                Cart = cart
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId,
                string returnUrl) {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null) {
                cart.AddItem(product, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId,
                string returnUrl) {
            Product product = repository.Products
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null) {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        public PartialViewResult Summary(Cart cart) {
            return PartialView(cart);
        }

        public ViewResult Checkout(Cart cart)
        {
            //for testing
            int rc = testStoreOfInfo(cart);

            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {

            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.ProcessOrder(cart, shippingDetails);
                cart.Clear();
                return View("Completed");
            }
            else
            {
                return View(shippingDetails);
            }
        }

        public int testStoreOfInfo(Cart cart)
        {
            int orderID;

            string firstName,  lastName,  company,  email, 
                streetLine1,  streetLine2,  streetLine3, 
                city,  postalCode,  county,   country;
            string paymentConfirmation;

            firstName = "firstName";
            lastName = "lastName"; company = "company"; email = "email";
            streetLine1 = "streetLine1"; streetLine2 = "streetLine2"; streetLine3 = "streetLine3";
            city = "city"; postalCode = "postalCode"; county = "county"; country = "country";
            paymentConfirmation = "paymentConfirmation";

            orderID = StoreOrderAndOrderItems(cart);

            orderID = UpdateOrderShipTo(orderID, firstName, lastName, company, email,
                streetLine1, streetLine2, streetLine3,
                city, postalCode, county, country);

            orderID = UpdateOrderConfirmation(orderID, paymentConfirmation);

            return 1;
        }

        public int StoreOrderAndOrderItems(Cart cart)
        {

            Order order = new Order();
            //order.OrderID
            order.OrderDate = DateTime.Now;
            order.ShipDate = DateTime.Now;
            order.TotalOrder = cart.ComputeTotalValue();
            order.ProductCount = cart.ComputeNumItems();
            order.FirstName = string.Empty;
            order.LastName = string.Empty;
            order.Company = string.Empty;
            order.Email = string.Empty;
            order.StreetLine1 = string.Empty;
            order.StreetLine2 = string.Empty;
            order.StreetLine3 = string.Empty;
            order.City = string.Empty;
            order.PostalCode = string.Empty;
            order.County = string.Empty;
            order.Country = string.Empty;
            order.PaymentConfirmation = string.Empty;
            order.CreatedAt = DateTime.Now;
            order.UpdatedAt = DateTime.Now;
            order.BillToAddressID = 1;
            order.ShipToAddressID = 1;
            order.CustomerID = 1;

            orderRepository.SaveOrder(order);

            foreach (var line in cart.Lines)
            {
                OrderItem orderItem = new OrderItem();

                Product product = repository.Products
                 .FirstOrDefault(p => p.ProductID == line.Product.ProductID); 

                //orderItem.OrderItemID
                orderItem.Name = line.Product.Name;
                orderItem.Description= line.Product.Description;
                orderItem.Price = line.Product.Price;
                orderItem.Category = line.Product.Category;
                orderItem.Special = line.Product.Special;
                orderItem.ImageData = line.Product.ImageData;
                orderItem.ImageMimeType = line.Product.ImageMimeType;
                orderItem.Seller = line.Product.Seller;
                orderItem.Buyer = line.Product.Buyer;
                orderItem.Quantity = line.Quantity;
                orderItem.OrderID = order.OrderID;
                orderItem.ProductID = product.ProductID;
                orderItem.CategoryID = product.CategoryID;
                orderItemRepository.SaveOrderItem(orderItem);

            }

            return order.OrderID;
        }

        public int UpdateOrderShipTo(int orderID, string firstName, string lastName, string company, string email, 
            string streetLine1, string streetLine2, string streetLine3, 
            string city, string postalCode, string county,  string country)
        {
            Order order = orderRepository.Orders.FirstOrDefault(p => p.OrderID == orderID);
            order.FirstName = firstName;
            order.LastName = lastName;
            order.Company = company;
            order.Email = email;
            order.StreetLine1 = streetLine1;
            order.StreetLine2 = streetLine2;
            order.StreetLine3 = streetLine3;
            order.City = city;
            order.PostalCode = postalCode;
            order.County = county;
            order.Country = country;
            orderRepository.SaveOrder(order);
            return orderID;
        }

        public int UpdateOrderConfirmation(int orderID, string paymentConfirmation)
        {
            Order order = orderRepository.Orders.FirstOrDefault(p => p.OrderID == orderID);
            order.PaymentConfirmation = paymentConfirmation;
            orderRepository.SaveOrder(order);
            return orderID;
        }

        public int UpdateProductQuantity(int productID, int quantity)
        {
            Product product = repository.Products.FirstOrDefault(p => p.ProductID == productID);
            product.Quantity -= quantity;
            repository.SaveProduct(product);
            return productID;
        }

        // PayPal
        public ActionResult CheckoutWithPayPal(Cart cart)
        {

            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
                return View("Completed");
            }
            else
            {

                int orderID = StoreOrderAndOrderItems(cart);
                UserSessionData.OrderID = orderID;

                NVPAPICaller payPalCaller = new NVPAPICaller();
                string retMsg = "";
                string token = "";
                decimal amtVal = cart.ComputeTotalValue();
                string amt = amtVal.ToString();
                bool ret = payPalCaller.ShortcutExpressCheckout(cart, amt, ref token, ref retMsg);
                if (ret)
                {

                    UserSessionData.Token = token;
                    UserSessionData.PaymentAmount = amtVal;

                    return Redirect(retMsg);
                }
                else
                {
                    return View("Completed");
                }
            }
        }

        public ViewResult CheckoutReview()
        {
            
            int orderID;

            string firstName, lastName, company, email,
                streetLine1, streetLine2, streetLine3,
                city, postalCode, county, country;

            string retMsg = "";
            string token = "";
            decimal paymentAmountOnCheckout = 0;
            string PayerID = "";

            NVPAPICaller payPalCaller = new NVPAPICaller();

            NVPCodec decoder = new NVPCodec();

            token = UserSessionData.Token.ToString();
            paymentAmountOnCheckout = UserSessionData.PaymentAmount;

            bool ret = payPalCaller.GetCheckoutDetails(token, ref PayerID, ref decoder, ref retMsg);
            if (ret)
            {
               
                // Verify total payment amount as set on CheckoutStart.aspx.
                try
                {
                    decimal paymentAmoutFromPayPal = Convert.ToDecimal(decoder["AMT"].ToString());
                    if (paymentAmountOnCheckout != paymentAmoutFromPayPal)
                    {
                        return View("CheckoutError");
                    }
                }
                catch (Exception)
                {
                    return View("CheckoutError");
                }

                
                //Session information
                UserSessionData.PayerID = PayerID;
                orderID = UserSessionData.OrderID;

                //Update the order with PayPal Informatin
                company = string.Empty; city = string.Empty; county = string.Empty; 
                //OrderDate = Convert.ToDateTime(decoder["TIMESTAMP"].ToString());
                //Username = User.Identity.Name;
                firstName = decoder["FIRSTNAME"].ToString();
                lastName = decoder["LASTNAME"].ToString();
                streetLine1 = decoder["SHIPTOSTREET"].ToString();
                streetLine2 = decoder["SHIPTOCITY"].ToString();
                streetLine3 = decoder["SHIPTOSTATE"].ToString();
                postalCode = decoder["SHIPTOZIP"].ToString();
                country = decoder["SHIPTOCOUNTRYCODE"].ToString();
                email = decoder["EMAIL"].ToString();
                //Total = Convert.ToDecimal(decoder["AMT"].ToString());
                orderID = UpdateOrderShipTo(orderID, firstName, lastName, company, email,
                    streetLine1, streetLine2, streetLine3,
                    city, postalCode, county, country);

                //Prepare for display
                PayPalReview ppReview = new PayPalReview();
                ppReview.OrderID = orderID.ToString();
                ppReview.OrderDate = decoder["TIMESTAMP"].ToString();
                ppReview.Username = User.Identity.Name;
                ppReview.FirstName = firstName;
                ppReview.LastName = lastName;
                ppReview.Address = streetLine1;
                ppReview.City = streetLine2;
                ppReview.State = streetLine3;
                ppReview.PostalCode = postalCode;
                ppReview.Email = email;
                ppReview.TotalAmount = paymentAmountOnCheckout.ToString();
                ViewBag.PayPalReview = ppReview;

                return View("CheckoutReviewOrder");
            }
            else
            {
                return View("CheckoutError");
            }
        }
        public ViewResult CheckoutReviewOrderCont()
        {

            int orderID;
            int currentOrderId = 0;
            string retMsg = "";
            string token = "";
            string finalPaymentAmount = "";
            string PayerID = "";

            NVPAPICaller payPalCaller = new NVPAPICaller();

            NVPCodec decoder = new NVPCodec();


            token = UserSessionData.Token.ToString();
            finalPaymentAmount = UserSessionData.PaymentAmount.ToString();
            PayerID = UserSessionData.PayerID;
            currentOrderId = UserSessionData.OrderID;

            bool ret = payPalCaller.DoCheckoutPayment(finalPaymentAmount, token, PayerID, ref decoder, ref retMsg);
            if (ret)
            {
                // Retrieve PayPal confirmation value.
                string PaymentConfirmation = decoder["PAYMENTINFO_0_TRANSACTIONID"].ToString();

                //Update the order 
                orderID = UserSessionData.OrderID;
                orderID = UpdateOrderConfirmation(orderID, PaymentConfirmation);

                //Reset session data
                UserSessionData.PaymentAmount = 0;
                UserSessionData.OrderID = 0;

                ViewBag.PaymentConfirmation = PaymentConfirmation;

                //Reduce the quantity of products in stock
                IEnumerable<OrderItem> orderItems = orderItemRepository.OrderItems.Where(x => x.OrderID == orderID);
                foreach (var orderItem in orderItems)
                {
                    UpdateProductQuantity(orderItem.ProductID, orderItem.Quantity);
                }

                return View("CheckoutReviewTrans");
            }
            else
            {
                return View("CheckoutError");
            }
        }
        public ViewResult CheckoutReviewTransCont()
        {
            return View("Completed");
        }

        public ViewResult CheckoutCancel()
        {
            return View("CheckoutCancel");
        }

        public ViewResult CheckoutCancelContinue()
        {
            return View("Completed");
        }

        public ViewResult CheckoutErrorContinue()
        {
            return View("Completed");
        }
 
        public ViewResult CheckoutCompleteContinue()
        {
            return View("Completed");
        }

        // End PayPal


    }
}
