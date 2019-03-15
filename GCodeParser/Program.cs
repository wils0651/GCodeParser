using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCodeParser
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = $"C:\\Users\\Tim\\Documents\\GitHub\\GCodeParser\\GCodeParser\\TestGCode\\hugger1_2.ngc";

            // Create Parser

            try
            {   // Open the text file using a stream reader.
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    int count = 0;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.WriteLine("Line " + count + ": " + line);
                        count++;
                        // Pass each line to parser
                    }
                }
            }
            catch (IOException e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }

            // Open the file

            // get the words

            // parse

            // output results

            Console.ReadLine();
        }



    }
}
