using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using TextEditor.Highlight;
using TextEditor.TextEditComponent.Constants;
using TextEditor.TextEditComponent.Text;
using TextEditor.TextEditComponent.TextHelpers;

namespace TextEditor.TextEditComponent
{
    public class TextEditBox : Control, IScrollInfo
    {
        #region DependencyProperties

        public static readonly DependencyProperty TextLinesProperty = DependencyProperty.Register(
            "TextLines",
            typeof(TextLines),
            typeof(TextEditBox),
            new FrameworkPropertyMetadata(
                null,
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty IsInsertKeyPressedProperty = DependencyProperty.Register(
            "IsInsertKeyPressed",
            typeof(bool),
            typeof(TextEditBox),
            new FrameworkPropertyMetadata(
                false,
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty NormalModePositionBrushProperty = DependencyProperty.Register(
            "NormalModePositionBrush",
            typeof(Brush),
            typeof(TextEditBox),
            new FrameworkPropertyMetadata(
                Brushes.DodgerBlue,
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty InsertModePositionBrushProperty = DependencyProperty.Register(
            "InsertModePositionBrush",
            typeof(Brush),
            typeof(TextEditBox),
            new FrameworkPropertyMetadata(
                Brushes.IndianRed,
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty PaddingLeftProperty = DependencyProperty.Register(
            "PaddingLeft",
            typeof(int),
            typeof(TextEditBox),
            new FrameworkPropertyMetadata(
                0,
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty BorderWidthProperty = DependencyProperty.Register(
            "BorderWidth",
            typeof(int),
            typeof(TextEditBox),
            new FrameworkPropertyMetadata(
                0,
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender));

        public TextLines TextLines
        {
            get => (TextLines) GetValue(TextLinesProperty);
            set => SetValue(TextLinesProperty, value);
        }

        public bool IsInsertKeyPressed
        {
            get => (bool) GetValue(IsInsertKeyPressedProperty);
            private set => SetValue(IsInsertKeyPressedProperty, value);
        }

        public Brush NormalModePositionBrush
        {
            get => (Brush) GetValue(NormalModePositionBrushProperty);
            set => SetValue(NormalModePositionBrushProperty, value);
        }

        public Brush InsertModePositionBrush
        {
            get => (Brush) GetValue(InsertModePositionBrushProperty);
            set => SetValue(InsertModePositionBrushProperty, value);
        }

        public int BorderWidth
        {
            get => (int) GetValue(BorderWidthProperty);
            set => SetValue(BorderWidthProperty, value);
        }

        public int PaddingLeft
        {
            get => (int) GetValue(PaddingLeftProperty);
            set => SetValue(PaddingLeftProperty, value);
        }

        #endregion

        public double HorizontalDelta { get; set; }
        public double VerticalDelta { get; set; }
        public double CaretHeightParameter { get; set; }

        public double LineHeight => TextLines.FontSize + TextLines.LineInterval;
        public double TextHeight => LineHeight * VerticalDelta + TextLines.TextHeight;
        public double TextWidth => PaddingLeft + HorizontalDelta + TextLines.MaxLineWidth;
        public double CaretHeight => CaretHeightParameter * TextLines.FontSize;
        public int CurrentString => CurrentPosition.Str;
        public int CurrentChar => CurrentPosition.Chr;
        public string Text => TextLines.ToString();
        public string SelectedTextString => TextLines.GetInBounds(SelectedText);

        public TextEditBox() => SetDefaultSettings();

        public void InvalidateAll()
        {
            SelectedText.Invalidate();
            CurrentPosition = new TextPosition();
            InvalidateVisual();
            TextLines.UpdateAll();
        }

        public void SetTextLines(IEnumerable<string> source)
        {
            TextLines.SetText(source);
            InvalidateAll();
        }

        #region Scroll

        public bool CanVerticallyScroll { get; set; }
        public bool CanHorizontallyScroll { get; set; }

        public double HorizontalOffset { get; private set; }
        public double VerticalOffset { get; private set; }
        public ScrollViewer ScrollOwner { get; set; }

        public double ExtentWidth => TextWidth;
        public double ExtentHeight => TextHeight;
        public double ViewportWidth => ActualWidth;
        public double ViewportHeight => ActualHeight;

        public void LineUp() => SetVerticalOffset(VerticalOffset - LineHeight);

        public void LineDown() => SetVerticalOffset(VerticalOffset + LineHeight);

        public void LineLeft() => SetHorizontalOffset(HorizontalOffset - HorizontalDelta);

        public void LineRight() => SetHorizontalOffset(HorizontalOffset + HorizontalDelta);

        public void PageUp() => SetVerticalOffset(VerticalOffset - ActualHeight);

        public void PageDown() => SetVerticalOffset(VerticalOffset + ActualHeight);

        public void PageLeft() => SetHorizontalOffset(HorizontalOffset - ActualWidth);

        public void PageRight() => SetHorizontalOffset(HorizontalOffset + ActualWidth);

        public void MouseWheelUp() => LineUp();

        public void MouseWheelDown() => LineDown();

        public void MouseWheelLeft() => LineLeft();

        public void MouseWheelRight() => LineRight();

        public void SetHorizontalOffset(double offset)
        {
            UpdateHorizontalOffsetBy(offset);
            InvalidateVisual();
        }

        public void SetVerticalOffset(double offset)
        {
            UpdateVerticalOffsetBy(offset);
            InvalidateVisual();
        }

        public Rect MakeVisible(Visual visual, Rect rectangle) => rectangle;

        #endregion

        #region OverridedMethods

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            if (Parent is ScrollViewer sv) ScrollOwner = sv;
            CreateContextMenu();
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);
            DrawBackground(drawingContext);
            DrawLines(drawingContext);
            ScrollOwner.InvalidateScrollInfo();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            Focus();
            CaptureMouse();
            var textPosition = GetCharByPixels(e.GetPosition(this));
            SelectedText.SetBounds(textPosition);
            CurrentPosition = textPosition;
            UpdateOffsetByCaretPosition();
            InvalidateVisual();
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            ReleaseMouseCapture();
            var textPosition = GetCharByPixels(e.GetPosition(this));
            SelectedText.MouseSelectionEnd = new TextPosition(textPosition);
            CurrentPosition = textPosition;
            InvalidateVisual();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (IsMouseOver && e.LeftButton == MouseButtonState.Pressed)
            {
                var textPosition = GetCharByPixels(e.GetPosition(this));
                SelectedText.MouseSelectionEnd = new TextPosition(textPosition);
                CurrentPosition = textPosition;
                UpdateOffsetByCaretPosition();
                InvalidateVisual();
            }
        }

        protected override void OnMouseRightButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseRightButtonDown(e);
            if (ContextMenu != null)
                ContextMenu.IsOpen = true;
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            InvalidateVisual();
        }

        protected override void OnTextInput(TextCompositionEventArgs e)
        {
            base.OnTextInput(e);
            if (string.IsNullOrEmpty(e.Text)) return;

            if (KeysCharacters.Backspace.Equals(e.Text))
            {
                BackspaceKey();
            }
            else
            {
                if (!SelectedText.IsEmpty)
                {
                    DeleteSelected();
                }

                if (KeysCharacters.Enter.Equals(e.Text))
                {
                    EnterKey();
                }
                else
                {
                    AddText(e.Text);
                }
            }


            UpdateOffsetByCaretPosition();
            InvalidateVisual();
            SelectedText.Invalidate();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.Key)
            {
                case Key.Left:
                    LeftKey();
                    break;
                case Key.Right:
                    RightKey();
                    break;
                case Key.Up:
                    UpKey();
                    break;
                case Key.Down:
                    DownKey();
                    break;
                case Key.Insert:
                    InsertKey();
                    break;
                case Key.Delete:
                    DeleteKey();
                    break;
                case Key.Tab:
                    TabKey();
                    break;
                default:
                    return;
            }

            UpdateOffsetByCaretPosition();
            SelectedText.Invalidate();
            InvalidateVisual();
        }

        protected override AutomationPeer OnCreateAutomationPeer() =>
            new TextEditBoxAutomationPeer(this);

        #endregion

        #region Private

        #region Keys

        private void DeleteKey()
        {
            if (!SelectedText.IsEmpty)
            {
                DeleteSelected();
                return;
            }

            if (CurrentChar == TextLines[CurrentString].Length)
            {
                if (CurrentString == TextLines.Count - 1) return;
                var nextString = CurrentString + 1;
                TextLines.AddInLine(CurrentString, TextLines[nextString].RawValue);
                TextLines.RemoveLineAt(nextString);
            }
            else
            {
                TextLines.RemoveInLine(CurrentString, CurrentChar, 1);
            }

            UpdateVerticalOffset();
            UpdateHorizontalOffset();
        }

        private void DownKey()
        {
            if (CurrentString == TextLines.Count - 1) return;
            CurrentPosition.Str++;
            CurrentPosition.Chr = Math.Min(CurrentChar, TextLines[CurrentString].Length);
        }

        private void UpKey()
        {
            if (CurrentString == 0) return;
            CurrentPosition.Str--;
            CurrentPosition.Chr = Math.Min(CurrentChar, TextLines[CurrentString].Length);
        }

        private void RightKey()
        {
            if (CurrentChar == TextLines[CurrentString].Length)
            {
                if (CurrentString == TextLines.Count - 1) return;
                CurrentPosition.Str++;
                CurrentPosition.Chr = 0;
                return;
            }

            CurrentPosition.Chr++;
        }

        private void LeftKey()
        {
            if (CurrentChar == 0)
            {
                if (CurrentString == 0) return;
                CurrentPosition.Str--;
                CurrentPosition.Chr = TextLines[CurrentString].Length;
                return;
            }

            CurrentPosition.Chr--;
        }

        private void InsertKey() => IsInsertKeyPressed = !IsInsertKeyPressed;

        private void BackspaceKey()
        {
            if (!SelectedText.IsEmpty)
            {
                DeleteSelected();
                return;
            }

            if (CurrentChar == 0)
            {
                if (CurrentString == 0) return;
                var newPosition = TextLines[CurrentString - 1].Length;
                TextLines.AddInLine(CurrentString - 1, TextLines[CurrentString].RawValue);
                TextLines.RemoveLineAt(CurrentString);
                CurrentPosition.Str--;
                CurrentPosition.Chr = newPosition;
            }
            else
            {
                TextLines.RemoveInLine(CurrentString, CurrentChar - 1, 1);
                CurrentPosition.Chr--;
            }

            UpdateVerticalOffset();
            UpdateHorizontalOffset();
        }

        private void EnterKey()
        {
            TextLines.InsertLine(
                CurrentString + 1,
                CurrentChar < TextLines[CurrentString].Length
                    ? TextLines[CurrentString].Substring(CurrentChar)
                    : string.Empty);
            if (CurrentChar < TextLines[CurrentString].Length)
                TextLines.RemoveInLine(CurrentString, CurrentChar);
            CurrentPosition.Str++;
            CurrentPosition.Chr = 0;
        }

        private void TabKey()
        {
            if (!SelectedText.IsEmpty)
            {
                DeleteSelected();
            }
            TextLines.InsertInLine(CurrentString, "\t", CurrentChar);
            CurrentPosition.Chr++;
        }

        #endregion

        #region Drawing

        private void DrawLines(DrawingContext dc)
        {
            var lowerBound = Math.Max(0, VerticalOffset / LineHeight - 1);
            var upperBound = Math.Min(TextLines.Count - 1, (ActualHeight + VerticalOffset) / LineHeight + 1);
            TextLines.Underline(SelectedText);
            for (var i = (int) lowerBound; i <= (int) upperBound; i++)
            {
                DrawLine(i, dc);
            }

            TextLines.RemoveDecoration(SelectedText);
        }

        private void DrawLine(int index, DrawingContext dc)
        {
            var formattedText = TextLines[index].FormattedValue;
            if (IsFocused && index == CurrentString)
            {
                var brush = IsInsertKeyPressed ? InsertModePositionBrush : NormalModePositionBrush;
                var caretPoint = GetCaretPoint();
                dc.DrawRectangle(brush, new Pen(),
                    new Rect(caretPoint.X, caretPoint.Y, 1, CaretHeight));
            }

            dc.DrawText(formattedText,
                new Point(PaddingLeft - HorizontalOffset, LineHeight * index - VerticalOffset));
        }

        private void DrawBackground(DrawingContext dc)
        {
            dc.DrawRectangle(Background, new Pen(BorderBrush, BorderWidth),
                new Rect(0, 0, ActualWidth, ActualHeight));
        }

        #endregion

        #region ContextMenu

        private void CreateContextMenu()
        {
            var copyItem = new MenuItem {Header = "Copy", Uid = "Copy"};
            copyItem.Click += CopySelected;
            var cutItem = new MenuItem {Header = "Cut", Uid = "Cut"};
            cutItem.Click += CutSelected;
            var pasteItem = new MenuItem {Header = "Paste", Uid = "Paste"};
            pasteItem.Click += PasteSelected;
            var selectAllItem = new MenuItem {Header = "Select all", Uid = "SelectAll"};
            selectAllItem.Click += SelectAll;
            ContextMenu = new ContextMenu
            {
                Placement = PlacementMode.MousePoint,
                ItemsSource = new[] {copyItem, cutItem, pasteItem, selectAllItem},
                Uid = "ContextMenu"
            };
        }

        private void SelectAll(object sender, RoutedEventArgs e)
        {
            SelectedText.MouseSelectionStart = new TextPosition();
            SelectedText.MouseSelectionEnd =
                new TextPosition(TextLines.Count - 1, TextLines[TextLines.Count - 1].Length);
            InvalidateVisual();
        }

        private void CutSelected(object sender, RoutedEventArgs e)
        {
            CopySelected(sender, e);
            DeleteSelected();
            UpdateOffsetByCaretPosition();
            InvalidateVisual();
        }

        private void CopySelected(object sender, RoutedEventArgs e) =>
            Clipboard.SetText(TextLines.GetInBounds(SelectedText));

        private void PasteSelected(object sender, RoutedEventArgs e)
        {
            if (!SelectedText.IsEmpty) DeleteSelected();
            var textLinesToPaste = Clipboard.GetText().Split('\n');
            for (var i = 0; i < textLinesToPaste.Length; i++)
            {
                AddText(textLinesToPaste[i]);
                if (i != textLinesToPaste.Length - 1)
                    EnterKey();
            }

            UpdateOffsetByCaretPosition();
            SelectedText.Invalidate();
            InvalidateVisual();
        }

        #endregion

        private void AddText(string text)
        {
            if (IsInsertKeyPressed && CurrentChar < TextLines[CurrentString].Length)
                TextLines.RemoveInLine(CurrentString, CurrentChar, text.Length);
            TextLines.InsertInLine(CurrentString, text, CurrentChar);
            CurrentPosition.Chr += text.Length;
        }


        private Point GetCaretPoint() =>
            new Point(
                FormattedTextHelper.GetWidth(TextLines.SubstringFromLine(CurrentString, 0, CurrentChar),
                    TextLines.FontStyle, TextLines.FontSize)
                + PaddingLeft
                - HorizontalOffset,
                LineHeight * CurrentString - VerticalOffset);

        private void UpdateOffsetByCaretPosition()
        {
            var caretPoint = GetCaretPoint();
            if (caretPoint.X < 0)
            {
                HorizontalOffset += caretPoint.X;
            }
            else if (caretPoint.X > ActualWidth)
            {
                HorizontalOffset -= (ActualWidth - caretPoint.X);
            }


            if (caretPoint.Y < 0)
            {
                VerticalOffset += caretPoint.Y;
            }
            else if (caretPoint.Y + CaretHeight > ActualHeight)
            {
                VerticalOffset -= (ActualHeight - (caretPoint.Y + CaretHeight));
            }
        }

        private void UpdateHorizontalOffsetBy(double offset)
        {
            HorizontalOffset = Math.Min(offset, TextWidth - ActualWidth);
            HorizontalOffset = Math.Max(HorizontalOffset, 0);
        }

        private void UpdateHorizontalOffset() => UpdateHorizontalOffsetBy(HorizontalOffset);

        private void UpdateVerticalOffsetBy(double offset)
        {
            VerticalOffset = Math.Min(offset, TextHeight - ActualHeight);
            VerticalOffset = Math.Max(VerticalOffset, 0);
        }

        private void UpdateVerticalOffset() => UpdateVerticalOffsetBy(VerticalOffset);

        private TextPosition GetCharByPixels(Point point)
        {
            var (realX, realY) = (point.X + HorizontalOffset, point.Y + VerticalOffset);
            var stringNumber = Math.Min((int) (realY / LineHeight), TextLines.Count - 1);
            stringNumber = Math.Max(0, stringNumber);
            var currentLength = 0d;
            var lastCharWidth = 0d;

            var i = 0;
            while (i < TextLines[stringNumber].Length && currentLength < realX)
            {
                lastCharWidth =
                    FormattedTextHelper.GetWidth(TextLines[stringNumber][i],
                        TextLines.FontStyle,
                        TextLines.FontSize);
                currentLength += lastCharWidth;
                i++;
            }

            if (realX > currentLength)
                return new TextPosition(stringNumber, i);
            if (currentLength - realX > realX - (currentLength - lastCharWidth))
                i--;
            i = Math.Max(i, 0);
            i = Math.Min(i, TextLines[stringNumber].Length);
            return new TextPosition(stringNumber, i);
        }

        private void DeleteSelected()
        {
            TextLines.DeleteInBounds(SelectedText);
            CurrentPosition = new TextPosition(SelectedText.RealStart.Str, SelectedText.RealStart.Chr);
            UpdateOffsetByCaretPosition();
            UpdateVerticalOffset();
            UpdateHorizontalOffset();
            SelectedText.Invalidate();
            InvalidateVisual();
        }

        private void SetDefaultSettings()
        {
            HorizontalDelta = 10;
            VerticalDelta = 5;
            CaretHeightParameter = 1.2;
            PaddingLeft = Settings.PaddingLeft;
            BorderWidth = 1;
            BorderBrush = Brushes.LightSkyBlue;
            Background = Brushes.White;
            VerticalAlignment = VerticalAlignment.Stretch;
            HorizontalAlignment = HorizontalAlignment.Stretch;
            Focusable = true;
            SelectedText = new SelectedTextBounds();
            TextLines = new TextLines(
                new[] {""},
                Settings.FontStyle,
                Settings.FontSize,
                Brushes.Black,
                Settings.LineInterval,
                new HighlightTextManager(BasicWordsToHighlight.JavaWords, Brushes.DarkBlue));
        }

        private SelectedTextBounds SelectedText { get; set; }

        private TextPosition CurrentPosition { get; set; }

        #endregion
    }
}