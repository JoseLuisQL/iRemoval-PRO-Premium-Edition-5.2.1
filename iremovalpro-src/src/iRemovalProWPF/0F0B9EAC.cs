internal struct _0F0B9EAC
{
	private uint BCB95F09;

	public uint _72153F06(_7C902113 _19007D98)
	{
		uint num = (_19007D98.D7BF25A6 >> 11) * BCB95F09;
		if (_19007D98._70B1770F < num)
		{
			_19007D98.D7BF25A6 = num;
			BCB95F09 += 2048 - BCB95F09 >> 5;
			if (_19007D98.D7BF25A6 < 16777216)
			{
				_19007D98._70B1770F = (_19007D98._70B1770F << 8) | (byte)_19007D98._6C351E33.ReadByte();
				_19007D98.D7BF25A6 <<= 8;
			}
			return 0u;
		}
		_19007D98.D7BF25A6 -= num;
		_19007D98._70B1770F -= num;
		BCB95F09 -= BCB95F09 >> 5;
		if (_19007D98.D7BF25A6 < 16777216)
		{
			_19007D98._70B1770F = (_19007D98._70B1770F << 8) | (byte)_19007D98._6C351E33.ReadByte();
			_19007D98.D7BF25A6 <<= 8;
		}
		return 1u;
	}

	public void _23BF982B()
	{
		BCB95F09 = 1024u;
	}
}
