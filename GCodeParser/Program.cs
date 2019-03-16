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
            string filePath = $"C:\\Users\\Tim\\Documents\\GitHub\\GCodeParser\\GCodeParser\\TestGCode\\spindleLeveler.nc";

            // Create Parser
            ICommandParser parser = new CommandParser();

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
                        line.Trim();
                        Console.WriteLine("Line " + count + ": " + line);
                        count++;
                        var thing = parser.ParseCommand(line);
                        Console.WriteLine("GCodeCommand: " + thing.GCodeCommand + "; ParameterString: " + thing.ParameterString);
                        // Pass each line to parser
                        while (!thing.GCodeCommand.Equals("NOP"))
                        {
                            thing = parser.ParseCommand(thing.ParameterString);
                            if (!thing.GCodeCommand.Equals("NOP"))
                            {
                                Console.WriteLine("SubParse GCodeCommand: " + thing.GCodeCommand + "; ParameterString: " + thing.ParameterString);
                            }
                        }
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
