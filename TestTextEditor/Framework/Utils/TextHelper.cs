using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TestTextEditor.Framework.Utils
{
    public static class TextHelper
    {
        private const string LineCharacters = "abcdefghijklmnopqrstuvwxyz0123456789~!@#$%^&*()-_=+/,.<>{}[]|;:?\"'`  ";
        private static readonly Random Random = new Random();

        public static string GenerateRandom(int length = 30)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < length; i++)
            {
                sb.Append(LineCharacters[Random.Next(LineCharacters.Length - 1)]);
            }

            return sb.ToString();
        }

        public static IList<string> GetText(int stringsCount, int stringsLength)
        {
            var textToInsert = new List<string>();
            for (var i = 0; i < stringsCount; i++)
            {
                textToInsert.Add(GenerateRandom(stringsLength));
            }

            return textToInsert;
        }

        public static string GetTextNotInBounds(
            IList<string> textToInsert,
            int strFrom, int chrFrom,
            int strTo, int chrTo,
            string joinMiddleLineBy = "")
        {
            var beginLines = textToInsert.Where((s, i) => i < strFrom).ToList();
            var endLines = textToInsert.Where((s, i) => i > strTo).ToList();
            var middleLine = textToInsert[strFrom].Substring(0, chrFrom) + joinMiddleLineBy +
                             textToInsert[strTo].Substring(chrTo);
            return string.Join("\n", beginLines) +
                   (beginLines.Any() ? "\n" : "") +
                   middleLine +
                   (endLines.Any() ? "\n" : "") +
                   string.Join("\n", endLines);
        }

        public static string GetTextInBounds(
            IList<string> textToInsert,
            int strFrom, int chrFrom,
            int strTo, int chrTo)
        {
            string expectedText;
            if (strFrom == strTo)
            {
                expectedText = textToInsert[strFrom].Substring(chrFrom, chrTo - chrFrom);
            }
            else
            {
                var middleLines = textToInsert.Where((s, i) => i > strFrom && i < strTo).ToList();
                expectedText = textToInsert[strFrom].Substring(chrFrom) + "\n" +
                               string.Join("\n", middleLines) +
                               (middleLines.Any() ? "\n" : "") +
                               textToInsert[strTo].Substring(0, chrTo);
            }

            return expectedText;
        }
    }
}