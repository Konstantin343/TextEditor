using NUnit.Framework;
using TestTextEditorViewModel.Models;

namespace TestTextEditorViewModel.Tests
{
    public abstract class BaseTests
    {
        protected TestViewModel TestViewModel;

        [SetUp]
        public void SetUp() => 
            TestViewModel = new TestViewModel();
    }
}