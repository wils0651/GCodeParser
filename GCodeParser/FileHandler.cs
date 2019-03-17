using GCodeParser.Interfaces;
using System;
using System.IO;

namespace GCodeParser
{
    public class FileHandler : IFileHandler
    {
        private ICommandParser _parser;

        public FileHandler(ICommandParser commandParser)
        {
            _parser = commandParser;
        }
        public void ProcessFile(string filePath)
        {

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
                        var parsedCommand = _parser.ParseCommand(line);
                        Console.WriteLine("GCodeCommand: " + parsedCommand.GCodeCommand + "; ParameterString: " + parsedCommand.ParameterString);
                        // Pass each line to parser
                        while (!parsedCommand.GCodeCommand.Equals("NOP"))
                        {
                            parsedCommand = _parser.ParseCommand(parsedCommand.ParameterString);
                            if (!parsedCommand.GCodeCommand.Equals("NOP"))
                            {
                                Console.WriteLine("SubParse GCodeCommand: " + parsedCommand.GCodeCommand + "; ParameterString: " + parsedCommand.ParameterString);
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
        }
    }
}
