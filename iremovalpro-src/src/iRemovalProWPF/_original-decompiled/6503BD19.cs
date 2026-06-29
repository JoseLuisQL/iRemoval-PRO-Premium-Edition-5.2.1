using System;

public static class _6503BD19
{
	public static uint D7A053BB(uint A88797B4)
	{
		uint num = A88797B4 & 0xFF00FFu;
		uint num2 = A88797B4 & 0xFF00FF00u;
		return ((num >> 8) | (num << 24)) + ((num2 << 8) | (num2 >> 24));
	}

	public static string C22B6D13(byte[] F4BF45BE, int _1D82702D, int _57378B1B)
	{
		byte[] array = new byte[5] { 192, 224, 240, 248, 252 };
		char[] array2 = null;
		for (int i = 0; i < 2; i++)
		{
			int num = 0;
			int num2 = 0;
			while (num2 < _57378B1B)
			{
				byte b = F4BF45BE[_1D82702D + num2++];
				if (b < 128)
				{
					if (array2 != null)
					{
						array2[num] = (char)b;
					}
					num++;
					continue;
				}
				int num3 = 0;
				for (int j = 0; j < array.Length; j++)
				{
					if (b < array[j])
					{
						num3 = j;
						break;
					}
				}
				if (num3 == 0)
				{
					throw new ArgumentException();
				}
				uint num4 = (uint)(b - array[num3 - 1]);
				for (int k = 0; k < num3; k++)
				{
					if (num2 == _57378B1B)
					{
						throw new ArgumentException();
					}
					b = F4BF45BE[_1D82702D + num2++];
					if ((b & 0xC0) != 128)
					{
						throw new ArgumentException();
					}
					num4 = (num4 << 6) | (b & 0x3Fu);
				}
				if (num4 < 65536)
				{
					if (array2 != null)
					{
						array2[num] = (char)num4;
					}
					num++;
				}
				else if (num4 <= 1114111)
				{
					num4 -= 65536;
					if (array2 != null)
					{
						array2[num] = (char)(55296 + (num4 >> 10));
						array2[num + 1] = (char)(56320 + (num4 & 0x3FF));
					}
					num += 2;
				}
			}
			if (i == 0)
			{
				if (num == 0)
				{
					return string.Empty;
				}
				array2 = new char[num];
			}
		}
		return new string(array2);
	}

	public static byte[] _3C9D1BA3(byte[] _9E1F4BBB, int _932B70B5, int B8B27291)
	{
		byte[] array = new byte[B8B27291];
		for (int i = 0; i < B8B27291; i++)
		{
			array[i] = _9E1F4BBB[_932B70B5 + i];
		}
		return array;
	}

	public static ulong EA8EA905(byte[] FB1B12AE, int _2912F419)
	{
		ulong num = 0uL;
		for (int i = 0; i < 8; i++)
		{
			num |= (ulong)FB1B12AE[_2912F419 + i] << 8 * i;
		}
		return num;
	}

	public static uint _9B3AA997(byte[] _7D1C83AB, int _1C028DA4)
	{
		uint num = 0u;
		for (int i = 0; i < 4; i++)
		{
			num |= (uint)(_7D1C83AB[_1C028DA4 + i] << 8 * i);
		}
		return num;
	}

	public static byte[] _378F141E(string F3930893)
	{
		int length = F3930893.Length;
		byte[] array = null;
		for (int i = 0; i < 2; i++)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			for (int j = 0; j < length; j++)
			{
				char c = F3930893[j];
				int num4;
				if (c >= 'A' && c <= 'Z')
				{
					num4 = c - 65;
				}
				else if (c >= 'a' && c <= 'z')
				{
					num4 = c - 97 + 26;
				}
				else if (c >= '0' && c <= '9')
				{
					num4 = c - 48 + 52;
				}
				else if (c == '+')
				{
					num4 = 62;
				}
				else
				{
					if (c != '/')
					{
						switch (c)
						{
						case '=':
							break;
						default:
							throw new FormatException();
						case '\t':
						case '\n':
						case '\r':
						case ' ':
							continue;
						}
						if (i != 0)
						{
							break;
						}
						int num5 = 1;
						for (int k = j + 1; k < length; k++)
						{
							switch (F3930893[k])
							{
							case '=':
								num5++;
								break;
							default:
								throw new FormatException();
							case '\t':
							case '\n':
							case '\r':
							case ' ':
								break;
							}
						}
						if (num5 <= 2 && num5 * 2 == num2)
						{
							break;
						}
						throw new FormatException();
					}
					num4 = 63;
				}
				num = (num << 6) | num4;
				num2 += 6;
				if (num2 >= 8)
				{
					num2 -= 8;
					if (array != null)
					{
						array[num3] = (byte)(num >> num2);
						num &= (1 << num2) - 1;
					}
					num3++;
				}
			}
			if (i == 0)
			{
				array = new byte[num3];
				if (num3 == 0)
				{
					break;
				}
			}
		}
		return array;
	}

	public static uint AA0EB907(uint _0EAEB912, int _170871B2)
	{
		return (_0EAEB912 << _170871B2) | (_0EAEB912 >> 32 - _170871B2);
	}
}
