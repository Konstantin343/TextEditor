using NUnit.Framework;

namespace TestTextEditComponent.Tests
{
    public class InsertModeTests : BaseTests
    {
        [Test]
        public void InsertModeOnTest()
        {
            TestTextEditBoxModel.ChangeInsertMode();
            Assert.IsTrue(TestTextEditBoxModel.IsInsertMode,
                "Insert mode isn't on");
        }
        
        [Test]
        public void StartInsertModeOffTest()
        {
            Assert.IsFalse(TestTextEditBoxModel.IsInsertMode,
                "Insert mode isn't off");
        }
        
        [Test]
        public void InsertModeOffTest()
        {
            TestTextEditBoxModel.ChangeInsertMode();
            TestTextEditBoxModel.ChangeInsertMode();
            Assert.IsFalse(TestTextEditBoxModel.IsInsertMode,
                "Insert mode isn't off");
        }
    }
}