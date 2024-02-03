namespace ski_shop.DTOs
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public BasketDTo Basket { get; set; }
    }
}