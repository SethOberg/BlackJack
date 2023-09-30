using System;
using System.Collections.Generic;
using System.Text;

namespace sharedResourcesLayer.Cards
{
    //Class used to bind images to databinding in the MainWindow GUI lists
    public class CardImage
    {
        public String imagePath { get; set; }
        public String cardBackPath;

        public static String cardBackImagePath()
        {
            return "/images/blue_back.png";
        }
    }
}
