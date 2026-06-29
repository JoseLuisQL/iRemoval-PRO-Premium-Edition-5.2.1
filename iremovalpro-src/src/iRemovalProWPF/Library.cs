using System.Runtime.InteropServices;

namespace iRemovalProWPF
{
    /// <summary>
    /// Contrato P/Invoke con iremovalpro.dll (el engine NativeAOT de 33 MB).
    /// Extraido del Library.cs original decompilado, sin el constructor obfuscado
    /// (que llamaba a C8BFB49E._81BBAF2C[24](this)).
    ///
    /// Estas 3 funciones son los unicos 4 exports del DLL (verificados con pefile):
    ///   ord=1  Action                   @ rva 0x00542810
    ///   ord=2  DotNetRuntimeDebugHeader @ rva 0x00FE60A0
    ///   ord=3  SetCallbacks              @ rva 0x00542B30
    ///   ord=4  SetWinInfo                @ rva 0x00542B60
    /// </summary>
    internal class Library
    {
        /// <summary>
        /// Callback que el engine invoca para drivear la UI del launcher.
        /// Firma: (action, key, value)
        ///   action — opcode que indica que control actualizar
        ///   key    — primer string (label name, resource key, etc.)
        ///   value  — segundo string (texto a mostrar, error, etc.)
        /// </summary>
        public delegate void FormCallback(int action, [In][MarshalAs(UnmanagedType.LPStr)] string a, [In][MarshalAs(UnmanagedType.LPStr)] string b);

        private const string RA1NLIB = "iremovalpro.dll";

        /// <summary>
        /// Registrar el callback del launcher para que el engine pueda actualizar la UI.
        /// Se llama una vez al inicio, antes de cualquier Action().
        /// </summary>
        [DllImport(RA1NLIB, CallingConvention = CallingConvention.StdCall)]
        public static extern void SetCallbacks(FormCallback callback);

        /// <summary>
        /// Push info de branding/telemetry al engine (titulos de ventana, version, etc.)
        /// </summary>
        [DllImport(RA1NLIB, CallingConvention = CallingConvention.StdCall)]
        public static extern void SetWinInfo(
            [In][MarshalAs(UnmanagedType.LPStr)] string winInfo1,
            [In][MarshalAs(UnmanagedType.LPStr)] string winInfo2,
            [In][MarshalAs(UnmanagedType.LPStr)] string winInfo3,
            [In][MarshalAs(UnmanagedType.LPStr)] string winInfo4);

        /// <summary>
        /// Dispatcher de acciones del launcher al engine.
        /// Codigos conocidos (extraidos de los closures en MainWindow.cs original):
        ///   5  — Sn_MouseDown     (click en label Serial Number)
        ///   6  — Imei_MouseDown   (click en label IMEI)
        ///   9  — Button_Click_5   (click en checkrainButt — botón principal)
        /// </summary>
        [DllImport(RA1NLIB, CallingConvention = CallingConvention.StdCall)]
        public static extern void Action(int action);
    }
}
