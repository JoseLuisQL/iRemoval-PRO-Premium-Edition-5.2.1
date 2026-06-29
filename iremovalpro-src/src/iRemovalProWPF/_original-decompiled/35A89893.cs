public class _35A89893
{
	private uint[] AC171CA0 = new uint[80];

	private uint[] B9B62509 = new uint[5];

	private byte[] _68B39686 = new byte[64];

	private long C1820EA2;

	private void _7186E505()
	{
		uint num = B9B62509[0];
		uint num2 = B9B62509[1];
		uint num3 = B9B62509[2];
		uint num4 = B9B62509[3];
		uint num5 = B9B62509[4];
		for (int i = 0; i < 16; i++)
		{
			uint num6 = 0u;
			for (int j = 0; j < 4; j++)
			{
				num6 |= (uint)(_68B39686[i * 4 + j] << 8 * (3 - j));
			}
			AC171CA0[i] = num6;
		}
		for (int k = 16; k < 80; k++)
		{
			uint num7 = AC171CA0[k - 3] ^ AC171CA0[k - 8] ^ AC171CA0[k - 14] ^ AC171CA0[k - 16];
			AC171CA0[k] = (num7 << 1) | (num7 >> 31);
		}
		for (int l = 0; l < 80; l++)
		{
			uint num8 = ((num << 5) | (num >> 27)) + num5 + AC171CA0[l];
			num8 = ((l >= 20) ? ((l >= 40) ? ((l >= 60) ? (num8 + (uint)((int)(num2 ^ num3 ^ num4) + -899497514)) : (num8 + (uint)((int)((num2 & num3) | (num2 & num4) | (num3 & num4)) + -1894007588))) : (num8 + ((num2 ^ num3 ^ num4) + 1859775393))) : (num8 + (((num2 & num3) | (~num2 & num4)) + 1518500249)));
			num5 = num4;
			num4 = num3;
			num3 = (num2 << 30) | (num2 >> 2);
			num2 = num;
			num = num8;
		}
		B9B62509[0] += num;
		B9B62509[1] += num2;
		B9B62509[2] += num3;
		B9B62509[3] += num4;
		B9B62509[4] += num5;
	}

	public byte[] D9A777A2(byte[] B9376A22, int DF8A2E35, int _4B91F030)
	{
		int num = 0;
		for (int i = 0; i < _4B91F030; i++)
		{
			_68B39686[num++] = B9376A22[DF8A2E35 + i];
			C1820EA2++;
			if (num == 64)
			{
				_7186E505();
				num = 0;
			}
		}
		_68B39686[num++] = 128;
		if (num > 56)
		{
			for (int j = num; j < 64; j++)
			{
				_68B39686[j] = 0;
			}
			_7186E505();
			num = 0;
		}
		for (int k = num; k < 56; k++)
		{
			_68B39686[k] = 0;
		}
		ulong num2 = (ulong)(C1820EA2 * 8);
		_68B39686[56] = (byte)(num2 >> 56);
		_68B39686[57] = (byte)(num2 >> 48);
		_68B39686[58] = (byte)(num2 >> 40);
		_68B39686[59] = (byte)(num2 >> 32);
		_68B39686[60] = (byte)(num2 >> 24);
		_68B39686[61] = (byte)(num2 >> 16);
		_68B39686[62] = (byte)(num2 >> 8);
		_68B39686[63] = (byte)num2;
		_7186E505();
		byte[] array = new byte[20];
		for (int l = 0; l < 5; l++)
		{
			uint num3 = B9B62509[l];
			array[l * 4] = (byte)(num3 >> 24);
			array[l * 4 + 1] = (byte)(num3 >> 16);
			array[l * 4 + 2] = (byte)(num3 >> 8);
			array[l * 4 + 3] = (byte)num3;
		}
		return array;
	}

	public void _263CF198()
	{
		C1820EA2 = 0L;
		B9B62509[0] = 1732584193u;
		B9B62509[1] = 4023233417u;
		B9B62509[2] = 2562383102u;
		B9B62509[3] = 271733878u;
		B9B62509[4] = 3285377520u;
	}

	public byte[] F9BF689E(byte[] F62DA93B)
	{
		return D9A777A2(F62DA93B, 0, F62DA93B.Length);
	}

	public _35A89893()
	{
		_263CF198();
	}
}
