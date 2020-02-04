using System.Windows.Controls;
using NUnit.Framework;

namespace TestTextEditComponent
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test1()
        {
            var textBox = new TextBox();
            textBox.Copy();
        }
    }
}