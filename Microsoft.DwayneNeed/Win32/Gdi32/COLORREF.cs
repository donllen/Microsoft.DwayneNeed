namespace Microsoft.DwayneNeed.Win32.Gdi32
{
    public struct COLORREF
    {
        public COLORREF(uint cr)
        {
            _value = cr;
        }

        public COLORREF(byte red, byte green, byte blue)
        {
            _value = (uint) (red | (green << 8) | (blue << 16));
        }

        public uint Value => _value;

        private readonly uint _value;
    }
}