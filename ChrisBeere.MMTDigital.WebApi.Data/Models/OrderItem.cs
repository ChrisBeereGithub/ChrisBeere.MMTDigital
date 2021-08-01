namespace ChrisBeere.MMTDigital.WebApi.Data.Models
{
    /// <summary>
    /// Model representing [SSE_Test.dbo.ORDERITEMS] table.
    /// </summary>
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public bool? Returnable { get; set; }
    }
}
