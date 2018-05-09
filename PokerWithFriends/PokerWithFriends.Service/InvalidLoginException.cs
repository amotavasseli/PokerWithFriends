using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerWithFriends.Service
{
    public class InvalidLoginException : Exception
    {
        public InvalidLoginException(string message) : base(message) { }
    }
}
