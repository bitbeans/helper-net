namespace Helper
{
    /// <summary>
    ///     Helper class with some useful converters.
    /// </summary>
    public static class ConversionHelper
    {
        /// <summary>
        ///     Converts an integer to a little endian byte array.
        /// </summary>
        /// <param name="data">An integer to convert.</param>
        /// <returns>A little endian byte array.</returns>
        public static byte[] IntegerToLittleEndian(int data)
        {
            var le = new byte[8];
            le[0] = (byte) data;
            le[1] = (byte) (((uint) data >> 8) & 0xFF);
            le[2] = (byte) (((uint) data >> 16) & 0xFF);
            le[3] = (byte) (((uint) data >> 24) & 0xFF);
            return le;
        }
    }
}