using System;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using NUnit.Framework;
using TextEditComponent.TextEditComponent;

namespace TestTextEditBox
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test()
        {
            TextEditBox teb = null;
            string e = null;
            var t = new Thread(() =>
            {
                teb = new TextEditBox();
                teb.RaiseEvent(new MouseButtonEventArgs(Mouse.PrimaryDevice, 0, MouseButton.Left)
                {
                    RoutedEvent = UIElement.MouseLeftButtonDownEvent
                });
                teb.RaiseEvent(new TextCompositionEventArgs(Keyboard.PrimaryDevice,
                    new TextComposition(InputManager.Current, teb, "1233"))
                {
                    RoutedEvent = TextCompositionManager.TextInputEvent
                });
                teb.RaiseEvent(new KeyEventArgs(Keyboard.PrimaryDevice, new HwndSource(new HwndSourceParameters()), 1,
                    Key.Enter)
                {
                    RoutedEvent = UIElement.KeyDownEvent
                });
                e = teb.Text;
            });
            t.SetApartmentState(ApartmentState.STA);
            t.Start();
            t.Join(); 
            Assert.AreEqual("1233\r\n", e);
            var tt = new Thread(() =>
            {
                teb.RaiseEvent(new TextCompositionEventArgs(Keyboard.PrimaryDevice,
                    new TextComposition(InputManager.Current, teb, "34242"))
                {
                    RoutedEvent = TextCompositionManager.TextInputEvent
                });
                teb.RaiseEvent(new KeyEventArgs(Keyboard.PrimaryDevice, new HwndSource(new HwndSourceParameters()), 1,
                    Key.Enter)
                {
                    RoutedEvent = UIElement.KeyDownEvent
                });
                e = teb.Text;
            });
            tt.SetApartmentState(ApartmentState.STA);
            tt.Start();
            tt.Join();
            Assert.AreEqual(e, "e");
        }
    }
}