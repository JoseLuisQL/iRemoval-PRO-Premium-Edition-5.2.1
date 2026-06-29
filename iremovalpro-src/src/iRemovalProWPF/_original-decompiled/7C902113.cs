using System.IO;

internal class _7C902113
{
	public uint D7BF25A6;

	public Stream _6C351E33;

	public uint _70B1770F;

	private uint _1E844A94 = 1u;

	public void _57847F3E()
	{
		_6C351E33 = null;
	}

	public uint _6E1858AB(int BC342494)
	{
		uint num = D7BF25A6;
		uint num2 = _70B1770F;
		uint num3 = 0u;
		for (int num4 = BC342494; num4 > 0; num4--)
		{
			num >>= 1;
			uint num5 = num2 - num >> 31;
			num2 -= num & (num5 - 1);
			num3 = (num3 << 1) | (1 - num5);
			if (num < 16777216)
			{
				num2 = (num2 << 8) | (byte)_6C351E33.ReadByte();
				num <<= 8;
			}
		}
		D7BF25A6 = num;
		_70B1770F = num2;
		return num3;
	}

	public void _9F07BBA7(Stream _5A9DCA97)
	{
		_6C351E33 = _5A9DCA97;
		_70B1770F = 0u;
		D7BF25A6 = uint.MaxValue;
		for (int i = 0; i < 5; i++)
		{
			_70B1770F = (_70B1770F << 8) | (byte)_6C351E33.ReadByte();
		}
	}
}
