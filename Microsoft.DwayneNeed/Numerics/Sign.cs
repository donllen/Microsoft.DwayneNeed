namespace Microsoft.DwayneNeed.Numerics
{
    /// <summary>
    ///     A simple type representing the sign of a numeric.
    /// </summary>
    public struct Sign
    {
        public Sign(bool isNegative)
        {
            _isNegative = isNegative;
        }

        public bool IsPositive => !_isNegative;
        public bool IsNegative => _isNegative;

        public static readonly Sign Positive = new Sign(false);
        public static readonly Sign Negative = new Sign(true);

        private readonly bool _isNegative;
    }
}