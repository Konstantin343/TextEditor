using System.Collections.Generic;
using System.Linq;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WPFUIItems;
using TestTextEditor.Framework.Utils.Logger;

namespace TestTextEditor.Framework.Forms.MenuForms
{
    public class ThemesMenuForm : BaseMenuForm
    {
        public ThemesMenuForm(IUIItem uiItem, string name) : base(uiItem, name)
        {
        }

        public void SelectTheme(string theme) => SelectItem($"{theme}Theme");

        public IList<string> AllThemes
        {
            get
            {
                TestLogger.Instance.Info($"Get all themes from '{_name}'");
                return _source.GetMultiple(SearchCriteria.All)
                    .Select(ui => ui.Name.StartsWith(SelectedMarker)
                        ? ui.Name.Split(' ')[1]
                        : ui.Name)
                    .ToList();
            }
        }

        public string SelectedTheme =>
            _source.GetMultiple(SearchCriteria.All)
                .FirstOrDefault(ui => ui.Name.StartsWith(SelectedMarker))?.Name.Split(' ')[1];

        public bool IsOneSelectedTheme =>
            _source.GetMultiple(SearchCriteria.All)
                .Count(ui => ui.Name.StartsWith(SelectedMarker)) == 1;

        private const string SelectedMarker = "•";
    }
}