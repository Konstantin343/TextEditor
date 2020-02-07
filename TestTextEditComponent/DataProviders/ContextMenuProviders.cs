using System.Collections;
using System.Linq;
using NUnit.Framework;
using TestTextEditComponent.TestData;
using TestTools.Utils;
using TextEditComponent.TextEditComponent.Text;

namespace TestTextEditComponent.DataProviders
{
    public class ContextMenuProviders
    {
        public static IEnumerable SelectAllProviders
        {
            get
            {
                foreach (var (text, testName) in BaseTestsObjects.Texts)
                {
                    var expectedBounds = new SelectedTextBounds(
                        new TextPosition(0, 0),
                        new TextPosition(text.Count - 1, text.Last().Length));
                    yield return new TestCaseData(text, expectedBounds)
                        .SetName(testName + "_{m}");
                }
            }
        }

        public static IEnumerable CopyCutProviders
        {
            get
            {
                var bounds = BaseTestsObjects.Bounds.Take(7);

                foreach (var bound in bounds)
                {
                    var (sf, cf, st, ct) =
                        (bound.MouseSelectionStart.Str, bound.MouseSelectionStart.Chr,
                            bound.MouseSelectionEnd.Str, bound.MouseSelectionEnd.Chr);
                    var expectedText = TextHelper.GetTextInBounds(BaseTestsObjects.TextLines,
                        sf, cf, st, ct);
                    yield return new TestCaseData(BaseTestsObjects.TextLines, expectedText, bound)
                        .SetName(
                            $"Bound_From_{sf}_{cf}_To_{st}_{ct}" + "_{m}");
                }
            }
        }

        public static IEnumerable CutRemainingProviders
        {
            get
            {
                var bounds = BaseTestsObjects.Bounds.Take(7);

                foreach (var bound in bounds)
                {
                    var (sf, cf, st, ct) =
                        (bound.MouseSelectionStart.Str, bound.MouseSelectionStart.Chr,
                            bound.MouseSelectionEnd.Str, bound.MouseSelectionEnd.Chr);
                    var expectedText = TextHelper.GetSplittedTextNotInBounds(BaseTestsObjects.TextLines,
                        sf, cf, st, ct);
                    yield return new TestCaseData(BaseTestsObjects.TextLines, expectedText, bound)
                        .SetName(
                            $"Bound_From_{sf}_{cf}_To_{st}_{ct}" + "_{m}");
                }
            }
        }

        public static IEnumerable PasteProviders
        {
            get
            {
                var textLines = BaseTestsObjects.TextLines;
                var positions = BaseTestsObjects.Positions;

                foreach (var position in positions)
                {
                    var expectedText = TextHelper.SplittedInsertLinesInText(
                        textLines, textLines, position.Str, position.Chr);
                    var toPaste = string.Join("\r\n", textLines);
                    yield return new TestCaseData(textLines, toPaste, expectedText, position)
                        .SetName($"Position_{position.Str}_{position.Chr}" + "_{m}");
                }
            }
        }

        public static IEnumerable PasteSelectedProviders
        {
            get
            {
                var textLines = BaseTestsObjects.TextLines;
                var bounds = BaseTestsObjects.Bounds;

                foreach (var bound in bounds)
                {
                    var (sf, cf, st, ct) =
                        (bound.MouseSelectionStart.Str, bound.MouseSelectionStart.Chr,
                            bound.MouseSelectionEnd.Str, bound.MouseSelectionEnd.Chr);
                    var remainingText = TextHelper.GetSplittedTextNotInBounds(BaseTestsObjects.TextLines,
                        bound.RealStart.Str, bound.RealStart.Chr, bound.RealEnd.Str, bound.RealEnd.Chr);
                    var expectedText = TextHelper.SplittedInsertLinesInText(remainingText,
                        textLines, bound.RealStart.Str, bound.RealStart.Chr);
                    var toPaste = string.Join("\r\n", textLines);
                    yield return new TestCaseData(BaseTestsObjects.TextLines, toPaste, expectedText, bound)
                        .SetName(
                            $"Bound_From_{sf}_{cf}_To_{st}_{ct}" + "_{m}");
                }
            }
        }
    }
}