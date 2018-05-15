using System.Collections.Generic;
using PokerWithFriends.Service.Domains;
using PokerWithFriends.Service.Requests;

namespace PokerWithFriends.Service.Services
{
    public interface IContactsService
    {
        int Create(ContactRequest req);
        void Delete(int id);
        List<Contact> GetAll();
        Contact GetById(int id);
        void Update(ContactUpdateRequest req);
    }
}