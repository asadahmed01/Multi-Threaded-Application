using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_04
{
    class FileInput
    {
        Utilities Utilities = new Utilities();
        public static void WriteToFile(string fileName)
        {
            using (StreamWriter file =
            new StreamWriter(fileName + ".txt", true))
            {
                 file.WriteLine(Utilities.GenerateString(200));
                    //file.Close();
                
            }
            
        }
    }
}
