namespace ChrisBeere.MMTDigital.WebApi.Services.DataTransferObjects
{
    /// <summary>
    /// Data transfer object defining OrderItem model encapsulated by CustomerOrderDto.
    /// </summary>
    public class OrderItemDto
    {
        public string Product { get; set; }
        public int? Quantity { get; set; }
        public decimal? PriceEach { get; set; }
    }
}
