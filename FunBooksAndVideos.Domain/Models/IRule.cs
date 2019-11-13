namespace FunBooksAndVideos.Domain.Models
{
    public interface IRule
    {
        void Apply(Order order);
    }
}
