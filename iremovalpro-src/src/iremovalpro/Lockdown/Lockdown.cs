using System;

namespace iremovalpro.Lockdown
{
    /// <summary>
    /// lockdownd client — the iOS port 62078 supervisor service.
    /// Provides session establishment, service discovery, pairing.
    /// </summary>

    public enum LockdownError
    {
        OK                      = 0,
        InvalidArg              = -1,
        InvalidConf             = -2,
        PairingFailed           = -3,
        SslNotEnabled           = -4,
        PasswordProtected       = -5,
        NoRunningSession        = -6,
        // ... recovered from engine strings
    }

    public sealed class LockdownException : Exception
    {
        public LockdownError Error { get; }
        public LockdownException(LockdownError e, string msg) : base(msg) { Error = e; }
    }

    public enum PairState
    {
        Paired,
        PairingFailed
    }

    public sealed class PairRecords
    {
        public string DeviceCertificate;
        public string HostCertificate;
        public string RootCertificate;
        public byte[]  HostId;
        public byte[]  SystemBUID;
    }

    /// <summary>
    /// Lockdown client — opens a session over usbmuxd tunnel, performs pairing
    /// dance (ExchangePairingRecords → ValidatePair → WritePairRecord), then
    /// exposes StartSession / StopSession / StartService(serviceName).
    /// </summary>
    public sealed class LockdownClient : IDisposable
    {
        public void Pair() => throw new NotImplementedException();
        public void Unpair() => throw new NotImplementedException();
        public PairRecords GetPairRecords() => throw new NotImplementedException();
        public IntPtr StartService(string serviceName) => throw new NotImplementedException();
        public void Dispose() { }
    }
}
