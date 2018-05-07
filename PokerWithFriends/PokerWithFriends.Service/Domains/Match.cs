using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerWithFriends.Service.Domains
{
    public class Match
    {
        public int Id { get; set; }
        public Guid MatchGuid { get; set; }
        public int ChallengerId { get; set; }
        public DateTime? MatchStartTime { get; set; }
        public int? Winner { get; set; }
        public int?[] Opponents { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}
