using System;

namespace Helper
{
    /// <summary>
    ///     Helper class for paddings.
    /// </summary>
    public static class PaddingHelper
    {
        /// <summary>
        ///     Removes the right Pkcs7 padding of an array.
        /// </summary>
        /// <param name="paddedByteArray">The padded array.</param>
        /// <returns>The unpadded array.</returns>
        /// <exception cref="OverflowException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static byte[] RemovePkcs7(byte[] paddedByteArray)
        {
            if (paddedByteArray == null)
            {
                throw new ArgumentNullException("paddedByteArray", "paddedByteArray can not be null");
            }

            var last = paddedByteArray[paddedByteArray.Length - 1];
            if (paddedByteArray.Length <= last)
            {
                // there is no padding
                return paddedByteArray;
            }

            return ArrayHelper.SubArray(paddedByteArray, 0, (paddedByteArray.Length - last));
        }

        /// <summary>
        ///     Fill up an array with Pkcs7 padding.
        /// </summary>
        /// <param name="data">The source array.</param>
        /// <param name="paddingLength">The length of the padding.</param>
        /// <returns>The padded array.</returns>
        /// <exception cref="OverflowException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static byte[] AddPkcs7(byte[] data, int paddingLength)
        {
            if (data.Length > 256)
            {
                throw new ArgumentOutOfRangeException("data", "data must be <= 256 in length");
            }
            if (paddingLength > 256)
            {
                throw new ArgumentOutOfRangeException("paddingLength", "paddingLength must be <= 256");
            }

            if (paddingLength <= data.Length)
            {
                // there is nothing to pad
                return data;
            }

            var output = new byte[paddingLength];
            Buffer.BlockCopy(data, 0, output, 0, data.Length);
            for (var i = data.Length; i < output.Length; i++)
            {
                output[i] = (byte) (paddingLength - data.Length);
            }
            return output;
        }

        /// <summary>
        ///     Fill up an array with zero padding.
        /// </summary>
        /// <param name="data">The source array.</param>
        /// <param name="paddingLength">The length of the padding.</param>
        /// <returns>The padded array.</returns>
        /// <exception cref="OverflowException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static byte[] AddZero(byte[] data, int paddingLength)
        {
            if (data.Length > 256)
            {
                throw new ArgumentOutOfRangeException("data", "data must be <= 256 in length");
            }
            if (paddingLength > 256)
            {
                throw new ArgumentOutOfRangeException("paddingLength", "paddingLength must be <= 256");
            }

            if (paddingLength <= data.Length)
            {
                // there is nothing to pad
                return data;
            }

            var padding = new byte[paddingLength - data.Length];
            return ArrayHelper.ConcatArrays(data, padding);
        }

        /// <summary>
        ///     Removes the right zero padding of an array.
        /// </summary>
        /// <param name="paddedByteArray">The padded array.</param>
        /// <returns>The unpadded array.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static byte[] RemoveZero(byte[] paddedByteArray)
        {
            if (paddedByteArray == null)
            {
                throw new ArgumentNullException("paddedByteArray", "paddedByteArray can not be null");
            }
            var lastIndex = Array.FindLastIndex(paddedByteArray, b => b != 0);
            Array.Resize(ref paddedByteArray, lastIndex + 1);
            return paddedByteArray;
        }
    }
}