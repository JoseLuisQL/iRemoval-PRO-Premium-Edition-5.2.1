using System;
using System.Net;
using System.Threading.Tasks;

namespace iremovalpro.License
{
    /// <summary>
    /// License / activation server client.
    ///
    /// Endpoints recovered from iremovalpro.dll UTF-16LE strings:
    ///   BASE:        https://s13.iremovalpro.com
    ///   PAY:         https://iremovalpro.com/Payax0.php
    ///   PUB:         https://s13.iremovalpro.com/pub.php              (server public key)
    ///   VERSION:     https://s13.iremovalpro.com/version33.txt        (build version check)
    ///
    ///   /iremovalActivation/ars2.php      (activation record submit?)
    ///   /iremovalActivation/auth3.php     (auth handshake)
    ///   /iremovalActivation/checkm8.php   (checkm8 exploit dispatch — A7-A11 devices)
    ///   /iremovalActivation/iact9.php     (iact = "iOS activation" — main flow)
    ///   /iremovalActivation/mf5.php       (multi-factor stage 5)
    ///   /iremovalActivation/mf6.php       (multi-factor stage 6)
    ///   /iremovalActivation/mf7.php       (multi-factor stage 7)
    ///
    /// Header:  X-iRemovalPRO-Version  (build version)
    /// Support: support@iremovalpro.com
    /// Telegram: t.me/iremoval (announcements)
    /// Trustpilot: trustpilot.com/review/iremovalpro.co
    ///
    /// Apple endpoint also called for DRM handshake:
    ///   https://albert.apple.com/deviceservices/drmHandshake
    /// </summary>
    public sealed class LicenseClient
    {
        public const string ActivationServer = "https://s13.iremovalpro.com";
        public const string PaymentEndpoint  = "https://iremovalpro.com/Payax0.php";
        public const string AppleAlbert      = "https://albert.apple.com/deviceservices/drmHandshake";
        public const string HeaderVersion    = "X-iRemovalPRO-Version";

        public const string EndpointPublicKey  = "/pub.php";
        public const string EndpointVersion    = "/version33.txt";
        public const string EndpointArs2       = "/iremovalActivation/ars2.php";
        public const string EndpointAuth3      = "/iremovalActivation/auth3.php";
        public const string EndpointCheckm8    = "/iremovalActivation/checkm8.php";
        public const string EndpointIact9      = "/iremovalActivation/iact9.php";
        public const string EndpointMf5         = "/iremovalActivation/mf5.php";
        public const string EndpointMf6         = "/iremovalActivation/mf6.php";
        public const string EndpointMf7         = "/iremovalActivation/mf7.php";

        /// <summary>
        /// Server-reported maintenance state.
        /// Message seen in strings: "iRemoval PRO Servers are currently under MAINTENANCE"
        /// </summary>
        public bool ServerMaintenance { get; private set; }

        /// <summary>
        /// Registration flow observed via strings:
        ///   "Contact your provider to register your Serial Number and bypass it instantly"
        ///   "Close this window and register your device through our official representatives"
        ///   "Close this window and register your device with our official representatives"
        /// </summary>
        public Task<bool> RegisterSerialAsync(string serialNumber, string udid) => throw new NotImplementedException("Method body not recovered from NativeAOT dump — requires dynamic dump in Windows.");

        /// <summary>
        /// HTTP 402 (Payment Required) is treated specially: prompts the user to pay via Payax0.php.
        /// </summary>
        public Task<bool> PayActivationAsync(string serialNumber, string paymentToken) => throw new NotImplementedException();
    }
}
