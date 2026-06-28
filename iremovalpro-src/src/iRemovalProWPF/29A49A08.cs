internal struct _29A49A08
{
	private readonly _0F0B9EAC[] C50ED016;

	private readonly int _3EA3CC1A;

	public static uint _91A5A19F(_0F0B9EAC[] _051700BF, uint _0213BB8F, _7C902113 _21B1C8A9, int _9DABA6B3)
	{
		uint num = 1u;
		uint num2 = 0u;
		for (int i = 0; i < _9DABA6B3; i++)
		{
			uint num3 = _051700BF[_0213BB8F + num]._72153F06(_21B1C8A9);
			num <<= 1;
			num += num3;
			num2 |= num3 << i;
		}
		return num2;
	}

	public uint _39206523(_7C902113 _75318109)
	{
		uint num = 1u;
		for (int num2 = _3EA3CC1A; num2 > 0; num2--)
		{
			num = (num << 1) + C50ED016[num]._72153F06(_75318109);
		}
		return num - (uint)(1 << _3EA3CC1A);
	}

	public uint _16B72701(_7C902113 BD33D408)
	{
		uint num = 1u;
		uint num2 = 0u;
		for (int i = 0; i < _3EA3CC1A; i++)
		{
			uint num3 = C50ED016[num]._72153F06(BD33D408);
			num <<= 1;
			num += num3;
			num2 |= num3 << i;
		}
		return num2;
	}

	public void C7A13639()
	{
		for (uint num = 1u; num < 1 << _3EA3CC1A; num++)
		{
			C50ED016[num]._23BF982B();
		}
	}

	public _29A49A08(int CBADC913)
	{
		_3EA3CC1A = CBADC913;
		C50ED016 = new _0F0B9EAC[1 << CBADC913];
	}
}
