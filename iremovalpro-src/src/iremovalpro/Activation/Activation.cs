using System;

namespace iremovalpro.Activation
{
    /// <summary>
    /// Activation Records — the XML/plist blobs iOS uses to prove the device
    /// is activated against Apple servers. Bypass targets this flow.
    ///
    /// Plist keys recovered from iremovalpro.dll UTF-16 strings:
    ///   /ActivationInfo
    ///   /ActivationPrivateKey
    ///   /ActivationRegulatoryVariant
    ///   /ActivationState
    ///   /Deactivate
    ///   /GetActivationRecord
    ///   /SBLockdownEverRegisteredKey
    ///
    /// XPC service: com.apple.mobileactivation / com.apple.mobileactivationd
    ///   (systemgroup.com.apple.mobileactivationd)
    /// </summary>

    public sealed class ActivationRecord
    {
        public string ActivationInfo { get; set; }
        public byte[] ActivationPrivateKey { get; set; }
        public string ActivationRegulatoryVariant { get; set; }
        public string ActivationState { get; set; }   // "Unactivated" or "Activated"
        public string SerialNumber { get; set; }
        public string BasebandSerialNumber { get; set; }
        public string MLBSerialNumber { get; set; }
    }

    /// <summary>
    /// mobileactivationd RPC methods recovered from strings.
    /// These are the XPC method names the engine invokes.
    /// </summary>
    public enum MobileActivationRequest
    {
        CreateActivationInfoRequest,
        CreateTunnel1ActivationInfoRequest,
        GetActivationStateRequest,
        HandleActivationInfoWithSessionRequest
    }

    /// <summary>
    /// iOS Device Activator versions recovered from strings:
    ///   "iOS Device Activator (MobileActivation-20 built on Jan 15 2012 at 19:07:28)"
    ///   "iOS Device Activator (MobileActivation-592.103.2)"
    /// The engine checks the running version to choose the request format.
    /// </summary>
    public static class MobileActivationVersions
    {
        public const string Legacy20  = "MobileActivation-20 built on Jan 15 2012 at 19:07:28";
        public const string Modern592  = "MobileActivation-592.103.2";
    }

    public sealed class ActivationBypass
    {
        public ActivationRecord GetActivationRecord() => throw new NotImplementedException();
        public void Deactivate() => throw new NotImplementedException();
        public void InjectBypassRecord(ActivationRecord r) => throw new NotImplementedException();
    }
}
