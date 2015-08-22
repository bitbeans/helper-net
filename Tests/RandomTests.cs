using Helper;
using NUnit.Framework;

namespace Tests
{
    /// <summary>
    ///     Test class for our Random Helper classes.
    /// </summary>
    [TestFixture]
    public class RandomTests
    {
        [Test]
        public void DefaultGetBytesTest()
        {
            for (var repeat = 0; repeat < 1000; repeat++)
            {
                var r = new byte[4];
                RandomProvider.GetBytes(r);
                var q = 0;
                foreach (var b in r)
                {
                    unchecked
                    {
                        q += b;
                    }
                }
                Assert.AreNotSame(q, 0);
            }
        }

        [Test]
        public void DefaultNextMaxTest()
        {
            for (var repeat = 0; repeat < 1000; repeat++)
            {
                Assert.LessOrEqual(RandomProvider.Next(100)%256, 100);
            }
        }

        [Test]
        public void DefaultNextRangeTest()
        {
            for (var repeat = 0; repeat < 1000; repeat++)
            {
                var r = RandomProvider.Next(10, 25)%256;
                Assert.GreaterOrEqual(r, 10);
                Assert.Less(r, 25);
            }
        }

        [Test]
        public void DefaultNextTest()
        {
            Assert.Greater(RandomProvider.Next()%256, 0);
        }

        [Test]
        public void SecureGetBytesTest()
        {
            for (var repeat = 0; repeat < 1000; repeat++)
            {
                var r = new byte[4];
                SecureRandomProvider.GetBytes(r);
                var q = 0;
                foreach (var b in r)
                {
                    unchecked
                    {
                        q += b;
                    }
                }
                Assert.AreNotSame(q, 0);
            }
        }

        [Test]
        public void SecureGetNonZeroBytesTest()
        {
            for (var repeat = 0; repeat < 1000; repeat++)
            {
                for (var i = 0; i < 100; i++)
                {
                    var r = new byte[4];
                    SecureRandomProvider.GetNonZeroBytes(r);
                    var q = 0;
                    foreach (var b in r)
                    {
                        Assert.AreNotEqual(b, 0);
                        unchecked
                        {
                            q += b;
                        }
                    }
                    Assert.AreNotSame(q, 0);
                }
            }
        }

        [Test]
        public void SecureNextMaxTest()
        {
            for (var repeat = 0; repeat < 1000; repeat++)
            {
                Assert.LessOrEqual(SecureRandomProvider.Next(100)%256, 100);
            }
        }

        [Test]
        public void SecureNextRangeTest()
        {
            for (var repeat = 0; repeat < 1000; repeat++)
            {
                var r = SecureRandomProvider.Next(10, 25)%256;
                Assert.GreaterOrEqual(r, 10);
                Assert.Less(r, 25);
            }
        }

        [Test]
        public void SecureNextTest()
        {
            for (var repeat = 0; repeat < 1000; repeat++)
            {
                Assert.Greater(SecureRandomProvider.Next(), 0);
            }
        }

        [Test]
        public void Well512NextMaxTest()
        {
            for (var repeat = 0; repeat < 1000; repeat++)
            {
                Assert.LessOrEqual(Well512RandomProvider.Next(100)%256, 100);
            }
        }

        [Test]
        public void Well512NextRangeTest()
        {
            for (var repeat = 0; repeat < 1000; repeat++)
            {
                var r = Well512RandomProvider.Next(10, 25)%256;
                Assert.GreaterOrEqual(r, 10);
                Assert.Less(r, 25);
            }
        }

        [Test]
        public void Well512NextTest()
        {
            for (var repeat = 0; repeat < 1000; repeat++)
            {
                Assert.Greater(Well512RandomProvider.Next(), 0);
            }
        }
    }
}