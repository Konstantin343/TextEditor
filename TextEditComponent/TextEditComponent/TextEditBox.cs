using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using TextEditComponent.TextEditComponent.Constants;
using TextEditComponent.TextEditComponent.Helpers;
using TextEditComponent.TextEditComponent.Services;
using TextEditComponent.TextEditComponent.Text;

namespace TextEditComponent.TextEditComponent
{
    public class TextEditBox : Control, IScrollInfo
    {
        #region DependencyProperties

        public static readonly DependencyProperty NormalModePositionBrushProperty = DependencyProperty.Register(
            nameof(NormalModePositionBrush),
            typeof(Brush),
            typeof(TextEditBox),
            new FrameworkPropertyMetadata(
                Brushes.DodgerBlue,
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty InsertModePositionBrushProperty = DependencyProperty.Register(
            nameof(InsertModePositionBrush),
            typeof(Brush),
            typeof(TextEditBox),
            new FrameworkPropertyMetadata(
                Brushes.IndianRed,
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty PaddingLeftProperty = DependencyProperty.Register(
            nameof(PaddingLeft),
            typeof(int),
            typeof(TextEditBox),
            new FrameworkPropertyMetadata(
                0,
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty BorderWidthProperty = DependencyProperty.Register(
            nameof(BorderWidth),
            typeof(int),
            typeof(TextEditBox),
            new FrameworkPropertyMetadata(
                0,
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender));

        public static readonly DependencyProperty HighlightBrushProperty = DependencyProperty.Register(
            nameof(HighlightBrush),
            typeof(Brush),
            typeof(TextEditBox),
            new FrameworkPropertyMetadata(
                Brushes.DarkBlue,
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender,
                OnHighlightBrushChanged)
        );

        private static void OnHighlightBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var teb = (TextEditBox) d;
            teb.FormattedTextService.HighlightTextService.HighlightBrush = (Brush) e.NewValue;
            teb.FormattedTextService?.UpdateAll();
            teb.InvalidateVisual();
        }

        public static readonly DependencyProperty TextBrushProperty = DependencyProperty.Register(
            nameof(TextBrush),
            typeof(Brush),
            typeof(TextEditBox),
            new FrameworkPropertyMetadata(
                Brushes.White,
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender,
                OnTextBrushChanged)
        );

        private static void OnTextBrushChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var teb = (TextEditBox) d;
            teb.FormattedTextService.TextBrush = (Brush) e.NewValue;
            teb.FormattedTextService?.UpdateAll();
            teb.InvalidateVisual();
        }

        public new static readonly DependencyProperty FontStyleProperty = DependencyProperty.Register(
            nameof(FontStyle),
            typeof(string),
            typeof(TextEditBox),
            new FrameworkPropertyMetadata(
                "Verdana",
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender,
                OnFontStyleChanged)
        );

        private static void OnFontStyleChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var teb = (TextEditBox) d;
            teb.FormattedTextService.FontStyle = (string) e.NewValue;
            teb.FormattedTextService?.UpdateAll();
            teb.InvalidateVisual();
        }

        public new static readonly DependencyProperty FontSizeProperty = DependencyProperty.Register(
            nameof(FontSize),
            typeof(double),
            typeof(TextEditBox),
            new FrameworkPropertyMetadata(
                14d,
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender,
                OnFontSizeChanged)
        );

        private static void OnFontSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var teb = (TextEditBox) d;
            teb.FormattedTextService.FontSize = (double) e.NewValue;
            teb.FormattedTextService?.UpdateAll();
            teb.InvalidateVisual();
        }

        public static readonly DependencyProperty LineIntervalProperty = DependencyProperty.Register(
            nameof(LineInterval),
            typeof(double),
            typeof(TextEditBox),
            new FrameworkPropertyMetadata(
                2d,
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender,
                OnLineIntervalChanged)
        );

        private static void OnLineIntervalChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var teb = (TextEditBox) d;
            teb.FormattedTextService.LineInterval = (double) e.NewValue;
            teb.FormattedTextService?.UpdateAll();
            teb.InvalidateVisual();
        }

        public static readonly DependencyProperty WordsToHighlightProperty = DependencyProperty.Register(
            nameof(WordsToHighlight),
            typeof(ISet<string>),
            typeof(TextEditBox),
            new FrameworkPropertyMetadata(
                new HashSet<string>(),
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender,
                OnWordsToHighlightChanged)
        );

        private static void OnWordsToHighlightChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var teb = (TextEditBox) d;
            teb.FormattedTextService.HighlightTextService.WordsToHighlight = (ISet<string>) e.NewValue;
            teb.FormattedTextService?.UpdateAll();
            teb.InvalidateVisual();
        }

