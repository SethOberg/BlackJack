using System;
namespace UtilitiesLibrary.ConvertString
{
    public class StringToNumbers
    {
        public StringToNumbers()
        {
        }

        public static bool stringToIntOk(string text)
        {
            bool successful = false;

            successful = Int32.TryParse(text, out int result);

            if (successful)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public static int convertToInt(string number)
        {
            Int32.TryParse(number, out int result);
            return result;
        }

        public bool convertToInt(string number, int min, int max)
        {
            Int32.TryParse(number, out int result);

            if(min <= result && result <= max)
            {
                return true;
            } else
            {
                return false;
            }
        }


        public static bool stringToFloatOk(string text)
        {
            bool successful = false;

            successful = float.TryParse(text, out float result);

            if (successful)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public static float convertToFloat(string number)
        {
            float.TryParse(number, out float result);
            return result;
        }

        public static bool convertToFloat(string number, float min, float max)
        {
            float.TryParse(number, out float result);

            if (min <= result && result <= max)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
