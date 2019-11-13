using System;
using System.Collections.Generic;
using System.Text;

namespace FunBooksAndVideos.Domain.Models
{
    public class Order
    {
        public int Id { get; set; }

        public decimal Total { get; set; }

        public int CustomerId { get; set; }

        public IList<Item> Items { get; set; }
    }
}
