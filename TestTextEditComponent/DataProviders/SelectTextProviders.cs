using System.Collections;
using System.Linq;
using NUnit.Framework;
using TestTextEditComponent.TestData;
using TestTools.Utils;

namespace TestTextEditComponent.DataProviders
{
    public class SelectTextProviders
    {
        public static IEnumerable SelectedBoundsProvider =>
            BaseTestsObjects.Bounds.Select(bound => new TestCaseData(BaseTestsObjects.TextLines, bound)
                .SetName(
                    $"Bound_From_{bound.MouseSelectionStart.Str}_{bound.MouseSelectionStart.Chr}_" +
                    $"To_{bound.MouseSelectionEnd.Str}_{bound.MouseSelectionEnd.Chr}" + "_{m}"));

        public static IEnumerable DeleteSelectedTextProvider
        {
            get
            {
                foreach (var bound in BaseTestsObjects.Bounds)
                {
                    var (sf, cf, st, ct) =
                        (bound.RealStart.Str, bound.RealStart.Chr, bound.RealEnd.Str, bound.RealEnd.Chr);

                    var expectedText = TextHelper.GetSplittedTextNotInBounds(
                        BaseTestsObjects.TextLines, sf, cf, st, ct);

                    yield return new TestCaseData(BaseTestsObjects.TextLines, expectedText, bound)
                        .SetName(
                            $"Bound_From_{bound.MouseSelectionStart.Str}_{bound.MouseSelectionStart.Chr}_" +
                            $"To_{bound.MouseSelectionEnd.Str}_{bound.MouseSelectionEnd.Chr}" + "_{m}");
                }
            }
        }
    }
}