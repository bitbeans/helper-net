using Helper;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    ///     Test class for our array helpers.
    /// </summary>
    [TestFixture]
    public class ArrayTests
    {
        [Test]
        public void ConcatFourArraysTest()
        {
            var array1 = new byte[]
            {
                66, 17, 81, 164, 89, 250, 234, 222
            };

            var array2 = new byte[]
            {
                61, 36, 113, 21, 249, 74, 237, 174
            };

            var array3 = new byte[]
            {
                66, 49, 129, 36, 9, 90, 250, 190
            };

            var array4 = new byte[]
            {
                77, 20, 81, 165, 89, 250, 237, 238
            };

            var expected = new byte[]
            {
                66, 17, 81, 164, 89, 250, 234, 222,
                61, 36, 113, 21, 249, 74, 237, 174,
                66, 49, 129, 36, 9, 90, 250, 190,
                77, 20, 81, 165, 89, 250, 237, 238
            };

            var merged = ArrayHelper.ConcatArrays(array1, array2, array3, array4);
            CollectionAssert.AreEqual(expected, merged);
        }

        [Test]
        public void ConcatTwoArraysTest()
        {
            var array1 = new byte[]
            {
                66, 17, 81, 164, 89, 250, 234, 222,
                61, 36, 113, 21, 249, 74, 237, 174
            };

            var array2 = new byte[]
            {
                66, 49, 129, 36, 9, 90, 250, 190,
                77, 20, 81, 165, 89, 250, 237, 238
            };

            var expected = new byte[]
            {
                66, 17, 81, 164, 89, 250, 234, 222,
                61, 36, 113, 21, 249, 74, 237, 174,
                66, 49, 129, 36, 9, 90, 250, 190,
                77, 20, 81, 165, 89, 250, 237, 238
            };

            var merged = ArrayHelper.ConcatArrays(array1, array2);
            CollectionAssert.AreEqual(expected, merged);
        }

        [Test]
        public void SubArraysTest()
        {
            var array = new byte[]
            {
                66, 49, 129, 36, 9, 90, 250, 190,
                61, 36, 113, 21, 249, 74, 237, 174
            };

            var expectedPart1 = new byte[]
            {
                66, 49, 129, 36, 9, 90, 250, 190
            };

            var expectedPart2 = new byte[]
            {
                61, 36, 113, 21, 249, 74, 237, 174
            };

            var sub1 = ArrayHelper.SubArray(array, 0, 8);
            CollectionAssert.AreEqual(expectedPart1, sub1);

            var sub2 = ArrayHelper.SubArray(array, 8);
            CollectionAssert.AreEqual(expectedPart2, sub2);
        }

        [Test]
        public void ConstantTimeEqualsTest()
        {
            var array1 = new byte[]
            {
                66, 49, 129, 36, 9, 90, 250, 190,
                61, 36, 113, 21, 249, 74, 237, 174
            };

            var array2 = new byte[]
            {
                66, 49, 129, 36, 9, 90, 250, 190,
                61, 36, 113, 21, 249, 74, 237, 174
            };
            Assert.True(ArrayHelper.ConstantTimeEquals(array1, array2));
        }
    }
}