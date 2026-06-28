using System;

namespace iremovalpro.Devices
{
    /// <summary>
    /// Information about the connected iOS device, recovered from usbmuxd +
    /// lockdown GetValue queries. Strings seen in the engine:
    ///   "Your device is supported for A12+ bypass!"
    ///   "Your device is supported for GSM/MEID bypass!"
    ///   "Your device is NOT supported for full signal bypass"
    ///   "This device is not supported for full bypass"
    ///   "This device looks like not activation locked"
    /// </summary>
    public enum DeviceClass
    {
        iPhone,
        iPad,
        iPod,
        AppleTV,
        Watch
    }

    public enum ChipFamily
    {
        A7, A8, A9, A10, A11,    // checkm8-vulnerable
        A12, A13, A14, A15, A16, // not checkm8-vulnerable; use different path
        S5L8942, S5L8960         // legacy SoC ids
    }

    [Flags]
    public enum BypassCapability
    {
        None              = 0,
        Checkm8           = 1 << 0,    // A7–A11 DFU exploit
        A12PlusBypass     = 1 << 1,    // "A12+ Bypass service"
        FullBypass        = 1 << 2,
        SignalBypass      = 1 << 3,    // GSM/MEID signal bypass
        OtaFeature        = 1 << 4     // "OTA & Erase" / "you can update but cannot restore"
    }

    public sealed class IDevice
    {
        public int          UsbmuxId        { get; set; }
        public string       UDID            { get; set; }
        public string       SerialNumber    { get; set; }
        public string       IMEI            { get; set; }
        public string       ECID            { get; set; }
        public string       Model           { get; set; }
        public string       IOSVersion      { get; set; }
        public DeviceClass  Class           { get; set; }
        public ChipFamily   Chip            { get; set; }
        public BypassCapability Capabilities { get; set; }

        public bool IsActivationLocked { get; set; }
    }
}
