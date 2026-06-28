using System.IO;

public class _9481CE26
{
	private uint EF1FCA9B;

	private uint _712E04A8 = 1u;

	public uint _603A7C26;

	private uint _3BAABF1B;

	private uint _6C2FCF83;

	private Stream _872A2530;

	private byte[] _52B01312;

	public void E31A2B1F(Stream _7891C534, bool A3861E33)
	{
		BDABB522();
		_872A2530 = _7891C534;
		if (!A3861E33)
		{
			_3BAABF1B = 0u;
			EF1FCA9B = 0u;
			_603A7C26 = 0u;
		}
	}

	public void _5D370A84(uint F53487A2, uint _3B3BFABD)
	{
		uint num = EF1FCA9B - F53487A2 - 1;
		if (num >= _6C2FCF83)
		{
			num += _6C2FCF83;
		}
		while (_3B3BFABD != 0)
		{
			if (num >= _6C2FCF83)
			{
				num = 0u;
			}
			_52B01312[EF1FCA9B++] = _52B01312[num++];
			if (EF1FCA9B >= _6C2FCF83)
			{
				_68B54AAD();
			}
			_3B3BFABD--;
		}
	}

	public void _59A0E127(byte C108DDBA)
	{
		_52B01312[EF1FCA9B++] = C108DDBA;
		if (EF1FCA9B >= _6C2FCF83)
		{
			_68B54AAD();
		}
	}

	public byte ABA37B35(uint _393DCE05)
	{
		uint num = EF1FCA9B - _393DCE05 - 1;
		if (num >= _6C2FCF83)
		{
			num += _6C2FCF83;
		}
		return _52B01312[num];
	}

	public void BDABB522()
	{
		_68B54AAD();
		_872A2530 = null;
	}

	public void _68B54AAD()
	{
		uint num = EF1FCA9B - _3BAABF1B;
		if (num != 0)
		{
			_872A2530.Write(_52B01312, (int)_3BAABF1B, (int)num);
			if (EF1FCA9B >= _6C2FCF83)
			{
				EF1FCA9B = 0u;
			}
			_3BAABF1B = EF1FCA9B;
		}
	}

	public void E811978B(uint _8D0D188B)
	{
		if (_6C2FCF83 != _8D0D188B)
		{
			_52B01312 = new byte[_8D0D188B];
		}
		_6C2FCF83 = _8D0D188B;
		EF1FCA9B = 0u;
		_3BAABF1B = 0u;
	}
}
