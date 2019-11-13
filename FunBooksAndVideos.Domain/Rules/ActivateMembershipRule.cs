using FunBooksAndVideos.Domain.Models;
using FunBooksAndVideos.Domain.Services;
using System.Linq;

namespace FunBooksAndVideos.Domain.Rules
{
    // Business Rule 1
    public class ActivateMembershipRule : IRule
    {
        private readonly IUserService _userService;

        public ActivateMembershipRule(IUserService userService)
        {
            _userService = userService;
        }

        public void Apply(Order order)
        {
            if (order.Items.Any(x => x.Type == ItemType.Membership))
            {
                foreach (var item in order.Items)
                {
                    if (item.Type == ItemType.Membership)
                    {
                        _userService.ActivateMembership(item.Id);
                    }
                }
            }
        }
    }
}
