using FunBooksAndVideos.Domain.Models;
using FunBooksAndVideos.Domain.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace FunBooksAndVideos.Tests.Unit.Domain.Services
{
    [TestFixture]
    public class OrderServiceTests
    {
        private IOrderService _orderService;

        [Test]
        public void ApplyIsCalledForEachRule()
        {
            var rule1 = new Mock<IRule>();
            var rule2 = new Mock<IRule>();
            var rule3 = new Mock<IRule>();

            rule1.Setup(x => x.Apply(It.IsAny<Order>())).Verifiable();
            rule2.Setup(x => x.Apply(It.IsAny<Order>())).Verifiable();
            rule3.Setup(x => x.Apply(It.IsAny<Order>())).Verifiable();

            var rules = new Mock<IList<IRule>>();

            rules.Setup(x => x.GetEnumerator()).Returns(Rules(rule1, rule2, rule3));

            _orderService = new OrderService(rules.Object);

            var order = new Order
            {
                CustomerId = 12,
                Id = 2,
                Total = 342,
                Items = new List<Item>() {
                    new Item { Id = 5, Name = "test", Type = ItemType.Membership },
                    new Item { Id = 4, Name = "test2", Type = ItemType.Membership } }
            };

            // WHEN 
            _orderService.Process(order);

            // Then
            rule1.Verify(x => x.Apply(order), Times.Once);
            rule2.Verify(x => x.Apply(order), Times.Once);
            rule3.Verify(x => x.Apply(order), Times.Once);
        }

        private IEnumerator<IRule> Rules(Mock<IRule> rule1, Mock<IRule> rule2, Mock<IRule> rule3)
        {
            yield return rule1.Object;
            yield return rule2.Object;
            yield return rule3.Object;
        }
    }
}
