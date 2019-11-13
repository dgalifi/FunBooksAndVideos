using FunBooksAndVideos.Domain.Models;

namespace FunBooksAndVideos.Domain.Services
{
    public interface IOrderService
    {
        void Process(Order order);
    }
}
