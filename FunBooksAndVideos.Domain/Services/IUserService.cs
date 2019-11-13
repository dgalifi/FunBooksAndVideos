using System;

namespace FunBooksAndVideos.Domain.Services
{
    public interface IUserService
    {
        void ActivateMembership(int itemId);
    }

    public class UserService : IUserService
    {
        public void ActivateMembership(int itemId)
        {
            throw new NotImplementedException();
        }
    }
}
