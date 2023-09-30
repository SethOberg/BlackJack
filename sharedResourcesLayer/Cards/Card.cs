using sharedResourcesLayer.Cards;
using System;
using System.Collections.Generic;
using System.Text;

namespace sharedResourcesLayer
{

    public class Card
    {
        public int value { get; set; }
        public CardSuite suite { get; set; }
        public string name { get; set; }
        public bool faceUp { get; set; }
        public String imagePath { get; set; }

        public Card(int value, CardSuite suite, String name, String imagePath)
        {
            this.value = value;
            this.suite = suite;
            this.name = name;
            faceUp = true;
            this.imagePath = imagePath;
        }

        public Card(int value, CardSuite suite, String name, String imagePath, bool faceUp)
        {
            this.value = value;
            this.suite = suite;
            this.name = name;
            this.imagePath = imagePath;
            this.faceUp = faceUp;
        }

        public String getImagePath()
        {
            if(faceUp)
            {
                return imagePath;
            } else
            {
                return CardImage.cardBackImagePath();
            }
        }

        public void setImagePath(String newImagePath)
        {
            imagePath = newImagePath;
        }

        public void setFaceUp(bool faceUp)
        {
            this.faceUp = faceUp;
        }

        public bool getFaceUp()
        {
            return faceUp;
        }

        public int getValue()
        {
            return value;
        }

        public void setValue(int newValue)
        {
            value = newValue;
        }

        public CardSuite getSuite()
        {
            return suite;
        }

        public void setSuite(CardSuite newSuite)
        {
            suite = newSuite;
        }

        public String getName()
        {
            return name;
        }

        public void setName(String newName)
        {
            name = newName;
        }
    }
}
