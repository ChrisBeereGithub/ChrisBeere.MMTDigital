using System;
using System.Collections.Generic;

namespace ChrisBeere.MMTDigital.WebApi.Data.Models
{
    /// <summary>
    /// Model representing [SSE_Test.dbo.ORDERS] table.
    /// </summary>
    public class Order
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryExpected { get; set; }
        public bool ContainsGift { get; set; }
        public string ShippingMode { get; set; }
        public string OrderSource { get; set; }
        public ICollection<OrderItem> OrderItems { get; set;}
    }
}
