using sharedResourcesLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace GameCardLib.GamePlay
{
    public class DatabaseInfo : EventArgs
    {
        private List<showDbResult> result;

        public DatabaseInfo(List<showDbResult> players)
        {
            result = players;
        }

        public List<showDbResult> savedPlayers
        {
            get { return result; }
            set { result = value; }
        }

    }
}
