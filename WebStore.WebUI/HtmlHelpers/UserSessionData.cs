using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebStore.WebUI.HtmlHelpers
{

    public class UserSessionData
    {

        //Session variable constants
        public const string USERID = "UserId";  
        public const string USERNAME = "UserName";  
        public const string ROLEID = "RoldId";

        public const string CUSTOMERID = "CustomerID";  
        public const string FIRSTNAME = "FirstName";  
        public const string LASTNAME = "LastName";  
        public const string COMPANY = "Company";  
        public const string EMAIL = "Email";  

        public const string TOKEN = "Token";
        public const string PAYMENTAMOUNT = "PaymentAmount";  
        public const string PAYERID = "PayerID";

        public const string ORDERID = "OrderID";  


        public static T Read<T>(string variable)
        {
            object value = System.Web.HttpContext.Current.Session[variable];
            if (value == null)
                return default(T);
            else
                return ((T)value);
        }

        public static void Write(string variable, object value)
        {
            HttpContext.Current.Session[variable] = value;
        }

        public static int UserId
        {
            get { return Read<int>(USERID); }
            set { Write(USERID, value); }
        }

        public static string UserName
        {
            get { return Read<string>(USERNAME); }
            set { Write(USERNAME, value); }
        }

        public static string RoldId
        {
            get { return Read<string>(ROLEID); }
            set { Write(ROLEID, value); }
        }

        public static int CustomerID
        {
            get { return Read<int>(CUSTOMERID); }
            set { Write(CUSTOMERID, value); }
        }

        public static string FirstName
        {
            get { return Read<string>(FIRSTNAME); }
            set { Write(FIRSTNAME, value); }
        }

        public static string LastName
        {
            get { return Read<string>(LASTNAME); }
            set { Write(LASTNAME, value); }
        }

        public static string Company
        {
            get { return Read<string>(COMPANY); }
            set { Write(COMPANY, value); }
        }

        public static string Email
        {
            get { return Read<string>(EMAIL); }
            set { Write(EMAIL, value); }
        }

        public static string Token
        {
            get { return Read<string>(TOKEN); }
            set { Write(TOKEN, value); }
        }

        public static decimal PaymentAmount
        {
            get { return Read<decimal>(PAYMENTAMOUNT); }
            set { Write(PAYMENTAMOUNT, value); }
        }

        public static string PayerID
        {
            get { return Read<string>(PAYERID); }
            set { Write(PAYERID, value); }
        }

        public static int OrderID
        {
            get { return Read<int>(ORDERID); }
            set { Write(ORDERID, value); }
        }

    }

}
