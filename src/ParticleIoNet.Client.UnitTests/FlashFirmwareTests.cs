using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParticleIoNet.Client.UnitTests
{
    [TestClass]
    public class FlashFirmwareTests
    {
        [TestMethod]
        public void FlashFirmwareEqualityTest()
        {
            var left = new FlashFirmware
            {
                Errors = new { error = "none" }
            };

            var right = new FlashFirmware
            {
                Errors = new { error = "none" }
            };

            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
            Assert.AreEqual(left, right);
        }
    }
}