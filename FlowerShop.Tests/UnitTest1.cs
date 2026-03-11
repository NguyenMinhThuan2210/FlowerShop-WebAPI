using FlowerShop.Core.DTOs.OrderDTOs;
using FlowerShop.Core.Entities;
using FlowerShop.Core.Interfaces;
using FlowerShop.Core.Services;
using Moq;

namespace FlowerShop.Tests
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepoMock;
        private readonly Mock<ICartRepository> _cartRepoMock;
        private readonly Mock<IProductRepository> _productRepoMock;
        private readonly Mock<IVoucherRepository> _voucherRepoMock;
        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _orderRepoMock = new Mock<IOrderRepository>();
            _cartRepoMock = new Mock<ICartRepository>();
            _productRepoMock = new Mock<IProductRepository>();
            _voucherRepoMock = new Mock<IVoucherRepository>();

            _orderService = new OrderService(
                _orderRepoMock.Object,
                _cartRepoMock.Object,
                _productRepoMock.Object,
                _voucherRepoMock.Object);
        }

        [Fact]
        public async Task CheckoutAsync_EmptyCart_ReturnsErrorMessage()
        {
            var userId = Guid.NewGuid();
            var dto = new CheckoutDto { Address = "123 SG", Phone = "0909" };
            _cartRepoMock.Setup(repo => repo.GetCartByUserIdAsync(userId))
                         .ReturnsAsync(new Cart { UserId = userId, CartItems = new List<CartItem>() });

            var result = await _orderService.CheckoutAsync(userId, dto);

            Assert.Equal("Giỏ hàng của bạn đang trống!", result);
        }

        [Fact]
        public async Task CheckoutAsync_ProductOutOfStock_ReturnsErrorMessage()
        {
            var userId = Guid.NewGuid();
            var productId = 1;
            var dto = new CheckoutDto { Address = "123 SG", Phone = "0909" };
            var mockCart = new Cart
            {
                UserId = userId,
                CartItems = new List<CartItem> { new CartItem { ProductId = productId, Quantity = 5} }
            };

            _cartRepoMock.Setup(repo => repo.GetCartByUserIdAsync(userId)).ReturnsAsync(mockCart);
            var mockProduct = new Product { Id = productId, Name = "Hoa Hồng", Price = 100, Stock = 2 };
            _productRepoMock.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(mockProduct);
            _orderRepoMock.Setup(repo => repo.BeginTransactionAsync()).Returns(Task.CompletedTask);
            _orderRepoMock.Setup(repo => repo.RollbackTransactionAsync()).Returns(Task.CompletedTask);
            var result = await _orderService.CheckoutAsync(userId, dto);
            Assert.Contains("không đủ số lượng", result);
            _orderRepoMock.Verify(repo => repo.RollbackTransactionAsync(), Times.Once);
        }
        [Fact]
        public async Task CheckoutAsync_ValidData_ReturnsNull()
        {
            var userId = Guid.NewGuid();
            var productId = 1;
            var dto = new CheckoutDto { Address = "123 SG", Phone = "0909" };
            var mockCart = new Cart { CartItems = new List<CartItem> { new CartItem { ProductId = productId, Quantity = 2 } } };
            _cartRepoMock.Setup(repo => repo.GetCartByUserIdAsync(userId)).ReturnsAsync(mockCart);
            var mockProduct = new Product { Id = productId, Name = "Hoa Hồng", Price = 100, Stock = 10};
            _productRepoMock.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(mockProduct);

            _orderRepoMock.Setup(repo => repo.BeginTransactionAsync()).Returns(Task.CompletedTask);
            _orderRepoMock.Setup(repo => repo.CommitTransactionAsync()).Returns(Task.CompletedTask);

            _orderRepoMock.Setup(repo => repo.SaveChangesAsync()).ReturnsAsync(true);
            var result = await _orderService.CheckoutAsync(userId, dto);

            Assert.Null(result);
            _orderRepoMock.Verify(repo => repo.CommitTransactionAsync(), Times.Once);
            Assert.Empty(mockCart.CartItems);
        }
    }
}