        public static readonly DependencyProperty RawTextLinesProperty = DependencyProperty.Register(
            nameof(RawTextLines),
            typeof(IList<string>),
            typeof(TextEditBox),
            new FrameworkPropertyMetadata(
                new List<string>(new[] {""}),
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender,
                OnRawTextLinesChanged)
        );

        private static void OnRawTextLinesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var teb = (TextEditBox) d;
            teb.TextLines.SetText((IList<string>) e.NewValue);
            teb.TextEditBoxModel.UpdateAll();
            teb.InvalidateVisual();
        }

        public static readonly DependencyProperty HorizontalDeltaProperty = DependencyProperty.Register(
            nameof(HorizontalDelta),
            typeof(double),
            typeof(TextEditBox),
            new FrameworkPropertyMetadata(
                10d,
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender)
        );

        public static readonly DependencyProperty VerticalDeltaProperty = DependencyProperty.Register(
            nameof(VerticalDelta),
            typeof(double),
            typeof(TextEditBox),
            new FrameworkPropertyMetadata(
                10d,
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender)
        );

        public static readonly DependencyProperty CaretHeightParameterProperty = DependencyProperty.Register(
            nameof(CaretHeightParameter),
            typeof(double),
            typeof(TextEditBox),
            new FrameworkPropertyMetadata(
                1.2d,
                FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsRender)
        );

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

        public Brush HighlightBrush
        {
            get => (Brush) GetValue(HighlightBrushProperty);
            set => SetValue(HighlightBrushProperty, value);
        }

        public Brush TextBrush
        {
            get => (Brush) GetValue(TextBrushProperty);
            set => SetValue(TextBrushProperty, value);
        }

        public new string FontStyle
        {
            get => (string) GetValue(FontStyleProperty);
            set => SetValue(FontStyleProperty, value);
        }

        public new double FontSize
        {
            get => (double) GetValue(FontSizeProperty);
            set => SetValue(FontSizeProperty, value);
        }

        public double LineInterval
        {
            get => (double) GetValue(LineIntervalProperty);
            set => SetValue(LineIntervalProperty, value);
        }

        public ISet<string> WordsToHighlight
        {
            get => (ISet<string>) GetValue(WordsToHighlightProperty);
            set => SetValue(WordsToHighlightProperty, value);
        }

        public IList<string> RawTextLines
        {
            get => (IList<string>) GetValue(RawTextLinesProperty);
            set => SetValue(RawTextLinesProperty, value);
        }

        public double HorizontalDelta
        {
            get => (double) GetValue(HorizontalDeltaProperty);
            set => SetValue(HorizontalDeltaProperty, value);
        }

        public double VerticalDelta
        {
            get => (double) GetValue(VerticalDeltaProperty);
            set => SetValue(VerticalDeltaProperty, value);
        }

        public double CaretHeightParameter
        {
            get => (double) GetValue(CaretHeightParameterProperty);
            set => SetValue(CaretHeightParameterProperty, value);
        }

        #endregion

        public FormattedTextService FormattedTextService { get; set; }
        public TextEditBoxModel TextEditBoxModel { get; set; }
        public TextLines TextLines => TextEditBoxModel.TextLines;
        public int CurrentString => TextEditBoxModel.CurrentString;
        public int CurrentChar => TextEditBoxModel.CurrentChar;
        public string Text => TextEditBoxModel.Text;
        public SelectedTextBounds SelectedTextBounds => TextEditBoxModel.SelectedText;
        public int LinesCount => TextEditBoxModel.TextLines.Count;
        public string SelectedText => TextLines.GetInBounds(SelectedTextBounds);
        public bool IsInsertKeyPressed => TextEditBoxModel.IsInsertMode;

        public double LineHeight => FormattedTextService.FontSize + FormattedTextService.LineInterval;
        public double TextHeight => LineHeight * VerticalDelta + FormattedTextService.TextHeight;
        public double TextWidth => PaddingLeft + HorizontalDelta + FormattedTextService.MaxLineWidth;
        public double CaretHeight => CaretHeightParameter * FormattedTextService.FontSize;

