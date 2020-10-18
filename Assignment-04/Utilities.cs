using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_04
{
    class Utilities
    {
        static Random RandomNumber = new Random();

        /*
         * Name: GenerateString()
         * Description: this method generates random string of certain length
         * Parameter: int stringLength. the desired length of the string
         * Return Value: generated string
         */
        static public string GenerateString(int stringLength)
        {
            string generatedString = "";
            if (stringLength > 0)
                for (int j = 0; j < stringLength; j++)
                {
                    //concatinate the random characters of generated
                    generatedString += (char)RandomNumber.Next('A', 'Z' + 1);
                }

            return generatedString;
        }
    }
}
