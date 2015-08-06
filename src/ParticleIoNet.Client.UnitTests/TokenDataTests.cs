using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParticleIoNet.Client.UnitTests
{
    [TestClass]
    public class TokenDataTests
    {
        [TestMethod]
        public void TokenDataEqualityTest()
        {
            var dateTimeOffset = DateTimeOffset.Now;

            var left = new TokenData
            {
                Client = "client",
                ExpiresAt = dateTimeOffset,
                Token = "token"
            };

            var right = new TokenData
            {
                Client = "client",
                ExpiresAt = dateTimeOffset,
                Token = "token"
            };

            Assert.AreEqual(left.GetHashCode(), right.GetHashCode());
            Assert.AreEqual(left, right);
        }
    }
}