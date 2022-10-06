using System;

namespace VISCACameraController.Utils
{
    public static class HexaConverter
    {
        public static byte[] ConvertHexaStringToByteArray(string hexaString)
        {
            if (hexaString.Length % 2 != 0)
            {
                throw new ArgumentException(String.Format(System.Globalization.CultureInfo.InvariantCulture, "The binary key cannot have an odd number of digits: {0}", hexaString));
            }

            byte[] data = new byte[hexaString.Length / 2];
            for (int index = 0; index < data.Length; index++)
            {
                string byteValue = hexaString.Substring(index * 2, 2);
                data[index] = byte.Parse(byteValue, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture);
            }

            return data;
        }
    }
}
