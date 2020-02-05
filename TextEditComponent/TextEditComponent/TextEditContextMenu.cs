using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using TextEditComponent.TextEditComponent.Text;
using Utils;

namespace TextEditComponent.TextEditComponent
{
    public class TextEditContextMenu : ContextMenu
    {
        public TextEditBox Owner { get; }

        public TextEditContextMenu(TextEditBox owner)
        {
            Owner = owner;
            Placement = PlacementMode.MousePoint;
            ItemsSource = new[]
            {
                new MenuItem {Header = "Copy", Uid = "Copy", Command = CopyCommand},
                new MenuItem {Header = "Cut", Uid = "Cut", Command = CutCommand},
                new MenuItem {Header = "Paste", Uid = "Paste", Command = PasteCommand},
                new MenuItem {Header = "Select all", Uid = "SelectAll", Command = SelectAllCommand}
            };
            Uid = "ContextMenu";
        }

        private ICommand _selectAllCommand;

        public ICommand SelectAllCommand =>
            _selectAllCommand ??
            (_selectAllCommand = new RelayCommand(obj =>
            {
                Owner.SelectedTextBounds.MouseSelectionStart = new TextPosition();
                Owner.SelectedTextBounds.MouseSelectionEnd =
                    new TextPosition(Owner.LinesCount - 1,
                        Owner.TextLines[Owner.LinesCount - 1].Length);
                Owner.InvalidateVisual();
            }));

        private ICommand _сutCommand;

        public ICommand CutCommand =>
            _сutCommand ??
            (_сutCommand = new RelayCommand(obj =>
            {
                CopyCommand.Execute(obj);
                Owner.DeleteSelected();

                Owner.UpdateOffsetByCaretPosition();
                Owner.InvalidateVisual();
            }));


        private ICommand _сopyCommand;

        public ICommand CopyCommand =>
            _сopyCommand ??
            (_сopyCommand = new RelayCommand(obj =>
                Clipboard.SetText(Owner.SelectedText)));

        private ICommand _pasteCommand;

        public ICommand PasteCommand =>
            _pasteCommand ??
            (_pasteCommand = new RelayCommand(obj =>
            {
                Owner.DeleteSelected();
                Owner.TextEditBoxModel.AddLinesOnCurrentPosition(Regex.Split(Clipboard.GetText(), "\r\n"));

                Owner.UpdateOffsetByCaretPosition();
                Owner.SelectedTextBounds.Invalidate();
                Owner.InvalidateVisual();
            }));
    }
}