        public TextEditBox() => SetDefaultSettings();

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

        public void UpdateOffsetByCaretPosition()
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

        private void UpdateOffset()
        {
            UpdateOffsetByCaretPosition();
            UpdateHorizontalOffset();
            UpdateVerticalOffset();
        }

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
            TextEditBoxModel.SetCurrentPosition(GetCharByPoint(e.GetPosition(this)));
            UpdateOffsetByCaretPosition();
            InvalidateVisual();
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            ReleaseMouseCapture();
            TextEditBoxModel.SelectToPosition(GetCharByPoint(e.GetPosition(this)));
            InvalidateVisual();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            if (!IsMouseOver || e.LeftButton != MouseButtonState.Pressed) return;
            TextEditBoxModel.SelectToPosition(GetCharByPoint(e.GetPosition(this)));
            UpdateOffsetByCaretPosition();
            InvalidateVisual();
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
            if (string.IsNullOrEmpty(e.Text)
                || KeysCharacters.Backspace.Equals(e.Text)
                || KeysCharacters.Enter.Equals(e.Text)) return;

            TextEditBoxModel.DeleteSelectedText();
            TextEditBoxModel.AddLinesOnCurrentPosition(Regex.Split(e.Text, "\r\n"));

            UpdateOffsetByCaretPosition();
            InvalidateVisual();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            switch (e.Key)
            {
                case Key.Enter:
                    OnEnterKey();
                    break;
                case Key.Back:
                    OnBackspaceKey();
                    break;
                case Key.Left:
                    OnLeftKey();
                    break;
                case Key.Right:
                    OnRightKey();
                    break;
                case Key.Up:
                    OnUpKey();
                    break;
                case Key.Down:
                    OnDownKey();
                    break;
                case Key.Insert:
                    OnInsertKey();
                    break;
                case Key.Delete:
                    OnDeleteKey();
                    break;
                case Key.Tab:
                    OnTabKey();
                    break;
                default:
                    return;
            }

