using System;
using System.Windows;
using Microsoft.DwayneNeed.Win32.User32;

namespace Microsoft.DwayneNeed.Interop
{
    public class WindowParameters
    {
        private HWND _parent;
        public object Tag { get; set; }
        public IntPtr HINSTANCE { get; set; }
        public Int32Rect WindowRect { get; set; }
        public string Name { get; set; }
        public WS Style { get; set; }
        public WS_EX ExtendedStyle { get; set; }

        public HWND Parent
        {
            get =>
                // never return null
                _parent ?? HWND.NULL;

            set => _parent = value;
        }
    }
}