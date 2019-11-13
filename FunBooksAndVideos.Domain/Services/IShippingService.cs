using FunBooksAndVideos.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FunBooksAndVideos.Domain.Services
{
    public interface IShippingService
    {
        void GenerateShippingSlip(Order order);
    }

    public class ShippingService : IShippingService
    {
        public void GenerateShippingSlip(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
