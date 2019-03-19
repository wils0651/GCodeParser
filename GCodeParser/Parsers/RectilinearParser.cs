using GCodeParser.Models;
using System;
using System.Collections.Generic;

namespace GCodeParser.Parsers
{
    public class RectilinearParser : IRectilinearParser
    {
        private Dictionary<char, float> _coordinateDictionary;

        public RectilinearParser()
        {
            _coordinateDictionary = new Dictionary<char, float>();
        }

        public RectilinearMove ParseMove(string parameters)
        {
            parameters = parameters.Trim();

            if (string.IsNullOrWhiteSpace(parameters))
            {
                throw new Exception("G0 Needs a parameter string");
            }

            var parameterCharacterArray = parameters.ToUpper().ToCharArray();

            int position = 0;
            while(position < parameterCharacterArray.Length)
            {
                position = ParseCoordinate(parameterCharacterArray, position);
            }

            float x = _coordinateDictionary.TryGetValue('X', out float xResult) ? xResult : 0.0F;
            float y = _coordinateDictionary.TryGetValue('Y', out float yResult) ? yResult : 0.0F;
            float z = _coordinateDictionary.TryGetValue('Z', out float zResult) ? zResult : 0.0F;
            float speed = _coordinateDictionary.TryGetValue('F', out float fResult) ? fResult : 0.0F;

            return new RectilinearMove(x, y, z, speed);
        }

        private int ParseCoordinate(char[] characters, int position)
        {
            int initialPosition = position;
            if(char.IsDigit(characters[initialPosition]))
            {
                throw new Exception("should be a character");
            }

            position++;
            string coordinateValue = string.Empty;
            while (position < characters.Length && IsValidNumericCharacter(characters[position]))
            {
                coordinateValue += characters[position];
                position++;
            }

            if (float.TryParse(coordinateValue, out float result))
            {
                _coordinateDictionary.Add(characters[initialPosition], result);
            }

            return position;
        }

        private bool IsValidNumericCharacter(char character)
        {
            return char.IsDigit(character) || character.Equals('.') || character.Equals('-');
        }
    }
}
