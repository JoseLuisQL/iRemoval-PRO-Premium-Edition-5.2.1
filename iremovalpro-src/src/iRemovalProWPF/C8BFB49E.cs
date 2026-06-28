using System.Runtime.InteropServices;

public static class C8BFB49E
{
	[UnmanagedFunctionPointer(CallingConvention.StdCall)]
	internal delegate void _9BBC0029();

	public static object[] _81BBAF2C;

	internal static bool _42954884(byte[] _841BB6B0)
	{
		for (int i = 0; i < _841BB6B0.Length; i++)
		{
			if (i + 3 < _841BB6B0.Length && _841BB6B0[i] == 81 && _841BB6B0[i + 1] == 69 && _841BB6B0[i + 2] == 77 && _841BB6B0[i + 3] == 85)
			{
				return true;
			}
			if (i + 8 < _841BB6B0.Length && _841BB6B0[i] == 77 && _841BB6B0[i + 1] == 105 && _841BB6B0[i + 2] == 99 && _841BB6B0[i + 3] == 114 && _841BB6B0[i + 4] == 111 && _841BB6B0[i + 5] == 115 && _841BB6B0[i + 6] == 111 && _841BB6B0[i + 7] == 102 && _841BB6B0[i + 8] == 116)
			{
				return true;
			}
			if (i + 6 < _841BB6B0.Length && _841BB6B0[i] == 105 && _841BB6B0[i + 1] == 110 && _841BB6B0[i + 2] == 110 && _841BB6B0[i + 3] == 111 && _841BB6B0[i + 4] == 116 && _841BB6B0[i + 5] == 101 && _841BB6B0[i + 6] == 107)
			{
				return true;
			}
			if (i + 9 < _841BB6B0.Length && _841BB6B0[i] == 86 && _841BB6B0[i + 1] == 105 && _841BB6B0[i + 2] == 114 && _841BB6B0[i + 3] == 116 && _841BB6B0[i + 4] == 117 && _841BB6B0[i + 5] == 97 && _841BB6B0[i + 6] == 108 && _841BB6B0[i + 7] == 66 && _841BB6B0[i + 8] == 111 && _841BB6B0[i + 9] == 120)
			{
				return true;
			}
			if (i + 5 < _841BB6B0.Length && _841BB6B0[i] == 86 && _841BB6B0[i + 1] == 77 && _841BB6B0[i + 2] == 119 && _841BB6B0[i + 3] == 97 && _841BB6B0[i + 4] == 114 && _841BB6B0[i + 5] == 101)
			{
				return true;
			}
			if (i + 8 < _841BB6B0.Length && _841BB6B0[i] == 80 && _841BB6B0[i + 1] == 97 && _841BB6B0[i + 2] == 114 && _841BB6B0[i + 3] == 97 && _841BB6B0[i + 4] == 108 && _841BB6B0[i + 5] == 108 && _841BB6B0[i + 6] == 101 && _841BB6B0[i + 7] == 108 && _841BB6B0[i + 8] == 115)
			{
				return true;
			}
		}
		return false;
	}
}
