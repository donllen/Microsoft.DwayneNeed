﻿using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.DwayneNeed.Numerics;

namespace Microsoft.DwayneNeed.Utilities
{
    public struct Buffer2DView<T> where T : struct
    {
        public Buffer2DView(Buffer2D<T> buffer)
        {
            _buffer = buffer;
        }

        public Buffer2DView(Buffer2D<T> buffer, Int32Rect bounds)
        {
            _buffer = new Buffer2D<T>(buffer, bounds);
        }

        public Buffer2DView(Buffer2DView<T> buffer, Int32Rect bounds)
        {
            _buffer = new Buffer2D<T>(buffer._buffer, bounds);
        }

        public bool CompareBits(Buffer2D<T> srcBuffer, Int32Rect srcRect, Int32Point? dstPoint = null)
        {
            return _buffer.CompareBits(srcBuffer, srcRect, dstPoint);
        }

        public T this[int x, int y] => _buffer[x, y];
        public int Width => _buffer.Width;
        public int Height => _buffer.Height;

        public BitmapSource CreateBitmapSource(double dpiX, double dpiY, PixelFormat pixelFormat,
            BitmapPalette bitmapPalette)
        {
            return _buffer.CreateBitmapSource(dpiX, dpiY, pixelFormat, bitmapPalette);
        }

        internal Buffer2D<T> _buffer;
    }
}