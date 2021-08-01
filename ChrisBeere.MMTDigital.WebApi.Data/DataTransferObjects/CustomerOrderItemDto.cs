using System;

namespace ChrisBeere.MMTDigital.WebApi.Data.DataTransferObjects
{
    /// <summary>
    /// Customer/Order/OrderItem/Product data transfer object used by the OrderRepository.
    /// </summary>
    public class CustomerOrderItemDto
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryExpected { get; set; }
        public bool ContainsGift { get; set; }
        public string ShippingMode { get; set; }
        public string OrderSource { get; set; }
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public bool? Returnable { get; set; }
        public string ProductName { get; set; }
        public decimal PackHeight { get; set; }
        public decimal PackWidth { get; set; }
        public decimal PackWeight { get; set; }
        public string Colour { get; set; }
        public string Size { get; set; }
    }
}
