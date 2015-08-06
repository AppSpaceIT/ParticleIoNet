using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParticleIoNet.Client.UnitTests
{
    [TestClass]
    public class DeviceTests
    {
        [TestMethod]
        public void TestClaimDevice()
        {
            var expected = new HttpResponseMessage(HttpStatusCode.OK);

            var client = new ParticleIoNetClient("token", TestHelpers.GetHttpClient(expected));
            var sut = client.GetDevice("device1");
            sut.ClaimAsync().Wait();
        }

        [TestMethod]
        public void TestGetInfo()
        {
            var expected = new DeviceInfo
            {
                Connected = true,
                DeviceId = "123",
                Name = "device",
                ProductId = 6,
                Cc3000PatchVersion = "1.0",
                LastHeard = DateTimeOffset.Now,
                Functions = new[] { "fn1" },
                Variables = new Dictionary<string, string>() { { "v1", "int32"} },
            };

            var client = new ParticleIoNetClient("token", TestHelpers.GetHttpClient(expected));
            var sut = client.GetDevice("device1");
            var actual = sut.GetInfoAsync().Result;
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestReadVariable()
        {
            const string deviceId = "device1";

            var expected = new Variable
            {
                Cmd = "command",
                Name = "var1",
                Result = 10,
                CoreInfo = new DeviceDetails
                {
                    Connected = true,
                    DeviceId = deviceId
                }
            };
            var client = new ParticleIoNetClient("token", TestHelpers.GetHttpClient(expected));
            var sut = client.GetDevice("device1");
            var actual = sut.ReadVariableAsync("var1").Result;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCallFunction()
        {
            const string functionName = "myfunction1";
            const string arg1 = "arg1";
            const string arg2 = "arg2";

            var expected = new CallFunction
            {
                Connected = true,
                DeviceId = "123",
                LastApp = functionName,
                ReturnValue = 1
            };

            var client = new ParticleIoNetClient("token", TestHelpers.GetHttpClient(expected));
            var sut = client.GetDevice("device1");
            var actual = sut.CallFunctionAsync(functionName, arg1, arg2).Result;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestRemoveDevice()
        {
            var expected = new HttpResponseMessage(HttpStatusCode.OK);

            var client = new ParticleIoNetClient("token", TestHelpers.GetHttpClient(expected));
            var sut = client.GetDevice("device1");

            sut.RemoveAsync().Wait();
        }

        [TestMethod]
        public void TestUnsubscribeDeviceEvents()
        {
            var expected = new HttpResponseMessage(HttpStatusCode.OK);

            var client = new ParticleIoNetClient("token", TestHelpers.GetHttpClient(expected));
            var sut = client.GetDevice("device1");

            sut.UnsubscribeEventsAsync().Wait();
        }

        [TestMethod]
        public void TestFlashFirmware()
        {
            var expected = new FlashFirmware
            {
                Errors = "none"
            };

            var client = new ParticleIoNetClient("token", TestHelpers.GetHttpClient(expected));
            var sut = client.GetDevice("device1");
            var actual = sut.FlashFirmwareAsync(new Dictionary<string, byte[]>()
            {
                {"app2.ino", new byte[1]}
            }).Result;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestFlashBinaryFirmware()
        {
            var expected = new FlashFirmware
            {
                Errors = "none"
            };

            var client = new ParticleIoNetClient("token", TestHelpers.GetHttpClient(expected));
            var sut = client.GetDevice("device1");
            var actual = sut.FlashBinaryFirmareAsync(new byte[1]).Result;

            Assert.AreEqual(expected, actual);
        }
    }
}