using System;
using System.Security.Cryptography;
using System.Threading;

namespace Helper
{
    /// <summary>
    ///     Getting random data with the Well512 algorithm.
    /// </summary>
    /// <remarks>This one is not cryptographically secure.</remarks>
    /// <see cref="http://stackoverflow.com/a/1227137/1837988" />
    public static class Well512RandomProvider
    {
        private static readonly uint[] State = new uint[16];
        private static uint _index;

        static Well512RandomProvider()
        {
            var random = new Random((int) DateTime.Now.Ticks);

            for (var i = 0; i < 16; i++)
            {
                State[i] = (uint) random.Next();
            }
        }

        /// <summary>
        ///     Returns a non-negative random integer.
        /// </summary>
        /// <returns>A non-negative random integer.</returns>
        public static uint Next()
        {
            var a = State[_index];
            var c = State[(_index + 13) & 15];
            var b = a ^ c ^ (a << 16) ^ (c << 15);
            c = State[(_index + 9) & 15];
            c ^= (c >> 11);
            a = State[_index] = b ^ c;
            var d = a ^ ((a << 5) & 0xda442d24U);
            //d = a ^ ((a << 5) & 0xDA442D24UL);
            _index = (_index + 15) & 15;
            a = State[_index];
            State[_index] = a ^ b ^ d ^ (a << 2) ^ (b << 18) ^ (c << 28);

            return State[_index];
        }

        /// <summary>
        ///     Returns a non-negative random integer that is less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">
        ///     The exclusive upper bound of the random number to be generated. maxValue must be greater than or
        ///     equal to 0.
        /// </param>
        /// <returns>A non-negative random integer.</returns>
        public static uint Next(uint maxValue)
        {
            return Next()%maxValue;
        }

        /// <summary>
        ///     Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">
        ///     The exclusive upper bound of the random number returned. maxValue must be greater than or equal
        ///     to minValue.
        /// </param>
        /// <returns>A random integer.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static uint Next(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException();
            }
            return (uint) ((Next()%(maxValue - minValue)) + minValue);
        }
    }

    /// <summary>
    ///     Getting random data in a thread-safe way.
    /// </summary>
    /// <remarks>This one is not cryptographically secure.</remarks>
    public static class RandomProvider
    {
        private static int _seed = Environment.TickCount;

        private static readonly ThreadLocal<Random> Provider = new ThreadLocal<Random>(() =>
            new Random(Interlocked.Increment(ref _seed))
            );

        /// <summary>
        ///     Returns a non-negative random integer.
        /// </summary>
        /// <returns>A non-negative random integer.</returns>
        public static int Next()
        {
            return Provider.Value.Next();
        }

        /// <summary>
        ///     Returns a non-negative random integer that is less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">
        ///     The exclusive upper bound of the random number to be generated. maxValue must be greater than or
        ///     equal to 0.
        /// </param>
        /// <returns>A non-negative random integer.</returns>
        public static int Next(int maxValue)
        {
            return Next()%maxValue;
        }

        /// <summary>
        ///     Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">
        ///     The exclusive upper bound of the random number returned. maxValue must be greater than or equal
        ///     to minValue.
        /// </param>
        /// <returns>A random integer.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException();
            }
            return ((Next()%(maxValue - minValue)) + minValue);
        }

        /// <summary>
        ///     Fills an array of bytes with a random sequence of values.
        /// </summary>
        /// <param name="data">The array to fill with random bytes.</param>
        public static void GetBytes(byte[] data)
        {
            Provider.Value.NextBytes(data);
        }
    }

    /// <summary>
    ///     Getting cryptographically strong random data in a thread-safe way.
    /// </summary>
    /// <remarks>This one is cryptographically secure.</remarks>
    public static class SecureRandomProvider
    {
        private static readonly ThreadLocal<RNGCryptoServiceProvider> Provider =
            new ThreadLocal<RNGCryptoServiceProvider>(() =>
                new RNGCryptoServiceProvider()
                );

        /// <summary>
        ///     Returns a non-negative random integer.
        /// </summary>
        /// <returns>A non-negative random integer.</returns>
        /// <exception cref="CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static int Next()
        {
            var buffer = new byte[4];
            Provider.Value.GetBytes(buffer);
            return new Random(BitConverter.ToInt32(buffer, 0)).Next();
        }

        /// <summary>
        ///     Returns a non-negative random integer that is less than the specified maximum.
        /// </summary>
        /// <param name="maxValue">
        ///     The exclusive upper bound of the random number to be generated. maxValue must be greater than or
        ///     equal to 0.
        /// </param>
        /// <returns>A non-negative random integer.</returns>
        /// <exception cref="CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static int Next(int maxValue)
        {
            return Next()%maxValue;
        }

        /// <summary>
        ///     Returns a random integer that is within a specified range.
        /// </summary>
        /// <param name="minValue">The inclusive lower bound of the random number returned.</param>
        /// <param name="maxValue">
        ///     The exclusive upper bound of the random number returned. maxValue must be greater than or equal
        ///     to minValue.
        /// </param>
        /// <returns>A random integer.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException();
            }
            return ((Next()%(maxValue - minValue)) + minValue);
        }

        /// <summary>
        ///     Fills an array of bytes with a cryptographically strong random sequence of values.
        /// </summary>
        /// <param name="data">The array to fill with cryptographically strong random bytes.</param>
        /// <exception cref="CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
        /// <exception cref="ArgumentNullException"><paramref name="data" /> is null.</exception>
        public static void GetBytes(byte[] data)
        {
            Provider.Value.GetBytes(data);
        }

        /// <summary>
        ///     Fills an array of bytes with a cryptographically strong sequence of random nonzero values.
        /// </summary>
        /// <param name="data"></param>
        /// <exception cref="CryptographicException">The cryptographic service provider (CSP) cannot be acquired. </exception>
        /// <exception cref="ArgumentNullException"><paramref name="data" /> is null.</exception>
        public static void GetNonZeroBytes(byte[] data)
        {
            Provider.Value.GetNonZeroBytes(data);
        }
    }
}