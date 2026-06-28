using System;

namespace iremovalpro.Usbmuxd
{
    // Reimplemented usbmuxd protocol in .NET (no P/Invoke to libimobiledevice).
    // Types recovered from iremovalpro.dll strings.

    public enum UsbmuxdVersion
    {
        /// <summary>Binary protocol v0 (legacy)</summary>
        V0 = 0,
        /// <summary>Binary protocol v1 (current)</summary>
        V1 = 1
    }

    public enum UsbmuxdMessageType
    {
        Result,
        Connected,
        DeviceAdd,
        DeviceRemove,
        Listen,
        Connect
    }

    [Flags]
    public enum UsbmuxdLookupOptions
    {
        None              = 0,
        UsbmuxdLookupOnly = 1 << 0
    }

    public enum UsbmuxdResult
    {
        OK              = 0,
        BadCommand      = 1,
        BadDevice       = 2,
        ConnectionRefused = 3,
        // ... extended at runtime
    }

    public sealed class UsbmuxdException : Exception
    {
        public UsbmuxdResult Result { get; }
        public UsbmuxdException(UsbmuxdResult r, string msg) : base(msg) { Result = r; }
    }

    public sealed class UsbmuxdVersionException : Exception
    {
        public UsbmuxdVersionException(string msg) : base(msg) { }
    }

    public struct UsbmuxdDevice
    {
        public int DeviceId;
        public ushort ProductId;
        public ushort VendorId;
        public string SerialNumber;   // 256-byte buffer in the wire format
    }

    /// <summary>
    /// Wire header for the binary usbmuxd protocol (28 bytes).
    /// </summary>
    public struct UsbmuxdHeader
    {
        public uint Length;       // total packet length (header + payload)
        public uint Version;     // 0 or 1
        public uint MessageType; // UsbmuxdMessageType
        public uint Tag;         // request/response correlation id
    }

    /// <summary>
    /// Establishes the socket connection to the host's usbmuxd daemon.
    /// On Windows this is the iTunes "Apple Mobile Device Service" socket
    /// (usbmuxd listens on 127.0.0.1:27015 by default; engine uses 127.0.0.1:4915).
    /// </summary>
    public sealed class UsbmuxdSocket : IDisposable
    {
        public void Connect() => throw new NotImplementedException("Body not recovered from NativeAOT dump.");
        public UsbmuxdDevice[] ListDevices() => throw new NotImplementedException();
        public void ConnectToDevice(int deviceId, ushort port) => throw new NotImplementedException();
        public void Dispose() { }
    }
}
