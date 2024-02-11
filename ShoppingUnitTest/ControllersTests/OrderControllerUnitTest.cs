using Microsoft.AspNetCore.Mvc;
using Moq;
using Shopping.UnitTest.UnitTests.ControllersTests.TestDataGenerators;
using ShoppingStore.Controllers;
using ShoppingStore.Models;
using ShoppingStore.Repository.Interfaces;

namespace Shopping.UnitTest.UnitTests.ControllersTests
{

    public class OrderControllerUnitTest
    {
        private Mock<IOrderRepository> _mockOrderRepository;
        private OrdersController _ordersController;

        [SetUp]
        public void Setup()
        {
            _mockOrderRepository = new Mock<IOrderRepository>();
            _ordersController = new OrdersController(_mockOrderRepository.Object);
        }

        

        [Test]
        public async Task Details_WithValidId_ReturnsViewWithOrder()
        {
            var order = OrderTestGenerator.GetTestOrder();
            // Arrange
            _mockOrderRepository.Setup(repo => repo.GetOrderAsync(order.Id)).ReturnsAsync(order);

            // Act
            var result = await _ordersController.Details(order.Id);

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsAssignableFrom<Orders>(order);
        }

        
        [Test]
        public async Task Delete_WithValidId_ReturnsRedirectToActionResult()
        {
            // Arrange
            var orderId = Guid.NewGuid();

            // Act
            var result = await _ordersController.Delete(orderId);

            // Assert
            Assert.IsInstanceOf<RedirectToActionResult>(result);
            var redirectToActionResult = (RedirectToActionResult)result;
            Assert.AreEqual("Index", redirectToActionResult.ActionName);
        }
    }

}
