using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plateRecognize.Helper
{
    internal class LetterCounter
    {
        private Dictionary<char, int> letterCount;

        public LetterCounter()
        {
            letterCount = new Dictionary<char, int>();
        }

        public void CountLetters(string inputString)
        {
            foreach (char c in inputString)
            {
                if (char.IsLetter(c))
                {
                    char lowercaseC = char.ToLower(c); // Convert to lowercase for case-insensitive counting
                    if (letterCount.ContainsKey(lowercaseC))
                    {
                        letterCount[lowercaseC]++;
                    }
                    else
                    {
                        letterCount[lowercaseC] = 1;
                    }
                }
            }
        }

        public void DisplayLetterCounts()
        {
            foreach (var kvp in letterCount)
            {
                Console.WriteLine($"Letter: {kvp.Key}, Count: {kvp.Value}");
            }
        }
    }
}
