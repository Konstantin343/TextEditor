using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using TextEditComponent.TextEditComponent.Helpers;
using TextEditComponent.TextEditComponent.Text;
using Utils;

namespace TextEditComponent.TextEditComponent
{
    public class TextEditContextMenuModel
    {
        public event EventHandler Copy;
        public event EventHandler Cut;
        public event EventHandler Paste;
        public event EventHandler SelectAll;
        public TextEditBoxModel Owner { get; }

        public TextEditContextMenuModel(TextEditBoxModel owner) =>
            Owner = owner;

        private ICommand _selectAllCommand;

        public ICommand SelectAllCommand =>
            _selectAllCommand ??
            (_selectAllCommand = new RelayCommand(obj =>
            {
                Owner.SelectedText.MouseSelectionStart = new TextPosition();
                Owner.SelectedText.MouseSelectionEnd =
                    new TextPosition(Owner.TextLines.Count - 1,
                        Owner.TextLines[Owner.TextLines.Count - 1].Length);

                SelectAll?.Invoke(this, EventArgs.Empty);
            }));

        private ICommand _сutCommand;

        public ICommand CutCommand =>
            _сutCommand ??
            (_сutCommand = new RelayCommand(obj =>
            {
                ClipboardHelper.SetText(Owner.TextLines.GetInBounds(Owner.SelectedText));
                Owner.DeleteSelectedText();

                Cut?.Invoke(this, EventArgs.Empty);
            }));


        private ICommand _сopyCommand;

        public ICommand CopyCommand =>
            _сopyCommand ??
            (_сopyCommand = new RelayCommand(obj =>
            {
                ClipboardHelper.SetText(Owner.TextLines.GetInBounds(Owner.SelectedText));

                Copy?.Invoke(this, EventArgs.Empty);
            }));

        private ICommand _pasteCommand;

        public ICommand PasteCommand =>
            _pasteCommand ??
            (_pasteCommand = new RelayCommand(obj =>
            {
                Owner.DeleteSelectedText();
                Owner.AddLinesOnCurrentPosition(new List<string>(Regex.Split(ClipboardHelper.GetText(), "\r\n")));
                Owner.SelectedText.Invalidate();

                Paste?.Invoke(this, EventArgs.Empty);
            }));
    }
}