using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerWithFriends.Service.Requests
{
    public class MatchRequest
    {
        [Required]
        public int ChallengerId { get; set; }
        public DateTime? MatchStartTime { get; set; }
        public int? Winner { get; set; }
        public int?[] Opponents { get; set; }
    }
}
