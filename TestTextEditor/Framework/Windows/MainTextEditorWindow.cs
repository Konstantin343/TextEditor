using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WPFUIItems;
using TestTextEditor.Framework.Forms;

namespace TestTextEditor.Framework.Windows
{
    public class MainTextEditorWindow
    {
        private static readonly SearchCriteria MenuFileSearchCriteria =
            SearchCriteria.ByAutomationId("File");

        private static readonly SearchCriteria MenuThemesSearchCriteria =
            SearchCriteria.ByAutomationId("Themes");

        private static readonly SearchCriteria CurrentOpenedFileSearchCriteria =
            SearchCriteria.ByAutomationId("OpenedFile");

        private static readonly SearchCriteria TextEditBoxSearchCriteria =
            SearchCriteria.ByAutomationId("TextEditBox");

        private static readonly SearchCriteria MenuFileOpenSearchCriteria =
            SearchCriteria.ByAutomationId("File");

        private static readonly SearchCriteria MenuFileSaveSearchCriteria =
            SearchCriteria.ByAutomationId("Save");

        private static readonly SearchCriteria MenuFileSaveAsSearchCriteria =
            SearchCriteria.ByAutomationId("SaveAs");

        public TextEditBoxForm TextEditBoxForm =>
            new TextEditBoxForm(TextEditorAppLoader.Window.Get(TextEditBoxSearchCriteria), "Text edit box");
        
        public ContextMenuForm ContextMenuForm => 
            new ContextMenuForm(TextEditorAppLoader.Window.Popup.Get(SearchCriteria.ByAutomationId("ContextMenu")), "Context menu");

        public string CurrentOpenedFile => TextEditorAppLoader.Window.Get(CurrentOpenedFileSearchCriteria).Name;
    }
}