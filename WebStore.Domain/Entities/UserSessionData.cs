using System;

namespace WebStore.Domain.Entities 
{

    [Serializable]
    public class UserSessionData
    {

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string RoldId { get; set; }

        public int CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }

    }
}
