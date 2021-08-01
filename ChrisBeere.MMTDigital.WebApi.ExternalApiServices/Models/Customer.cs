using System;

namespace ChrisBeere.MMTDigital.WebApi.ExternalApiServices.Models
{
    /// <summary>
    /// Model representing deserialised Json returned from Customer Account Api.
    /// </summary>
    public class Customer
    {
        public string Email { get; set; }
        public string CustomerId { get; set; }
        public bool Website { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime LastLoggedIn { get; set; }
        public string HouseNumber { get; set; }
        public string Street { get; set; }
        public string Town { get; set; }
        public string PostCode { get; set; }
    }
}
