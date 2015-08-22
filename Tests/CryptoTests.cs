using Helper;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    ///     Test class for our crypto helpers.
    /// </summary>
    [TestFixture]
    public class CryptoTests
    {
        [Test]
        public void XorTest()
        {
            var data = new byte[]
            {
                66, 17, 81, 164, 89, 250, 234, 222,
                61, 36, 113, 21, 249, 74, 237, 174,
                66, 49, 129, 36, 9, 90, 250, 190,
                77, 20, 81, 165, 89, 250, 237, 238
            };

            var keys = new byte[]
            {
                93, 171, 8, 126, 98, 74, 138, 75,
                121, 225, 127, 139, 131, 128, 14, 230,
                111, 59, 177, 41, 38, 24, 182, 253,
                28, 47, 139, 39, 255, 136, 224, 235
            };

            var expected = new byte[]
            {
                31, 186, 89, 218, 59, 176, 96, 149,
                68, 197, 14, 158, 122, 202, 227, 72,
                45, 10, 48, 13, 47, 66, 76, 67,
                81, 59, 218, 130, 166, 114, 13, 5
            };
            var encrypted = CryptoHelper.Xor(data, keys);
            Assert.AreEqual(encrypted.Length, 32);
            CollectionAssert.AreEqual(expected, encrypted);
            var decrypted = CryptoHelper.Xor(encrypted, keys);
            Assert.AreEqual(decrypted.Length, 32);
            CollectionAssert.AreEqual(data, decrypted);
        }
    }
}