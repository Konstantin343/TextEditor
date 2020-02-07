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

        public static IEnumerable CopyProviders
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
    }
}