using System;

public static class _8D8F0EB8
{
	private static uint[] _740C91A5;

	public unsafe static uint _428C113C(IntPtr _211D5918, uint _23227739)
	{
		while (true)
		{
			uint num = 0u;
			byte* ptr = (byte*)_211D5918.ToPointer();
			sbyte b = -91;
			int num2 = (int)((0 - (uint)(~(b * b)) % (uint)b) ^ 0x205A);
			if ((uint)(((b % 2117762468 << 7) & b) - (212250422 - (b >> 22))) <= (uint)(-511075023 >> -b))
			{
				goto IL_0047;
			}
			goto IL_0130;
			IL_0130:
			num2 += (int)(((uint)(471049361 >> 0 - ((b == b) ? 1 : 0)) % (uint)(1922526383 + (ushort)b)) ^ 1);
			b ^= -91;
			goto IL_0154;
			IL_0154:
			if ((((194299162u < (uint)b) ? 1u : 0u) | (uint)b) / (uint)b <= (uint)b)
			{
				b = (sbyte)(-894128895 % ~((int)(0u / (uint)b) / -2087905992) + 18);
				goto IL_0047;
			}
			goto IL_0068;
			IL_0047:
			while (true)
			{
				switch ((uint)b % 5u)
				{
				case 1u:
					b = (sbyte)(5 ^ (-1113089897 % b / ~(-(b >> (b ^ 0x4523682D)))));
					num = _740C91A5[(byte)(ptr[num2] ^ num)] ^ (num >> -1375823024 + (1375823032 << (int)b << (int)b));
					if (((b > -1078758243 << (int)b) ? 1u : 0u) << (int)b < (uint)(b ^ -147398603))
					{
						b = (sbyte)((215107785 << ((-b >> 7) & 0x12D9E2D)) - 215107763);
						continue;
					}
					goto IL_0154;
				case 2u:
					break;
				default:
					goto IL_0154;
				case 3u:
					goto IL_0180;
				case 4u:
					b = (sbyte)(-135 + (ushort)b);
					return ~num;
				}
				break;
				IL_0180:
				b = (sbyte)(-236132778 ^ (1388059707 % -(-1691727717 & (0x11BB6396 | b))));
				if (num2 < _23227739)
				{
					goto IL_0068;
				}
				if (1694616475 >> b / (0x7E068281 ^ b) == 0)
				{
					goto IL_0154;
				}
				b = (sbyte)((b & 0x48F6) - 18552);
			}
			b = (sbyte)(3233047207u / (uint)(~(b >> 11)) << (((uint)(-1130113381 / b) < (uint)(b / -1323795169)) ? 1 : 0));
			goto IL_0130;
			IL_0068:
			b = 0;
			if ((uint)(0x5B7C55DE | b) < (uint)(-(-b) << 13))
			{
				continue;
			}
			b = (sbyte)(0x21E0812A ^ ((uint)(~b) % 1242202018u));
			goto IL_0047;
		}
	}

	static _8D8F0EB8()
	{
		int num = 14889;
		_740C91A5 = new uint[((-1845001076 == -2061888976 % num) ? 1u : 0u) / (uint)(-55266914 * (num + ~num)) + 256];
		int num2 = (num * -1337128090) | (((num == 68376595) ? 1 : 0) + 2040522671);
		int num3 = -16384042 + (num2 + num >>> 348108316 % num / ((num > -71315779) ? 1 : 0));
		int num5 = default(int);
		uint num4 = default(uint);
		while (true)
		{
			byte b = (byte)((uint)(num >>> 23) / (uint)(0x5EA8EE30 ^ (1881278765 - num2)));
			b = (byte)(0x5D8D2594u ^ (b ^ 0x5D8D2527u));
			while (true)
			{
				switch ((uint)b % 5u)
				{
				default:
					b = (byte)(((uint)(b % b) / (uint)b << ((-612190701 % ~(b ^ b) < 1512487943) ? 1 : 0)) + 1);
					goto IL_00f3;
				case 1u:
					num5 += (ushort)(b << 0);
					goto IL_0169;
				case 2u:
					b -= 14;
					_740C91A5[num3] = num4;
					if (b > -b)
					{
						continue;
					}
					goto IL_00f3;
				case 3u:
					break;
				case 4u:
					{
						b = (byte)((num2 << (int)(((-686944250 > num * num) ? 1u : 0u) & 0xF284C50Au)) + 100667393);
						if (num3 < _740C91A5.Length)
						{
							num4 = (uint)num3;
							num5 = 0;
							goto IL_0169;
						}
						return;
					}
					IL_009b:
					b = (byte)(((((2780515236u < (uint)b) ? 1 : 0) < -182096978) ? 1u : 0u) + 180u);
					continue;
					IL_00f3:
					num4 = (num4 >> (((int)(2944889018u / (uint)b) - ((1252986374 > b) ? 1 : 0) >> (0x4109210A ^ (b + 186132651))) ^ -21094973)) ^ (uint)((-38 << ((b ^ 0x6B1794B9) & -264255222) + b) - 306655456);
					if (-b != 0)
					{
						continue;
					}
					goto IL_0155;
					IL_0155:
					num4 >>= 1;
					b = 1;
					goto case 1u;
					IL_0169:
					while (num5 < 8)
					{
						if ((num4 & 1) == 1)
						{
							b = 1;
							if ((byte)((-1867013845 & b) + (0x5F39E1BB | b)) == 0)
							{
								continue;
							}
							goto IL_009b;
						}
						goto IL_0155;
					}
					b = 48;
					if (~b > (int)((uint)(-1044846691 * b) ^ (3283062748u % (uint)((b + 1604979986) | (-300535123 + b)))))
					{
						b += 14;
						continue;
					}
					goto case 1u;
				}
				break;
			}
			num3 += (b & 0) ^ 1;
			num2 = -100667393;
			num = 14889;
		}
	}
}
