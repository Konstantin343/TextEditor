using TestStack.White.UIItems.Finders;
using TestStack.White.UIItems.WindowItems;
using TestStack.White.UIItems.WPFUIItems;
using TestTextEditor.Framework.Forms;
using TestTextEditor.Framework.Forms.MenuForms;
using TestTextEditor.Framework.Forms.ModalForns;
using TestTextEditor.Framework.Forms.TextForms;

namespace TestTextEditor.Framework.Windows
{
    public class MainTextEditorWindow
    {
        private static readonly SearchCriteria FileMenuSearchCriteria =
            SearchCriteria.ByAutomationId("File");

        private static readonly SearchCriteria ContextMenuSearchCriteria =
            SearchCriteria.ByAutomationId("ContextMenu");

        private static readonly SearchCriteria CurrentOpenedFileSearchCriteria =
            SearchCriteria.ByAutomationId("OpenedFile");

        private static readonly SearchCriteria TextEditBoxSearchCriteria =
            SearchCriteria.ByAutomationId("TextEditBox");

        private static readonly string SaveFileAsModalWindowTitle = "Save File As";
        
        private static readonly string OpenFileModalWindowTitle = "Open File";

        private Window _source;
        
        public MainTextEditorWindow(Window source)
        {
            _source = source;
        }

        public Window Source => _source;
        
        public string Title => _source.Title;

        public TextEditBoxForm TextEditBoxForm =>
            new TextEditBoxForm(_source.Get(TextEditBoxSearchCriteria), 
                "Text edit box");

        public ContextMenuForm ContextMenuForm =>
            new ContextMenuForm(_source.Popup.Get(ContextMenuSearchCriteria), 
                "Context menu");

        public FileMenuForm FileMenuForm =>
            new FileMenuForm(_source.Get(FileMenuSearchCriteria), 
                "File menu");

        public ModalForm SaveFileAsModalForm =>
            new ModalForm(_source.ModalWindow(SaveFileAsModalWindowTitle),
                "Save file as modal window");
        
        public ModalForm OpenFileModalForm =>
            new ModalForm(_source.ModalWindow(OpenFileModalWindowTitle),
                "Open file as modal window");

        public string CurrentOpenedFile => _source.Get(CurrentOpenedFileSearchCriteria).Name;
    }
}