using Helper;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    ///     Test class for our padding helpers.
    /// </summary>
    [TestFixture]
    public class PaddingTests
    {
        [Test]
        public void Pkcs7Test()
        {
            var original = new byte[]
            {
                66, 17, 81, 164, 89, 250, 234, 222,
                61, 36, 113, 21, 249, 74, 237, 174,
                66, 49, 129, 36, 9, 90, 250, 190,
                77, 20, 81, 165, 89, 250, 237, 238
            };

            var expected = new byte[]
            {
                66, 17, 81, 164, 89, 250, 234, 222,
                61, 36, 113, 21, 249, 74, 237, 174,
                66, 49, 129, 36, 9, 90, 250, 190,
                77, 20, 81, 165, 89, 250, 237, 238,
                32, 32, 32, 32, 32, 32, 32, 32,
                32, 32, 32, 32, 32, 32, 32, 32,
                32, 32, 32, 32, 32, 32, 32, 32,
                32, 32, 32, 32, 32, 32, 32, 32
            };

            var paddedOriginal = PaddingHelper.AddPkcs7(original, 64);
            Assert.AreEqual(paddedOriginal.Length, 64);
            CollectionAssert.AreEqual(expected, paddedOriginal);

            var cleared = PaddingHelper.RemovePkcs7(paddedOriginal);
            Assert.AreEqual(cleared.Length, 32);
            CollectionAssert.AreEqual(original, cleared);
        }

        [Test]
        public void ZeroTest()
        {
            var original = new byte[]
            {
                66, 17, 81, 164, 89, 250, 234, 222,
                61, 36, 113, 21, 249, 74, 237, 174,
                66, 49, 129, 36, 9, 90, 250, 190,
                77, 20, 81, 165, 89, 250, 237, 238
            };

            var expected = new byte[]
            {
                66, 17, 81, 164, 89, 250, 234, 222,
                61, 36, 113, 21, 249, 74, 237, 174,
                66, 49, 129, 36, 9, 90, 250, 190,
                77, 20, 81, 165, 89, 250, 237, 238,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0,
                0, 0, 0, 0, 0, 0, 0, 0
            };

            var paddedOriginal = PaddingHelper.AddZero(original, 64);
            Assert.AreEqual(paddedOriginal.Length, 64);
            CollectionAssert.AreEqual(expected, paddedOriginal);

            var cleared = PaddingHelper.RemoveZero(paddedOriginal);
            Assert.AreEqual(cleared.Length, 32);
            CollectionAssert.AreEqual(original, cleared);
        }
    }
}