using System;

namespace Microsoft.DwayneNeed.Numerics
{
    /// <summary>
    ///     A numeric type for representing a fraction.
    /// </summary>
    public struct Binary64Significand
    {
        public Binary64Significand(ulong value, bool isSubNormal) : this()
        {
            // Only 52 bits can be specified.
            ulong mask = ((ulong) 1 << 52) - 1;
            if ((value & ~mask) != 0) throw new ArgumentOutOfRangeException("significand");

            Value = value;
            IsSubnormal = isSubNormal;
        }

        public ulong Value { get; }
        public bool IsSubnormal { get; }

        public double Fraction
        {
            get
            {
                ulong denominator = (ulong) 1 << 52;
                ulong numerator = Value;
                if (!IsSubnormal) numerator += denominator;

                return numerator / (double) denominator;
            }
        }
    }
}