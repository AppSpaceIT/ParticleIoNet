using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParticleIoNet.Client.UnitTests
{
    [TestClass]
    public class CallFunctionTests
    {
        [TestMethod]
        public void CallFunctionEqualityTest()
        {
            var left = new CallFunction
            {
                DeviceId = "id",
                LastApp = "lastapp",
                Connected = true,
                ReturnValue = 123
            };

            var right = new CallFunction
            {
                DeviceId = "id",
                LastApp = "lastapp",
                Connected = true,
                ReturnValue = 123
            };

            //Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
            Assert.AreEqual(left, right);
        }
    }
}