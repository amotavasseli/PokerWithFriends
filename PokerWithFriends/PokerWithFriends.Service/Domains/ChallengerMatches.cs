using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerWithFriends.Service.Domains
{
    public class ChallengerMatches : DateMadeModified
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public int MatchId { get; set; }
        public Guid MatchGuid { get; set; }
        public DateTime? MatchStartTime { get; set; }
        public int? Winner { get; set; }
        public int?[] Opponents { get; set; }
    }
}
