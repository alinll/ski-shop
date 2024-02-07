using Microsoft.EntityFrameworkCore;
using ski_shop.DTOs;
using ski_shop.Entities;

namespace ski_shop.Extensions
{
    public static class BasketExtensions
    {
        public static BasketDTo MapBasketToDto(this Basket basket)
        {
            return new BasketDTo
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
                Items = basket.Items.Select(item => new BasketItemDTo
                {
                    ProductId = item.ProductId,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    PictureUrl = item.Product.PictureUrl,
                    Type = item.Product.Type,
                    Brand = item.Product.Brand,
                    Quantity = item.Quantity
                }).ToList()
            };
        }

        public static IQueryable<Basket> RetrieveBasketWithItems(this IQueryable<Basket> query, string buyerId)
        {
            return query.Include(i => i.Items)
            .ThenInclude(p => p.Product)
            .Where(b => b.BuyerId == buyerId);
        }
    }
}