using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace sharedResourcesLayer
{
    public class Hand
    {
        public List<Card> cards { get; set; }
        public int totalValue { get; set; }

        public Hand()
        {
            cards = new List<Card>();
            totalValue = 0;
        }

        public int getCardCount()
        {
            return cards.Count;
        }

        public void addCard(Card newCard)
        {
            cards.Add(newCard);
            totalValue += newCard.getValue();
        }


        public void faceUpAllCards()
        {
            for(int i = 0; i < cards.Count; i++)
            {
                cards[i].setFaceUp(true);
            }
        }

        //count ace! Either 1 or 11 depending on situation
        public int getHandValue()
        {
            if(hasAce())
            {
                return calculateAces();
            } else
            {
                return totalValue;
            }
        }

        public void clearHand()
        {
            cards = new List<Card>();
            totalValue = 0;
        }

        public int calculateAces()
        {
            int amountOfAces = getAmountOfAces();

            if(totalValue < 12)
            {
                return (totalValue += 10);
            } else
            {
                return totalValue;
            }

        }

        public bool containsCard(Card card)
        {
            return (cards.Contains(card));
        }

        public void removeCard(Card card)
        {
            cards.Remove(card);
            totalValue -= card.getValue();
        }

        public bool hasAce()
        {
            
            if(cards.Count > 0)
            {
                foreach(Card card in cards)
                {
                    if(card.getName().Equals("Ace"))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public int getAmountOfAces()
        {
            int amount = 0;

            foreach(Card card in cards)
            {
                if(card.getName().Equals("Ace"))
                {
                    amount += 1;
                }
            }

            return amount;
        }

        public List<String> getCardImages()
        {
            List<String> temp = new List<string>();

            for(int i = 0; i < cards.Count; i++)
            {
                temp.Add(cards[i].getImagePath());
            }

            return temp;
        }
    }
}
