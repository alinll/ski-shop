using ski_shop.Entities.OrderAggregate;

namespace ski_shop.DTOs
{
    public class CreateOrderDto
    {
        public bool SaveAddress { get; set; }
        public ShippingAddress ShippingAddress { get; set; }
    }
}