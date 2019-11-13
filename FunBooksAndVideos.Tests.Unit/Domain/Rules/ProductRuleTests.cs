using FunBooksAndVideos.Domain.Models;
using FunBooksAndVideos.Domain.Rules;
using FunBooksAndVideos.Domain.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunBooksAndVideos.Tests.Unit.Domain.Rules
{
    [TestFixture]
    public class ProductRuleTests
    {
        private IRule _productRule;
        private Mock<IShippingService> _shippingService;

        [SetUp]
        public void Setup()
        {
            _shippingService = new Mock<IShippingService>();
            _productRule = new ProductRule(_shippingService.Object);
        }

        [Test]
        public void WhenApplyIsCalled_WithNoProducts_GenerateShippingSlipIsNeverCalled()
        {
            // GIVEN 
            _shippingService.Setup(x => x.GenerateShippingSlip(It.IsAny<Order>())).Verifiable();

            var order = new Order
            {
                CustomerId = 12,
                Id = 2,
                Total = 342,
                Items = new List<Item>() { new Item { Id = 5, Name = "test", Type = ItemType.Membership } }
            };

            // WHEN 
            _productRule.Apply(order);

            // Then
            _shippingService.Verify(x => x.GenerateShippingSlip(It.IsAny<Order>()), Times.Never);
        }

        [Test]
        public void WhenApplyIsCalled_WithXNumberOfProducts_GenerateShippingSlipIsCalled_WithCorrectParameters()
        {
            // GIVEN 
            _shippingService.Setup(x => x.GenerateShippingSlip(It.IsAny<Order>())).Verifiable();

            var order = new Order
            {
                CustomerId = 12,
                Id = 2,
                Total = 342,
                Items = new List<Item>() {
                    new Item { Id = 5, Name = "test", Type = ItemType.Product },
                    new Item { Id = 4, Name = "test2", Type = ItemType.Product } }
            };

            // WHEN 
            _productRule.Apply(order);

            // Then
            _shippingService.Verify(x => x.GenerateShippingSlip(order), Times.Once);
        }
    }
}
