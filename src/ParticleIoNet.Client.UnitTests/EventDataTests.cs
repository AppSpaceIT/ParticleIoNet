using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParticleIoNet.Client.UnitTests
{
    [TestClass]
    public class EventDataTests
    {
        [TestMethod]
        public void EventDataEqualityTest()
        {
            var dateTimeOffset = DateTimeOffset.Now;

            var left = new EventData
            {
                Name = "name",
                Data = "somedata",
                Ttl = 50,
                PublishedAt = dateTimeOffset,
                DeviceId = "coreid"
            };

            var right = new EventData
            {
                Name = "name",
                Data = "somedata",
                Ttl = 50,
                PublishedAt = dateTimeOffset,
                DeviceId = "coreid"
            };

            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
            Assert.AreEqual(left, right);
        }
    }
}