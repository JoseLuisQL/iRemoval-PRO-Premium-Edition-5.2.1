using System;
using System.Diagnostics;

namespace iremovalpro.Native
{
    /// <summary>
    /// External CLI tools the engine shells out to (recovered from iremovalpro.dll strings):
    ///
    ///   /c idevicepair pair
    ///   /c ideviceproxy ad2 aw                                   (asymmetric reverse tunnel?)
    ///   /c ideviceproxy lao abc ofq com.iremovalpro.bypass --stream
    ///       ^ uses NETCONF base:1.1 capability (urn:ietf:params:netconf:base:1.1)
    ///   /C pnputil                                                 (Windows driver installer)
    ///   /c pnputil -a <inf>                                        (add driver package)
    ///   /c pnputil -f -d <inf>                                     (force delete)
    ///   /c pnputil -i -a <inf>                                     (install)
    ///
    /// Driver payload path in the binary:
    ///   \ref\ira1n\bin\driver\usb\usbaapl64.inf
    ///
    /// Local HTTP endpoint for the iTunes COM service (usbmuxd bridge):
    ///   http://127.0.0.1:4915
    /// </summary>
    public static class ExternalTools
    {
        public const string Idevicepair     = "idevicepair.exe";
        public const string Ideviceproxy   = "ideviceproxy.exe";
        public const string Pnputil         = "pnputil.exe";

        /// <summary>
        /// Run the libimobiledevice-net pair tool against the connected device.
        /// </summary>
        public static int PairDevice() =>
            RunShell(Idevicepair, "pair");

        /// <summary>
        /// Open a NETCONF-over-usbmuxd tunnel to the com.iremovalpro.bypass service
        /// installed on the iPhone by the Blackhound tweak. The "lao abc ofq" portion
        /// is the (obfuscated) NETCONF filter / RPC encoded payload.
        /// </summary>
        public static int StartBypassTunnel() =>
            RunShell(Ideviceproxy, "lao abc ofq com.iremovalpro.bypass --stream");

        /// <summary>
        /// Install the Apple USB driver on the host so usbmuxd can see the iPhone.
        /// </summary>
        public static int InstallUsbDriver(string infPath) =>
            RunShell(Pnputil, $"-i -a \"{infPath}\"");

        private static int RunShell(string exe, string args)
        {
            var psi = new ProcessStartInfo(exe, args)
            {
                UseShellExecute        = false,
                RedirectStandardOutput = true,
                RedirectStandardError  = true,
                CreateNoWindow         = true
            };
            using var p = Process.Start(psi);
            p?.WaitForExit();
            return p?.ExitCode ?? -1;
        }
    }
}
