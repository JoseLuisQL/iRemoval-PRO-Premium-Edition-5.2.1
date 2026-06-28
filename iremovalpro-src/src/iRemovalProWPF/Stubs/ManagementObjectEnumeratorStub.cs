// Stub System.Management.ManagementObjectEnumerator to satisfy decompiled code.
// Real type is only available on Windows (runtimes/win/lib/net6.0/System.Management.dll).
// On a Windows build host, removing this file lets the runtime version resolve.
#if !WINDOWS_RUNTIME_PRESENT
namespace System.Management
{
    using System.Collections;

    // Stubs the public surface used by decompiled iRemovalProWPF sources:
    //   internal delegate ManagementObjectEnumerator _4EAF2933(object);
    //   internal static ManagementObjectEnumerator _910CED02(object) => ...
    // They are obfuscation-generated helpers and are never actually invoked
    // because the bodies route through the C8BFB49E._81BBAF2C[] delegate table.
    public sealed class ManagementObjectEnumerator : IDisposable, IEnumerator
    {
        public object Current => null;
        public bool MoveNext() => false;
        public void Reset() { }
        public void Dispose() { }
    }
}
#endif
