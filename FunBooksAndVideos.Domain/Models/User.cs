using System;

namespace FunBooksAndVideos.Domain.Models
{
    public interface IUser
    {
        void ActivateMembership(int itemId);
    }

    public class User : IUser
    {
        public void ActivateMembership(int itemId)
        {
            throw new NotImplementedException();
        }
    }
}
