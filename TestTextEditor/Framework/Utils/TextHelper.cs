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
            IList<string> text,
            int strFrom, int chrFrom,
            int strTo, int chrTo,
            string joinMiddleLineBy = "")
        {
            var beginLines = text.Where((s, i) => i < strFrom).ToList();
            var endLines = text.Where((s, i) => i > strTo).ToList();
            var middleLine = text[strFrom].Substring(0, chrFrom) + joinMiddleLineBy +
                             text[strTo].Substring(chrTo);
            return string.Join("\r\n", beginLines) +
                   (beginLines.Any() ? "\r\n" : "") +
                   middleLine +
                   (endLines.Any() ? "\r\n" : "") +
                   string.Join("\r\n", endLines);
        }

        public static string GetTextInBounds(
            IList<string> text,
            int strFrom, int chrFrom,
            int strTo, int chrTo)
        {
            string expectedText;
            if (strFrom == strTo)
            {
                expectedText = text[strFrom].Substring(chrFrom, chrTo - chrFrom);
            }
            else
            {
                var middleLines = text.Where((s, i) => i > strFrom && i < strTo).ToList();
                expectedText = text[strFrom].Substring(chrFrom) + "\r\n" +
                               string.Join("\r\n", middleLines) +
                               (middleLines.Any() ? "\r\n" : "") +
                               text[strTo].Substring(0, chrTo);
            }

            return expectedText;
        }

        public static string InsertLinesInText(
            IList<string> text,
            IList<string> lines,
            int str, int chr)
        {
            var expectedTest = new StringBuilder();
            for (var i = 0; i < text.Count; i++)
            {
                if (i == str)
                {
                    expectedTest.Append(text[i].Substring(0, chr));
                    for (var j = 0; j < lines.Count; j++)
                    {
                        expectedTest.Append(lines[j]);
                        if (j != lines.Count - 1)
                        {
                            expectedTest.Append("\r\n");
                        }
                    }

                    expectedTest.Append(text[i].Substring(chr));
                }
                else
                {
                    expectedTest.Append(text[i]);
                }

                if (i != text.Count - 1)
                {
                    expectedTest.Append("\r\n");
                }
            }

            return expectedTest.ToString();
        }
        
    }
}