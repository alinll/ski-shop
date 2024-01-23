namespace ski_shop.DTOs
{
    public class BasketDTo
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public List<BasketItemDTo> Items { get; set; }
    }
}