using System;
using System.Runtime.InteropServices;

namespace iremovalpro
{
    /// <summary>
    /// Public P/Invoke surface exposed by iremovalpro.dll.
    /// The launcher (iRemovalProWPF) imports these via [DllImport("iremovalpro.dll")].
    /// Extracted from the launcher's Library.cs and confirmed by pefile.DIRECTORY_ENTRY_EXPORT.
    ///
    /// RVA addresses in iremovalpro.dll (on-disk, before hydration):
    ///   ord=1  Action                   @ 0x00542810  (code is 0xFF in disk — encrypted)
    ///   ord=2  DotNetRuntimeDebugHeader @ 0x00FE60A0  (NativeAOT runtime debug stub)
    ///   ord=3  SetCallbacks              @ 0x00542B30
    ///   ord=4  SetWinInfo                @ 0x00542B60
    /// </summary>
    public static class NativeExports
    {
        public const string RA1NLIB = "iremovalpro.dll";

        /// <summary>
        /// Delegate invoked by the engine to drive the launcher UI.
        /// (action, key, value):
        ///   action — int opcode that tells MainWindow which control to update.
        ///   key    — first string parameter (label name, resource key, etc.)
        ///   value  — second string parameter (text to display, error message, etc.)
        /// </summary>
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate void FormCallback(int action, [MarshalAs(UnmanagedType.LPStr)] string a, [MarshalAs(UnmanagedType.LPStr)] string b);

        /// <summary>
        /// Register the launcher's FormCallback so the engine can drive the UI.
        /// Called once at startup by _31335E26.E794E8A4(app) in App.Main().
        /// </summary>
        [DllImport(RA1NLIB, CallingConvention = CallingConvention.StdCall)]
        public static extern void SetCallbacks(FormCallback callback);

        /// <summary>
        /// Push window-title strings into the engine (likely telemetry / branding).
        /// </summary>
        [DllImport(RA1NLIB, CallingConvention = CallingConvention.StdCall)]
        public static extern void SetWinInfo(
            [MarshalAs(UnmanagedType.LPStr)] string winInfo1,
            [MarshalAs(UnmanagedType.LPStr)] string winInfo2,
            [MarshalAs(UnmanagedType.LPStr)] string winInfo3,
            [MarshalAs(UnmanagedType.LPStr)] string winInfo4);

        /// <summary>
        /// Engine action dispatcher. Called by the launcher when the user clicks a button.
        /// Known action codes (extracted from MainWindow.xaml.cs closures):
        ///   5  — Sn_MouseDown     (clicked on the SerialNumber label)
        ///   6  — Imei_MouseDown   (clicked on the IMEI label)
        ///   9  — Button_Click     (clicked the main button, likely "Bypass")
        /// </summary>
        [DllImport(RA1NLIB, CallingConvention = CallingConvention.StdCall)]
        public static extern void Action(int action);

        /// <summary>
        /// NativeAOT runtime debug header accessor. Returns a pointer to the embedded
        /// runtime's debug metadata. Not used by the launcher directly.
        /// </summary>
        [DllImport(RA1NLIB, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr DotNetRuntimeDebugHeader();
    }
}
