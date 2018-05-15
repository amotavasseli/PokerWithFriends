using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerWithFriends.Service.Requests
{
    public class ContactUpdateRequest : ContactRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
