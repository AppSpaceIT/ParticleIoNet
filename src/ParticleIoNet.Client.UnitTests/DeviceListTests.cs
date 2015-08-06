using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParticleIoNet.Client.UnitTests
{
    [TestClass]
    public class DeviceListTests
    {
        [TestMethod]
        public void DeviceListEqualityTest()
        {
            var dateTimeOffset = DateTimeOffset.Now;

            var left = new DeviceList
            {
                DeviceId = "id",
                LastApp = "lastapp",
                LastHeard = dateTimeOffset,
                Name = "name",
                LastIpAddress = "127.0.0.1",
                ProductId = 6,
                Connected = true
            };

            var right = new DeviceList
            {
                DeviceId = "id",
                LastApp = "lastapp",
                LastHeard = dateTimeOffset,
                Name = "name",
                LastIpAddress = "127.0.0.1",
                ProductId = 6,
                Connected = true
            };

            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
            Assert.AreEqual(left, right);
        }
    }
}