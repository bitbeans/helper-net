using Helper;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    ///     Test class for our conversion helpers.
    /// </summary>
    [TestFixture]
    public class ConversionTests
    {
        [Test]
        public void ConvertIntegerToLittleEndianTest()
        {
            var expected = new byte[] {10, 0, 0, 0, 0, 0, 0, 0};
            var b = ConversionHelper.IntegerToLittleEndian(10);
            CollectionAssert.AreEqual(expected, b);
        }
    }
}