using System;
using System.IO;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace ParticleIoNet.Client.UnitTests
{
    [TestClass]
    public class EventStreamProcessorTests
    {
        [TestMethod]
        public void TestProcessEvent()
        {
            var datetime = DateTimeOffset.Now;

            var expected = new EventData
            {
                Name = "event1",
                DeviceId = "id1",
                PublishedAt = datetime,
                Data = "somedata",
                Ttl = 60
            };

            using (var ms = new MemoryStream())
            {
                using (var writer = new StreamWriter(ms))
                {
                    writer.WriteLine("event: {0}", expected.Name);
                    writer.Flush();
                    var actualLength = ms.Length;
                    ms.Position = 0;

                    var sut = new EventStreamProcessor(ms, new CancellationTokenSource());

                    var actual = sut.GetEvent();
                    Assert.IsNull(actual);

                    writer.WriteLine("data: {0}", JsonConvert.SerializeObject(expected));
                    writer.Flush();
                    ms.Position = actualLength;

                    actual = sut.GetEvent();

                    Assert.IsNotNull(actual);
                    Assert.AreEqual(expected, actual);
                }
            }
        }
    }
}