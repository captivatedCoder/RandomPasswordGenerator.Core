//Implementation from the September 2007 article in MSDN Magazine

using System;
using System.Security.Cryptography;

namespace PasswordGenerator.Generators
{
    public class CryptoRandom : Random
    {
        private readonly RNGCryptoServiceProvider _cspRng = new RNGCryptoServiceProvider();
        private readonly byte[] _byteBuffer = new byte[4];

        public CryptoRandom() { }
        public CryptoRandom(int ignoredSeed) { }

        public override int Next()
        {
            _cspRng.GetBytes(_byteBuffer);
            return BitConverter.ToInt32(_byteBuffer, 0) & 0x7FFFFFFF;
        }

        public override int Next(int maxValue)
        {
            if (maxValue < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maxValue));
            }

            return Next(0, maxValue);
        }

        public override int Next(int minValue, int maxValue)
        {
            if (minValue > maxValue)
            {
                throw new ArgumentOutOfRangeException(nameof(minValue));
            }

            if (minValue == maxValue) return minValue;

            var diff = maxValue - minValue;

            while (true)
            {
                _cspRng.GetBytes(_byteBuffer);
                var rand = BitConverter.ToUInt32(_byteBuffer, 0);

                const long max = (1 + (long)uint.MaxValue);
                var remainder = max % diff;
                if (rand < max - remainder)
                {
                    return (int)(minValue + (rand % diff));
                }
            }
        }

        public override double NextDouble()
        {
            _cspRng.GetBytes(_byteBuffer);
            var rand = BitConverter.ToUInt32(_byteBuffer, 0);

            return rand / (1.0 + uint.MaxValue);
        }

        public override void NextBytes(byte[] buffer)
        {
            if (buffer == null)
            {
                throw new ArgumentNullException(nameof(buffer));
            }

            _cspRng.GetBytes(buffer);
        }
    }
}