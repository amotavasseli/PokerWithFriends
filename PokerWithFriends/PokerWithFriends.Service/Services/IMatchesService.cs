using System.Collections.Generic;
using PokerWithFriends.Service.Domains;
using PokerWithFriends.Service.Requests;

namespace PokerWithFriends.Service.Services
{
    public interface IMatchesService
    {
        int CreateMatch(MatchRequest req);
        void DeleteMatch(int id);
        List<Match> GetAllMatches();
        Match GetMatchById(int id);
        void UpdateMatch(MatchUpdateRequest req);
    }
}