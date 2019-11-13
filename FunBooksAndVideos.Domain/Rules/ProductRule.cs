using FunBooksAndVideos.Domain.Models;
using FunBooksAndVideos.Domain.Services;
using System.Linq;

namespace FunBooksAndVideos.Domain.Rules
{
    // Business Rule 2
    public class ProductRule : IRule
    {
        IShippingService _shippingService;

        public ProductRule(IShippingService shippingService)
        {
            _shippingService = shippingService;
        }

        public void Apply(Order order)
        {
            if (order.Items.Any(x => x.Type == ItemType.Product))
            {
                _shippingService.GenerateShippingSlip(order);
            }
        }
    }
}
