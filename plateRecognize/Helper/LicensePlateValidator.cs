using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace plateRecognize.Helper
{
    internal class LicensePlateValidator
    {
        // Define a regular expression pattern for Irish license plates
        private static readonly string Pattern = @"^[A-Z]{2}\d{1,2}[A-Z]{1,2}\d?$";
        private static readonly string SpacePattern = @"^\s+|\s+$";
        private static readonly string HypenPattern = @"[^a-zA-Z0-9\-]";

        // Validate an Irish license plate
        public static string ValidateIrishLicensePlate(string licensePlate)
        {
            //Console.WriteLine(ExtractLast4Digits(licensePlate));
            var plate = Regex.Replace(licensePlate, SpacePattern, ""); 
            //  && Regex.IsMatch(plate, Pattern)
            if (CheckStartNumber(plate) && !Regex.IsMatch(plate, HypenPattern))
                return plate;
            else
                return null;
        }

        // Extract the last 4 digits from the license plate in the text
        public static string ExtractLast4Digits(string text)
        {
            MatchCollection matches = Regex.Matches(text, Pattern);

            if (matches.Count > 0)
            {
                string lastLicensePlate = matches[matches.Count - 1].Value;
                int length = lastLicensePlate.Length;
                if (length >= 5)
                {
                    return lastLicensePlate.Substring(length - 6);
                }
            }

            // If no matching license plate is found, return null or handle the case as needed
            return null;
        }

        public static bool CheckStartNumber(string text)
        {
            // Define a regular expression pattern to match data starting with a number
            string pattern = @"\d[\d\.,]*\b";

            // Use Regex to find all matching data in the text
            MatchCollection matches = Regex.Matches(text, pattern);

            return matches.Count > 0 ? true : false; 
        }
    }
}
