using FunBooksAndVideos.Domain.Models;
using FunBooksAndVideos.Domain.Rules;
using FunBooksAndVideos.Domain.Services;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace FunBooksAndVideos.Tests.Unit.Domain.Rules
{
    [TestFixture]
    public class ActivateMembershipRuleTests
    {
        private IRule _activateMembershipRule;
        private Mock<IUserService> _userService;

        [SetUp]
        public void Setup()
        {
            _userService = new Mock<IUserService>();
            _activateMembershipRule = new ActivateMembershipRule(_userService.Object);
        }

        [Test]
        public void WhenApplyIsCalled_WithNoMemberships_ActivateMembershipIsNeverCalled()
        {
            // GIVEN 
            _userService.Setup(x => x.ActivateMembership(It.IsAny<int>())).Verifiable();

            var order = new Order
            {
                CustomerId = 12,
                Id = 2,
                Total = 342,
                Items = new List<Item>() { new Item { Id = 5, Name = "test", Type = ItemType.Product } }
            };

            // WHEN 
            _activateMembershipRule.Apply(order);

            // Then
            _userService.Verify(x => x.ActivateMembership(It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void WhenApplyIsCalled_WithXNumberOfMemberships_ActivateMembershipIsCalledXTimes_WithCorrectParameters()
        {
            // GIVEN 
            _userService.Setup(x => x.ActivateMembership(It.IsAny<int>())).Verifiable();

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
            _activateMembershipRule.Apply(order);

            // Then
            _userService.Verify(x => x.ActivateMembership(5), Times.Once);
            _userService.Verify(x => x.ActivateMembership(4), Times.Once);
        }
    }
}
