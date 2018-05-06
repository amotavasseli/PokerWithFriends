using System.Collections.Generic;
using PokerWithFriends.Service.Domains;
using PokerWithFriends.Service.Requests;

namespace PokerWithFriends.Service.Services
{
    public interface IUsersService
    {
        List<User> GetAll();
        User GetUserById(int id);
        int Create(UserRequest req);
        void Update(UserUpdateRequest req);
        void Delete(int id);
    }
}