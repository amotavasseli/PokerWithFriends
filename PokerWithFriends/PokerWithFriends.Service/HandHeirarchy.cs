using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerWithFriends.Service
{
    public class HandHeirarchy
    {
        private string[] suits = new string[] { "Spade", "Heart", "Club", "Diamond" };
        private string[] values = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A" };

        public List<Card> Deck()
        {
            List<Card> cards = new List<Card>();
            for(int i = 0; i < suits.Length; i++)
            {
                for(int j = 0; j < values.Length; j++)
                {
                    Card card = new Card();
                    card.Suit = suits[i];
                    card.Value = values[j];
                    cards.Add(card);
                }
            }
            return cards;
        }
    }
}
