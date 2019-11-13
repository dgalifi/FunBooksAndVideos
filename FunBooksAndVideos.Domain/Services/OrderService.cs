using FunBooksAndVideos.Domain.Models;
using System.Collections.Generic;

namespace FunBooksAndVideos.Domain.Services
{
    public class OrderService : IOrderService
    {
        IList<IRule> _rules;

        public OrderService(IList<IRule> rules)
        {
            _rules = rules;
        }
        public void Process(Order order)
        {
            ProcessRules(order);
        }

        private void ProcessRules(Order order)
        {
            foreach (var rules in _rules)
            {
                rules.Apply(order);
            }

            // Submit the order
            Submit();
        }

        private void Submit()
        {
            // Save the order in a DB or somewhere else and submit it
        }
    }
}
