using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TestTextEditor.Framework.Utils;
using TestTextEditor.Tests.TestData;

namespace TestTextEditor.Tests.DataProviders
{
    public class FileMenuProviders
    {
        public static IEnumerable SaveAsProviders
        {
            get
            {
                var i = 1;
                foreach (var text in BaseTestObjects.BaseMultilineTexts.Select(t => t.Item1))
                {
                    yield return new TestCaseData(text, EnvironmentHelper.GetOutputPath("test.txt"))
                        .SetName($"TxtTest{i}" + "_{m}");
                    yield return new TestCaseData(text, EnvironmentHelper.GetOutputPath("test.cs"))
                        .SetName($"CsTest{i}" + "_{m}");
                    yield return new TestCaseData(text, EnvironmentHelper.GetOutputPath("test.java"))
                        .SetName($"JavaTest{i}" + "_{m}");
                    i++;
                }
            }
        }

        public static IEnumerable SaveAsCurrentOpenedFileProviders
        {
            get
            {
                foreach (var (fileName, testName) in new[]
                {
                    ("tEsTfiLe.txt", "DifferentCases"),
                    ("test", "NoExtension"),
                    ("s p a c e s.txt", "Spaces"),
                    (".hidden.txt", "Hidden"),
                    ("12312312.txt", "Numbers"),
                    ("tEsTfiLe.txt", "DifferentCasesName"),
                    ("java.java", "OtherExtension")
                })
                {
                    yield return new TestCaseData(EnvironmentHelper.GetOutputPath(fileName))
                        .SetName(testName + "_{m}");
                }
            }
        }

        public static IEnumerable OpenFileProviders
        {
            get
            {
                foreach (var (fileName, testName) in new[]
                {
                    ("small.txt", "Small"),
                    ("large.txt", "Large"),
                    ("one_line.txt", "OneLine"),
                    ("many_lines.txt", "ManyLines"),
                    ("empty.txt", "Empty"),
                    ("empty_lines.txt", "EmptyLines"),
                    ("spaces.txt", "Spaces"),
                    ("csharp.cs", "CSharp"),
                    ("java.java", "Java"),
                    ("clojure.clj", "Clojure")
                })
                {
                    yield return new TestCaseData(EnvironmentHelper.GetResourcePath(fileName))
                        .SetName(testName + "_{m}");
                }
            }
        }

        public static IEnumerable SaveAfterSaveAsProviders
        {
            get
            {
                var textToInsert = BaseTestObjects.TextToInsertSelectedTests;
                var textToChange = new List<string>(
                    new[]
                    {
                        "123123",
                        "323233232",
                        "abc"
                    });

                yield return new TestCaseData(
                        EnvironmentHelper.GetOutputPath("test.txt"),
                        textToInsert,
                        textToChange)
                    .SetName("TestCase" + "_{m}");
            }
        }
    }
}