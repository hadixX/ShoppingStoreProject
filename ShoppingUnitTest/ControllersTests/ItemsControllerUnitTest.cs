using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShoppingStore.Controllers;
using ShoppingStore.Models;
using ShoppingStore.Models.Dtos;
using ShoppingStore.Repository.Interfaces;
using ShoppingUnitTest.ControllersTests.TestDataGenerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingUnitTest.ControllersTests
{
    public class ItemsControllerTests
    {
        private Mock<IItemsRepository> _mockItemsRepository;
        private Mock<UserManager<IdentityUser>> _mockUserManager;
        private Mock<IMapper> _mockMapper;

        private ItemsController _itemsController;

        [SetUp]
        public void Setup()
        {
            _mockItemsRepository = new Mock<IItemsRepository>();
            _mockUserManager = MockUserManager<IdentityUser>();
            _mockMapper = new Mock<IMapper>();

            _itemsController = new ItemsController(
                _mockItemsRepository.Object,
                _mockUserManager.Object,
                _mockMapper.Object
            );
        }
        private Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var store = new Mock<IUserStore<TUser>>();
            return new Mock<UserManager<TUser>>(store.Object, null, null, null, null, null, null, null, null);
        }

        [Test]
        public async Task Index_ReturnsViewWithItems()
        {
            // Arrange
            var items = ItemTestGenerator.GetTestItems();
            _mockItemsRepository.Setup(repo => repo.GetAllItems(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(items);

            // Act
            var result = await _itemsController.Index();

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsAssignableFrom<PagedResultDto<Items>>(items);
        }

        [Test]
        public async Task Details_WithValidId_ReturnsViewWithItem()
        {
            var item = ItemTestGenerator.GetTestItem();
            // Arrange
            var itemId = Guid.NewGuid();
            _mockItemsRepository.Setup(repo => repo.GetItem(It.IsAny<Guid>())).ReturnsAsync(item);
            // Act
            var result = await _itemsController.Details(item.Id);

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
            Assert.IsAssignableFrom<Items>(item);
        }
    }
}
