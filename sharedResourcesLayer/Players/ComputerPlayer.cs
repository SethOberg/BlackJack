using System;
using System.Collections.Generic;
using System.Text;

namespace sharedResourcesLayer.Players
{
    public class ComputerPlayer : OrdinaryPlayer
    {
        public ComputerPlayer()
        {

        }

        public ComputerPlayer(String name, int id) : base(name)
        {
        }

        public ComputerPlayer(String name, int balance, Stats stats) : base(name, balance, stats)
        {
        }

        //Determine if computer should pick up another card
        //Based on the computers value of their hand
        public bool takeCard()
        {
            if(getHand().getHandValue() < 16) 
            {
                return true;
            } else
            {
                return false;
            }
        }

        public void showAllCards()
        {
            getHand().faceUpAllCards();
        }
    }
}
