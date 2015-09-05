using System;
using System.IO;

namespace Helper
{
    /// <summary>
    ///     Helper class for stream handling.
    /// </summary>
    public static class StreamHelper
    {
        /// <summary>
        ///     Converts a Stream into a byte array.
        /// </summary>
        /// <param name="input">A stream to read.</param>
        /// <see cref="https://stackoverflow.com/questions/221925/creating-a-byte-array-from-a-stream" />
        /// <returns>A byte array.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="NotSupportedException"></exception>
        /// <exception cref="ObjectDisposedException"></exception>
        /// <exception cref="IOException"></exception>
        /// <exception cref="OverflowException"></exception>
        public static byte[] ReadFully(Stream input)
        {
            var buffer = new byte[16*1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}