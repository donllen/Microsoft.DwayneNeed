using System;
using System.Windows;
using System.Windows.Markup;
using System.Windows.Media;

namespace Microsoft.DwayneNeed.Controls
{
    /// <summary>
    ///     The VisualWrapper simply integrates a raw Visual child into a tree
    ///     of FrameworkElements.
    /// </summary>
    [ContentProperty("Child")]
    public class VisualWrapper<T> : FrameworkElement where T : Visual
    {
        private T _child;

        public T Child
        {
            get => _child;

            set
            {
                if (_child != null) RemoveVisualChild(_child);

                _child = value;

                if (_child != null) AddVisualChild(_child);
            }
        }

        protected override int VisualChildrenCount => _child != null ? 1 : 0;

        protected override Visual GetVisualChild(int index)
        {
            if (_child != null && index == 0)
                return _child;
            throw new ArgumentOutOfRangeException("index");
        }
    }

    /// <summary>
    ///     The VisualWrapper simply integrates a raw Visual child into a tree
    ///     of FrameworkElements.
    /// </summary>
    public class VisualWrapper : VisualWrapper<Visual>
    {
    }
}