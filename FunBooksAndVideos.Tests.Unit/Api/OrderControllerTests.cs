using FunBooksAndVideos.Api.Controllers;
using FunBooksAndVideos.Domain.Models;
using FunBooksAndVideos.Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace FunBooksAndVideos.Tests.Unit.Api
{
    [TestFixture]
    public class OrderControllerTests
    {
        private OrderController _orderController;
        private Mock<IOrderService> _orderService;

        [SetUp]
        public void Setup()
        {
            _orderService = new Mock<IOrderService>();
            _orderController = new OrderController(_orderService.Object);
        }

        [Test]
        public void When_ProcessActionIsCalled_ProcessMethodIsCalled_WithRightParameter()
        {
            // GIVEN 
            _orderService.Setup(x => x.Process(It.IsAny<Order>())).Verifiable();
            var order = GetSampleOrder();

            // WHEN 
            _orderController.Process(order);

            // Then
            _orderService.Verify(x => x.Process(It.Is<Order>(o =>
            o == order)));
        }

        [Test]
        public void When_ProcessActionIsCalled_AndIsSuccessful_Returns_200()
        {
            // GIVEN 
            _orderService.Setup(x => x.Process(It.IsAny<Order>())).Verifiable();
            var order = GetSampleOrder();

            // WHEN 
            var res = _orderController.Process(order);

            // Then
            Assert.True(res is OkResult);
        }

        private Order GetSampleOrder()
        {
            return new Order
            {
                Id = 3,
                CustomerId = 4535,
                Items = new List<Item>()
                {
                    new Item {Id= 1, Name="test membership", Type = ItemType.Membership},
                    new Item {Id= 2, Name="test product", Type = ItemType.Product}
                }
            };
        }
    }
}
