using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ski_shop.Controllers;
using ski_shop.Data;
using ski_shop.Entities;
using ski_shop.RequestHelpers;

namespace ski_shop.Test.Controllers
{
    public class ProductsTestController
    {
        private readonly StoreContext _context;
        private readonly ProductsController _controller;
        private readonly IMapper _mapper;

        public ProductsTestController()
        {
            var connection = new Connection();
            _context = connection.CreateContext();
            _controller = new ProductsController(_context, _mapper);
            _controller.ControllerContext.HttpContext = new DefaultHttpContext();
        }

        [Fact]
        public async Task TaskGetProductsReturnProducts()
        {
            var productParams = new ProductParams { OrderBy = "desc", PageSize = 2 };

            Product[] products = 
            {
                new Product
                {
                    Id = 8,
                    Name = "B"
                },
                new Product
                {
                    Id = 7,
                    Name = "P"
                }
            };

            _context.Products.AddRange(products);
            _context.SaveChanges();

            var data = await _controller.GetProducts(productParams);

            Assert.Equal(products[0].Name, _context.Products.OrderBy(i => i.Name).First().Name.ToString());
        }

        [Fact]
        public async Task TaskGetProductsReturnPagedList()
        {
            var productParams = new ProductParams { OrderBy = "desc", PageSize = 2 };

            Product[] products = 
            {
                new Product
                {
                    Id = 8,
                    Name = "B"
                },
                new Product
                {
                    Id = 7,
                    Name = "P"
                }
            };

            _context.Products.AddRange(products);
            _context.SaveChanges();

            var data = await _controller.GetProducts(productParams);

            Assert.IsType<ActionResult<PagedList<Product>>>(data);
        }

        [Fact]
        public async Task TaskGetProductReturnProduct()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Angular Speedster Board 2000"
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            var data = await _controller.GetProduct(product.Id);

            Assert.Equal("Angular Speedster Board 2000", data.Value.Name);
        }

        [Fact]
        public async Task TaskGetProductReturnTypeProduct()
        {
            var product = new Product
            {
                Id = 1,
                Name = "Angular Speedster Board 2000"
            };

            _context.Products.Add(product);
            _context.SaveChanges();

            var data = await _controller.GetProduct(product.Id);

            Assert.IsType<ActionResult<Product>>(data);
        }

        [Fact]
        public async Task TaskGetProductReturnNotFound()
        {
            var data = await _controller.GetProduct(100);

            Assert.IsType<NotFoundResult>(data.Result);
        }

        [Fact]
        public async Task TaskGetFiltersReturnFilters()
        {
            var data = await _controller.GetFilters();

            Assert.IsType<OkObjectResult>(data);
        }
    }
}
