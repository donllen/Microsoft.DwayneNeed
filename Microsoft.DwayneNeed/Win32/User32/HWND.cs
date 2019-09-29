using System;
using System.Runtime.InteropServices;

namespace Microsoft.DwayneNeed.Win32.User32
{
    /// <summary>
    ///     A SafeHandle representing an HWND.
    /// </summary>
    /// <remarks>
    ///     HWNDs have very loose ownership semantics.  Unlike normal handles,
    ///     there is no "CloseHandle" API for HWNDs.  There are APIs like
    ///     CloseWindow or DestroyWindow, but these actually affect the window,
    ///     not just your handle to the window.  This SafeHandle type does not
    ///     actually do anything to release the handle in the finalizer, it
    ///     simply provides type safety to the PInvoke signatures.
    ///     The StrongHWND SafeHandle will actually destroy the HWND when it
    ///     is disposed or finalized.
    ///     Because of this loose ownership semantic, the same HWND value can
    ///     be returned from multiple APIs and can be directly compared.  Since
    ///     SafeHandles are actually reference types, we have to override all
    ///     of the comparison methods and operators.  We also support equality
    ///     between null and HWND(IntPtr.Zero).
    /// </remarks>
    public class HWND : SafeHandle
    {
        static HWND()
        {
            NULL = new HWND(IntPtr.Zero);
            BROADCAST = new HWND(new IntPtr(0xffff));
            MESSAGE = new HWND(new IntPtr(-3));
            DESKTOP = new HWND(new IntPtr(0));
            TOP = new HWND(new IntPtr(0));
            BOTTOM = new HWND(new IntPtr(1));
            TOPMOST = new HWND(new IntPtr(-1));
            NOTOPMOST = new HWND(new IntPtr(-2));
        }

        /// <summary>
        ///     Public constructor to create an empty HWND SafeHandle instance.
        /// </summary>
        /// <remarks>
        ///     This constructor is used by the marshaller.  The handle value
        ///     is then set directly.
        /// </remarks>
        public HWND()
            : base(IntPtr.Zero, false)
        {
        }

        /// <summary>
        ///     Public constructor to create an HWND SafeHandle instance for
        ///     an existing handle.
        /// </summary>
        public HWND(IntPtr hwnd) : this()
        {
            SetHandle(hwnd);
        }

        /// <summary>
        ///     Constructor for derived classes to control whether or not the
        ///     handle is owned.
        /// </summary>
        protected HWND(bool ownsHandle)
            : base(IntPtr.Zero, ownsHandle)
        {
        }

        /// <summary>
        ///     Constructor for derived classes to specify a handle and to
        ///     control whether or not the handle is owned.
        /// </summary>
        protected HWND(IntPtr hwnd, bool ownsHandle)
            : base(IntPtr.Zero, ownsHandle)
        {
            SetHandle(hwnd);
        }

        public static HWND NULL { get; }
        public static HWND BROADCAST { get; }
        public static HWND MESSAGE { get; }
        public static HWND DESKTOP { get; }
        public static HWND TOP { get; }
        public static HWND BOTTOM { get; }
        public static HWND TOPMOST { get; }
        public static HWND NOTOPMOST { get; }

        public override bool IsInvalid => !IsWindow(handle);

        protected override bool ReleaseHandle()
        {
            // This should never get called, since we specify ownsHandle:false
            // when constructed.
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null)) return handle == IntPtr.Zero;

            HWND other = obj as HWND;
            return other != null && Equals(other);
        }

        public bool Equals(HWND other)
        {
            if (ReferenceEquals(other, null))
                return handle == IntPtr.Zero;
            return other.handle == handle;
        }

        public override int GetHashCode()
        {
            return handle.GetHashCode();
        }

        public static bool operator ==(HWND lvalue, HWND rvalue)
        {
            if (ReferenceEquals(lvalue, null))
                return ReferenceEquals(rvalue, null) || rvalue.handle == IntPtr.Zero;
            if (ReferenceEquals(rvalue, null))
                return lvalue.handle == IntPtr.Zero;
            return lvalue.handle == rvalue.handle;
        }

        public static bool operator !=(HWND lvalue, HWND rvalue)
        {
            return !(lvalue == rvalue);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool IsWindow(IntPtr hwnd);
    }
}