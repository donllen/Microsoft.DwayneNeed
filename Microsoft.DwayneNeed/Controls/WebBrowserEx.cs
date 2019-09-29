using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using Microsoft.DwayneNeed.Extensions;
using Microsoft.DwayneNeed.Interop;

namespace Microsoft.DwayneNeed.Controls
{
    /// <summary>
    ///     WebBrowser is sealed, so we can't derive from it.  Instead, we
    ///     provide a simple element that wraps a WebBrowser and directly
    ///     exposes the WebBrowserExtension properties.
    /// </summary>
    public class WebBrowserEx : FrameworkElement
    {
        /// <summary>
        ///     A property controlling the behavior for handling the
        ///     SWP_NOCOPYBITS flag during moving or sizing operations.
        /// </summary>
        public static readonly DependencyProperty CopyBitsBehaviorProperty = DependencyProperty.Register(
            "CopyBitsBehavior",
            typeof(CopyBitsBehavior),
            typeof(WebBrowserEx),
            new FrameworkPropertyMetadata(CopyBitsBehavior.Default));

        /// <summary>
        ///     A property controlling whether or not script errors are
        ///     suppressed.
        /// </summary>
        public static readonly DependencyProperty SuppressScriptErrorsProperty = DependencyProperty.Register(
            "SuppressScriptErrors",
            typeof(bool),
            typeof(WebBrowserEx),
            new FrameworkPropertyMetadata(false));

        /// <summary>
        ///     A property controlling whether or not WM_ERASEBKGND is
        ///     suppressed.
        /// </summary>
        public static readonly DependencyProperty SuppressEraseBackgroundProperty = DependencyProperty.Register(
            "SuppressEraseBackground",
            typeof(bool),
            typeof(WebBrowserEx),
            new FrameworkPropertyMetadata(false));

        private readonly WebBrowser _webBrowser;

        static WebBrowserEx()
        {
            // Look up the style for this control by using its type as its key.
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WebBrowserEx),
                new FrameworkPropertyMetadata(typeof(WebBrowserEx)));
        }

        public WebBrowserEx()
        {
            _webBrowser = new WebBrowser();
            AddVisualChild(_webBrowser);

            // Create a binding between the
            // HwndHostExtensions.CopyBitsBehaviorProperty on the child
            // WebBrowser and the CopyBitsBehaviorProperty on this object.
            Binding bindingCopyBitsBehavior = new Binding("CopyBitsBehavior")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            _webBrowser.SetBinding(HwndHostExtensions.CopyBitsBehaviorProperty, bindingCopyBitsBehavior);

            // Create a binding between the
            // WebBrowserExtensions.SuppressScriptErrorsProperty on the child
            // WebBrowser and the SuppressScriptErrorsProperty on this object.
            Binding bindingSuppressScriptErrors = new Binding("SuppressScriptErrors")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            _webBrowser.SetBinding(WebBrowserExtensions.SuppressScriptErrorsProperty, bindingSuppressScriptErrors);

            // Create a binding between the
            // WebBrowserExtensions.SuppressEraseBackgroundProperty on the child
            // WebBrowser and the SuppressEraseBackgroundProperty on this object.
            Binding bindingSuppressEraseBackground = new Binding("SuppressEraseBackground")
            {
                Source = this,
                Mode = BindingMode.TwoWay
            };
            _webBrowser.SetBinding(WebBrowserExtensions.SuppressEraseBackgroundProperty,
                bindingSuppressEraseBackground);
        }

        /// <summary>
        ///     The behavior for handling the SWP_NOCOPYBITS flag during
        ///     moving or sizing operations.
        /// </summary>
        public CopyBitsBehavior CopyBitsBehavior
        {
            get => (CopyBitsBehavior) GetValue(CopyBitsBehaviorProperty);

            set => SetValue(CopyBitsBehaviorProperty, value);
        }

        /// <summary>
        ///     Whether or not to suppress script errors.
        /// </summary>
        public bool SuppressScriptErrors
        {
            get => (bool) GetValue(SuppressScriptErrorsProperty);

            set => SetValue(SuppressScriptErrorsProperty, value);
        }

        /// <summary>
        ///     Whether or not to suppress WM_ERASEBKGND.
        /// </summary>
        public bool SuppressEraseBackground
        {
            get => (bool) GetValue(SuppressEraseBackgroundProperty);

            set => SetValue(SuppressEraseBackgroundProperty, value);
        }

        public WebBrowser WebBrowser => _webBrowser;

        protected override int VisualChildrenCount => 1;

        protected override Visual GetVisualChild(int index)
        {
            if (index != 0) throw new IndexOutOfRangeException();

            return _webBrowser;
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            _webBrowser.Measure(availableSize);
            return _webBrowser.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            _webBrowser.Arrange(new Rect(finalSize));
            return _webBrowser.RenderSize;
        }
    }
}