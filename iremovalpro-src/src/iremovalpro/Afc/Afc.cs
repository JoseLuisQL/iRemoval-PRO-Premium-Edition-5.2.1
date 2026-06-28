using System;

namespace iremovalpro.Afc
{
    /// <summary>
    /// AFC (Apple File Conduit) — file access over the lockdown service.
    /// Types recovered from iremovalpro.dll strings.
    /// </summary>

    [Flags]
    public enum AfcFileOpenMode
    {
        Read        = 1 << 0,
        Write       = 1 << 1,
        Create      = 1 << 2,
        Append      = 1 << 3,
        Truncate    = 1 << 4,
        Exclusive   = 1 << 5
    }

    public enum AfcOpCode
    {
        Status          = 0x0001,
        Data            = 0x0002,
        ReadDir         = 0x0003,
        ReadFile        = 0x0004,
        WriteFile       = 0x0005,
        MakeDir         = 0x0006,
        GetFileInfo     = 0x0007,
        GetDevInfo      = 0x0008,
        RemovePath      = 0x0009,
        RenamePath      = 0x000A,
        SetFsBlockSize  = 0x000B,
        SetFileTime     = 0x000C,
        MakeLink        = 0x000D,
        GetFileHash      = 0x000E,
        // ... engine uses an extended set; recovered opcodes match libimobiledevice's afc.h
    }

    public enum AfcError
    {
        OK                  = 0,
        Unknown             = 1,
        NoResources         = 2,
        NoSuchResource      = 3,
        AccessDenied        = 4,
        ReadOnly            = 5,
        QuotaExceeded       = 6,
        PathExists          = 7,
        InvalidArgument     = 8,
        FileNotFound        = 9
    }

    public class AfcException : Exception
    {
        public AfcError Error { get; }
        public AfcException(AfcError e, string msg) : base(msg) { Error = e; }
    }

    public class AfcFileNotFoundException : AfcException
    {
        public AfcFileNotFoundException(string path) : base(AfcError.FileNotFound, path) { }
    }

    /// <summary>
    /// On-wire header. AFC packets are 48-byte aligned.
    /// </summary>
    public struct AfcHeader
    {
        public ulong Magic;       // "CFA6LPAA"
        public ulong PacketNumber;
        public ulong Operation;   // AfcOpCode
        public ulong Unknown1;
        public ulong Unknown2;
        public ulong PayloadLength;
    }

    public struct AfcFileInfoRequest { public string Path; }
    public struct AfcFileOpenRequest { public string Path; public AfcFileOpenMode Mode; }
    public struct AfcFileReadRequest { public long Handle; public long Length; }
    public struct AfcRmRequest       { public string Path; }
    public struct AfcReadDirectoryRequest { public string Path; }

    /// <summary>
    /// High-level client over the AFC lockdown service.
    /// </summary>
    public sealed class AfcService : IDisposable
    {
        public void OpenFile(string path, AfcFileOpenMode mode) => throw new NotImplementedException();
        public byte[] ReadFile(long handle, long length) => throw new NotImplementedException();
        public void WriteFile(long handle, byte[] data) => throw new NotImplementedException();
        public void CloseFile(long handle) => throw new NotImplementedException();
        public string[] ReadDirectory(string path) => throw new NotImplementedException();
        public void RemovePath(string path) => throw new NotImplementedException();
        public void Dispose() { }
    }
}
