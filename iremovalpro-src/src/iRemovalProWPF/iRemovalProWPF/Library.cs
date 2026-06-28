using System.Runtime.InteropServices;

namespace iRemovalProWPF;

internal class Library
{
	public delegate void FormCallback(int action, [In][MarshalAs(UnmanagedType.LPStr)] string a, [In][MarshalAs(UnmanagedType.LPStr)] string b);

	private const string RA1NLIB = "iremovalpro.dll";

	[DllImport("iremovalpro.dll")]
	public static extern void SetCallbacks(FormCallback callback);

	[DllImport("iremovalpro.dll")]
	public static extern void SetWinInfo([In][MarshalAs(UnmanagedType.LPStr)] string winInfo1, [In][MarshalAs(UnmanagedType.LPStr)] string winInfo2, [In][MarshalAs(UnmanagedType.LPStr)] string winInfo3, [In][MarshalAs(UnmanagedType.LPStr)] string winInfo4);

	[DllImport("iremovalpro.dll")]
	public static extern void Action(int action);

	public Library()
	{
		C8BFB49E._81BBAF2C[24](this);
	}
}
