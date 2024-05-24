using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ski_shop.Controllers;
using ski_shop.Data;
using ski_shop.DTOs;
using ski_shop.Entities;
using ski_shop.Services;

namespace ski_shop.Test.Controllers
{
    public class AccountTestController
    {
        private readonly StoreContext _context;
        private readonly UserManager<User> _userManager;
        private readonly TokenService _tokenService;

        public AccountTestController()
        {
            var connection = new Connection();
            _context = connection.CreateContext();
        }

        [Fact]
        public async Task TaskRegisterReturn201()
        {
            var userManagerMock = new Mock<UserManager<User>>(Mock
            .Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            var controller = new AccountController(userManagerMock.Object, _tokenService, _context);
            var registerDto = new RegisterDto
            {
                UserName = "testuser",
                Email = "test@example.com",
                Password = "TestPassword123"
            };

            userManagerMock.Setup(um => um.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var result = await controller.Register(registerDto);

            var statusCodeResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(201, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task TaskLoginReturnUserDto()
        {
            var userManagerMock = new Mock<UserManager<User>>(Mock
            .Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
            var tokenServiceMock = new Mock<TokenService>(userManagerMock.Object, null);
            var controller = new AccountController(userManagerMock.Object, tokenServiceMock.Object, _context);

            var loginDto = new LoginDto
            {
                UserName = "testuser",
                Password = "testpassword"
            };

            userManagerMock.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(true);

            var result = await controller.Login(loginDto);

            Assert.IsType<ActionResult<UserDto>>(result);
        }
    }
}