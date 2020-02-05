using System.Collections.Generic;
using System.Linq;
using TestStack.White.UIItems;
using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WPFUIItems;
using TestTools.Logger;

namespace TestTextEditor.Framework.Forms.MenuForms
{
    public class ThemesMenuForm : BaseMenuForm
    {
        public ThemesMenuForm(IUIItem uiItem, string name) : base(uiItem, name)
        {
        }

        public void SelectTheme(string theme) => SelectItem(theme);

        public IList<string> AllThemes
        {
            get
            {
                TestLogger.Instance.Info($"Get all themes from '{_name}'");
                return _source.GetMultiple(SearchCriteria.All)
                    .Select(ui => ui.Name)
                    .ToList();
            }
        }

        public string SelectedTheme => _source.Name;

    }
}