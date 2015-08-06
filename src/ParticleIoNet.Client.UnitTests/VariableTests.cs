using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParticleIoNet.Client.UnitTests
{
    [TestClass]
    public class VariableTests
    {
        [TestMethod]
        public void VariableEqualityTest()
        {
            var dateTimeOffset = DateTimeOffset.Now;

            var left = new Variable
            {
                Name = "variable",
                Result = 3.5,
                Cmd = "command",
                CoreInfo = new DeviceDetails
                {
                    Connected = true,
                    DeviceId = "device",
                    LastApp = "lastapp",
                    LastHeard = dateTimeOffset
                }
            };

            var right = new Variable
            {
                Name = "variable",
                Result = 3.5,
                Cmd = "command",
                CoreInfo = new DeviceDetails
                {
                    Connected = true,
                    DeviceId = "device",
                    LastApp = "lastapp",
                    LastHeard = dateTimeOffset
                }
            };

            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
            Assert.AreEqual(left, right);
        }
    }
}