using System;
using System.Collections.Generic;
using System.Text;

namespace GameCardLib
{
    //Class to send information to subscribers about the event that occurred

    public class GameEventInfo : EventArgs
    {
        private string gameInfo;
        private bool gameActive;

        public GameEventInfo(string gameInfo, bool gameActive)
        {
            this.gameInfo = gameInfo;
            this.gameActive = gameActive;
        }

        public string info
        {
            get { return gameInfo; }
            set { gameInfo = value; }
        }

        public bool gameState
        {
            get { return gameActive; }
            set { gameActive = value; }
        }


    }
}
