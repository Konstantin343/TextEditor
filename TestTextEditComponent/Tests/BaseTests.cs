using NUnit.Framework;
using TestTextEditComponent.Models;

namespace TestTextEditComponent.Tests
{
    public abstract class BaseTests
    {
        protected TestTextEditBoxModel TestTextEditBoxModel;

        [SetUp]
        public void SetUp() => 
            TestTextEditBoxModel = new TestTextEditBoxModel();
    }
}