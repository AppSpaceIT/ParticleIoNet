using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParticleIoNet.Client.UnitTests
{
    [TestClass]
    public class DeviceDetailsTests
    {
        [TestMethod]
        public void CoreInfoEqualityTest()
        {
            var dateTimeOffset = DateTimeOffset.Now;

            var left = new DeviceDetails
            {
                DeviceId = "id",
                LastApp = "lastapp",
                LastHeard = dateTimeOffset,
                LastHandshake = dateTimeOffset,
                Connected = true
            };

            var right = new DeviceDetails
            {
                DeviceId = "id",
                LastApp = "lastapp",
                LastHeard = dateTimeOffset,
                LastHandshake = dateTimeOffset,
                Connected = true
            };

            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
            Assert.AreEqual(left, right);
        }
    }
}