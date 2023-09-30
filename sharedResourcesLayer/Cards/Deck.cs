using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace sharedResourcesLayer
{
    public class Deck
    {
        private List<Card> cards;
        private List<Card> oldCards;
        private int amountOfDecks;
        private int availableCards;


        public Deck()
        {
            cards = new List<Card>();
            oldCards = new List<Card>();
            amountOfDecks = 0;
            availableCards = 0;
        }

        public void addDecks(int amount)
        {
            for(int i = 0; i < amount; i++)
            {
                addCards();
                amountOfDecks += 1;
                availableCards += 52;
            }
        }

        public void addCards()
        {
            foreach(CardSuite suite in Enum.GetValues(typeof(CardSuite)))
            {
                addSuite(suite);
            }
        }

        public void addSuite(CardSuite suite)
        {
            String start = "/Images/";
            String end = suite.ToString()[0] + ".png";
            String path;

            path = start + "A" + end;
            cards.Add(new Card(1, suite, "Ace", path));
            path = start + "2" + end;
            cards.Add(new Card(2, suite, "Two", path));
            path = start + "3" + end;
            cards.Add(new Card(3, suite, "Three", path));
            path = start + "4" + end;
            cards.Add(new Card(3, suite, "Four", path));
            path = start + "5" + end;
            cards.Add(new Card(5, suite, "Five", path));
            path = start + "6" + end;
            cards.Add(new Card(6, suite, "Six", path));
            path = start + "7" + end;
            cards.Add(new Card(7, suite, "Seven", path));
            path = start + "8" + end;
            cards.Add(new Card(8, suite, "Eight", path));
            path = start + "9" + end;
            cards.Add(new Card(9, suite, "Nine", path));
            path = start + "10" + end;
            cards.Add(new Card(10, suite, "Ten", path));
            path = start + "J" + end;
            cards.Add(new Card(10, suite, "Jack", path));
            path = start + "Q" + end;
            cards.Add(new Card(10, suite, "Queen", path));
            path = start + "K" + end;
            cards.Add(new Card(10, suite, "King", path));
        }


        public List<Card> getCards()
        {
            return cards;
        }

        public int getAmountOfCards()
        {
            return cards.Count;
        }

        public int getAmountOfAvailableCards()
        {
            return availableCards;
        }

        public void shuffleDeck()
        {
            Random rand = new Random();
            cards = cards.OrderBy(x => rand.Next()).ToList();
        }

        public Card getCard()
        {
            if(cards.Count < 2)
            {
                automaticShuffle();
            }
            Card temp = cards[0];
            removeCard(temp);

            return temp;
        }

        public void removeCard(Card card)
        {
            cards.Remove(card);
            oldCards.Add(card);
            availableCards -= 1;
        }

        private void automaticShuffle()
        {
            cards = oldCards;
            shuffleDeck();
            availableCards = (amountOfDecks * 52);
        }
    }
}
