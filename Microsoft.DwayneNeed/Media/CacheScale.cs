namespace Microsoft.DwayneNeed.Media
{
    public class CacheScale
    {
        private static CacheScale _auto;
        private readonly double? _scale;

        public CacheScale(double scale)
            : this((double?) scale)
        {
        }

        private CacheScale(double? scale)
        {
            _scale = scale;
        }

        public static CacheScale Auto
        {
            get
            {
                if (_auto == null) _auto = new CacheScale(null);

                return _auto;
            }
        }

        public bool IsAuto => !_scale.HasValue;

        public double Scale => _scale.Value;
    }
}