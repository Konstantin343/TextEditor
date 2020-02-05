using NUnit.Framework;
using TextEditComponent.TextEditComponent;

namespace TestTextEditComponent
{
    public abstract class BaseTests
    {
        protected TextEditBoxModel TextEditBoxModel;

        [OneTimeSetUp]
        public void OneTimeSetUp() => 
            TextEditBoxModel = new TextEditBoxModel();
    }
}