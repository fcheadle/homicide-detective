using homicide_detective;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace unit_tests
{
    [TestClass]
    public class SceneTests
    {
        [TestMethod]
        public void NewSceneTest()
        {
            Scene scene = new Scene(11, 55);
            Assert.AreEqual(" bedroom", scene.name);
            Assert.IsTrue(scene.length <= scene.lengthRange.maximum);
            Assert.IsTrue(scene.length >= scene.lengthRange.minimum);
            Assert.IsTrue(scene.width <= scene.widthRange.maximum);
            Assert.IsTrue(scene.width <= scene.widthRange.maximum);
        }
    }
}
