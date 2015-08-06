using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace ParticleIoNet.Client.UnitTests
{
    internal class CreateToken
    {
        [JsonProperty(PropertyName = "token_type")]
        public string TokenType { get; set; }

        [JsonProperty(PropertyName = "access_token")]
        public string AccessToken { get; set; }

        [JsonProperty(PropertyName = "expires_in")]
        public long ExpiresIn { get; set; }

        [JsonProperty(PropertyName = "refresh_token")]
        public string Refreshtoken { get; set; }
    }

    [TestClass]
    public class ParticleIoClientTests
    {
        [TestMethod]
        public void TestLogin()
        {
            var expected = new CreateToken
            {
                AccessToken = "token",
                ExpiresIn = 600,
                Refreshtoken = "refresh-token",
                TokenType = "token-type"
            };

            var sut = new ParticleIoNetClient("token", TestHelpers.GetHttpClient(expected));
            var actual = sut.LoginAsync("username", "password").Result;

            Assert.AreEqual(expected.AccessToken, actual);
        }

        [TestMethod]
        public void TestDeleteToken()
        {
            var expected = new HttpResponseMessage(HttpStatusCode.OK);

            var sut = new ParticleIoNetClient("token", TestHelpers.GetHttpClient(expected));
            
            sut.DeleteTokenAsync("user", "pwd", "token").Wait();
        }

        [TestMethod]
        public void TestListDevices()
        {
            var expected = new List<DeviceList>
            {
                new DeviceList
                {
                    DeviceId = "id1",
                    Connected = true
                },
                new DeviceList
                {
                    DeviceId = "id2",
                    Connected = false
                }
            };

            var sut = new ParticleIoNetClient("token", TestHelpers.GetHttpClient(expected));
            var actual = sut.ListDevicesAsync().Result;

            Assert.IsNotNull(actual);
            Assert.AreEqual(actual[0], expected[0]);
            Assert.AreEqual(actual[1], expected[1]);
        }
        
        [TestMethod]
        public void TestSubscribeAllDevicesEvents()
        {
            var expected = new HttpResponseMessage(HttpStatusCode.OK);

            var sut = new ParticleIoNetClient("token", TestHelpers.GetHttpClient(expected));

            sut.SubscribeAllDevicesEventsAsync().Wait();
        }

        [TestMethod]
        public void TestUnSubscribeAllDevicesEvents()
        {
            var expected = new HttpResponseMessage(HttpStatusCode.OK);

            var sut = new ParticleIoNetClient("token", TestHelpers.GetHttpClient(expected));
            sut.UnsubscribeAllDevicesEventsAsync().Wait();
        }
    }
}