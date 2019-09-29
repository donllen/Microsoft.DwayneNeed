using System;
using System.Runtime.InteropServices;
using Microsoft.DwayneNeed.Win32.Common;

namespace Microsoft.DwayneNeed.Win32.Gdi32
{
    /// <summary>
    ///     A handle to a GDI object.
    /// </summary>
    public abstract class HGDIOBJ : ThreadAffinitizedHandle
    {
        public bool DangerousOwnsHandle;

        protected HGDIOBJ() : base(true)
        {
        }

        protected HGDIOBJ(IntPtr hObject) : base(true)
        {
            SetHandle(hObject);
        }

        /// <summary>
        ///     Retrieves the GDI object type.
        /// </summary>
        public OBJ ObjectType => GetObjectType(this);

        protected override bool ReleaseHandleSameThread()
        {
            if (DangerousOwnsHandle) DeleteObject(handle);

            return true;
        }

        #region PInvoke

        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern OBJ GetObjectType(HGDIOBJ hObject);

        // The handle type is IntPtr because this function is called during
        // handle cleanup, and the SafeHandle itself cannot be marshalled.
        [DllImport("gdi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool DeleteObject(IntPtr hObject);

        #endregion
    }
}