            UpdateOffsetByCaretPosition();
            SelectedTextBounds.Invalidate();
            InvalidateVisual();
        }

        protected override AutomationPeer OnCreateAutomationPeer() =>
            new TextEditBoxAutomationPeer(this);

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);
            if (e.Property.Equals(ActualWidthProperty))
            {
                UpdateHorizontalOffset();
            }
            else if (e.Property.Equals(ActualHeightProperty))
            {
                UpdateVerticalOffset();
            }
        }

        #endregion

        #region Keys

        private void OnDeleteKey()
        {
            if (DeleteSelected())
                return;

            TextEditBoxModel.DeleteAfterCurrentPosition();

            UpdateVerticalOffset();
            UpdateHorizontalOffset();
        }

        private void OnDownKey() => TextEditBoxModel.SetPositionOneLineDown();

        private void OnUpKey() => TextEditBoxModel.SetPositionOneLineUp();

        private void OnRightKey() => TextEditBoxModel.SetPositionOneCharRight();

        private void OnLeftKey() => TextEditBoxModel.SetPositionOneCharLeft();

        private void OnInsertKey() => TextEditBoxModel.ChangeInsertMode();

        private void OnBackspaceKey()
        {
            if (DeleteSelected())
                return;

            TextEditBoxModel.DeleteBeforeCurrentPosition();

            UpdateVerticalOffset();
            UpdateHorizontalOffset();
        }

        private void OnEnterKey()
        {
            DeleteSelected();

            TextEditBoxModel.NewLineFromCurrentPosition();
        }

        private void OnTabKey()
        {
            DeleteSelected();

            TextEditBoxModel.AddTabulationOnCurrentPosition();
        }

        #endregion

        #region ContextMenu

        private void CreateContextMenu()
        {
            var textEditContextMenu = new TextEditContextMenuModel(TextEditBoxModel);
            textEditContextMenu.Cut += OnCutPasteSelectAll;
            textEditContextMenu.Paste += OnCutPasteSelectAll;
            textEditContextMenu.SelectAll += OnCutPasteSelectAll;

            ContextMenu = new ContextMenu
            {
                Placement = PlacementMode.MousePoint,
                ItemsSource = new[]
                {
                    new MenuItem {Header = "Copy", Uid = "Copy", Command = textEditContextMenu.CopyCommand},
                    new MenuItem {Header = "Cut", Uid = "Cut", Command = textEditContextMenu.CutCommand},
                    new MenuItem {Header = "Paste", Uid = "Paste", Command = textEditContextMenu.PasteCommand},
                    new MenuItem
                        {Header = "Select all", Uid = "SelectAll", Command = textEditContextMenu.SelectAllCommand}
                },
                Uid = "ContextMenu"
            };
        }

        private void OnCutPasteSelectAll(object sender, EventArgs e)
        {
            UpdateOffset();
            InvalidateVisual();
        }

        #endregion

        #region Drawing

        private void DrawLines(DrawingContext dc)
        {
            var lowerBound = Math.Max(0, VerticalOffset / LineHeight - 1);
            var upperBound = Math.Min(TextLines.Count - 1,
                (ActualHeight + VerticalOffset) / LineHeight + 1);
            FormattedTextService.Underline(SelectedTextBounds, (int) lowerBound, (int) upperBound);
            for (var i = (int) lowerBound; i <= (int) upperBound; i++)
            {
                DrawLine(i, dc);
            }

            FormattedTextService.RemoveDecoration(SelectedTextBounds, (int) lowerBound, (int) upperBound);
        }

        private void DrawLine(int index, DrawingContext dc)
        {
            if (IsFocused && index == CurrentString)
            {
                var brush = IsInsertKeyPressed ? InsertModePositionBrush : NormalModePositionBrush;
                var caretPoint = GetCaretPoint();
                dc.DrawRectangle(brush, new Pen(),
                    new Rect(caretPoint.X, caretPoint.Y, 1, CaretHeight));
            }

            dc.DrawText(FormattedTextService[index],
                new Point(PaddingLeft - HorizontalOffset, LineHeight * index - VerticalOffset));
        }

        private void DrawBackground(DrawingContext dc) =>
            dc.DrawRectangle(Background, new Pen(BorderBrush, BorderWidth),
                new Rect(0, 0, ActualWidth, ActualHeight));

        #endregion

        public TextPosition GetCharByPoint(Point point)
        {
            var (realX, realY) = (point.X + HorizontalOffset, point.Y + VerticalOffset);
            var stringNumber = Math.Min((int) (realY / LineHeight), TextLines.Count - 1);
            stringNumber = Math.Max(0, stringNumber);
            var currentLength = 0d;
            var lastCharWidth = 0d;

            var i = 0;
            while (i < TextLines[stringNumber].Length && currentLength < realX)
            {
                var newCurrentLength =
                    FormattedTextHelper.GetWidth(TextLines[stringNumber].Substring(0, i + 1),
                        FormattedTextService.FontStyle,
                        FormattedTextService.FontSize);
                lastCharWidth = newCurrentLength - currentLength;
                currentLength = newCurrentLength;
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

        public bool DeleteSelected()
        {
            if (!TextEditBoxModel.DeleteSelectedText()) return false;
            UpdateOffset();
            InvalidateVisual();
            return true;
        }

        private Point GetCaretPoint() =>
            new Point(
                FormattedTextHelper.GetWidth(
                    TextLines.SubstringFromLine(CurrentString, 0, CurrentChar),
                    FormattedTextService.FontStyle, FormattedTextService.FontSize)
                + PaddingLeft
                - HorizontalOffset,
                LineHeight * CurrentString - VerticalOffset);

        private void SetDefaultSettings()
        {
            HorizontalDelta = Settings.HorizontalDelta;
            VerticalDelta = Settings.VerticalDelta;
            CaretHeightParameter = Settings.CaretHeightParameter;
            PaddingLeft = Settings.PaddingLeft;
            BorderWidth = Settings.BorderWidth;
            BorderBrush = Settings.BorderBrush;
            Background = Settings.Background;
            VerticalAlignment = VerticalAlignment.Stretch;
            HorizontalAlignment = HorizontalAlignment.Stretch;
            Focusable = Settings.Focusable;
            TextEditBoxModel = new TextEditBoxModel();
            FormattedTextService = new FormattedTextService(
                TextLines,
                Settings.FontStyle,
                Settings.FontSize,
                Settings.TextBrush,
                Settings.LineInterval,
                new HighlightTextService(new string[0], Settings.HighlightBrush));
            TextBrush = Settings.TextBrush;
            FontStyle = Settings.FontStyle;
            FontSize = Settings.FontSize;
            LineInterval = Settings.LineInterval;
            HighlightBrush = Settings.HighlightBrush;
        }
    }
}