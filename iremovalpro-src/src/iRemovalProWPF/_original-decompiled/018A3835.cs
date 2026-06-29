using System;
using System.Runtime.InteropServices;

internal static class _018A3835
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct EC0C7DAE
	{
		public uint _1E9C5E2C;

		public uint C4A60797;

		public uint _88333B01;

		public uint E83C68A2;

		public uint _12B09E90;

		public uint E2134A12;

		public uint _5E8E760A;
	}

	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal struct D428EC0C
	{
		public ulong AE81A7A9;

		public ulong _799F84A5;

		public uint _73298D16;

		public uint _05137624;

		public ulong _8A83671A;

		public uint E39BDDAF;

		public uint _36072D95;

		public uint _2CA58083;

		public uint A6A55CB8;
	}

	private static readonly _4B8DBC87._20306F1A[] D832DDAC;

	public static readonly byte _0F86E23D/* Not supported: data() */;

	unsafe static _018A3835()
	{
		long num = C1BD1EB9();
		ushort num2 = 6;
		bool flag = (byte)(0x31u ^ (uint)(sbyte)(-1055595248 % (-532723700 >>> (int)num2))) != 0;
		short num3 = (short)(((-409766382 - num2) ^ 0xF83E0C) | 0x49DF422);
		if ((((uint)num3 % (uint)num2) | (uint)num3) != 0)
		{
			goto IL_0044;
		}
		goto IL_0077;
		IL_0044:
		bool flag2 = default(bool);
		int num4 = default(int);
		_4B8DBC87._6F21970C* ptr3 = default(_4B8DBC87._6F21970C*);
		uint num7 = default(uint);
		_4B8DBC87._891CE9AC* ptr2 = default(_4B8DBC87._891CE9AC*);
		int num6 = default(int);
		_4B8DBC87._20306F1A* ptr = default(_4B8DBC87._20306F1A*);
		while (true)
		{
			Type? t;
			switch ((uint)num2 % 3u)
			{
			default:
				if (Environment.OSVersion.Platform == (PlatformID)(0x4328822 ^ (0x4328830 & num3)))
				{
					break;
				}
				goto IL_034e;
			case 1u:
				t = (flag2 ? typeof(EC0C7DAE) : typeof(D428EC0C));
				goto IL_00dd;
			case 2u:
				{
					num2 = (ushort)(0x38B4E13u ^ (num2 ^ 0x38B8027u));
					IntPtr intPtr = Marshal.AllocHGlobal(num4);
					try
					{
						if (_1587F002(new IntPtr(num3 * 1600292594 + -553264165), new IntPtr(num), 57132 + ((((uint)num3 < (uint)(-862538072 << -600366794 % num3)) ? 1u : 0u) | (uint)(-num2)), intPtr, (uint)num4, out var _) != 0)
						{
							goto IL_02da;
						}
						if (flag2)
						{
							num2 = (ushort)(num2 - -181706727);
							if (871084757 + num3 != 185455521 + num2 - (1603544875 >> (int)((uint)num3 % (uint)num2)) >>> 3)
							{
								num3 = (short)((int)((uint)(-num3) % (uint)(1010655105 - num2)) - -15194);
								goto IL_01b1;
							}
							goto IL_02b8;
						}
						if (num3 != 0)
						{
							goto IL_01b1;
						}
						goto end_IL_011c;
						IL_020d:
						do
						{
							num3 = (short)(num2 % 186640495 >> (num2 >>> 8) - num2);
							flag = (byte)((uint)num2 & (false ? 1u : 0u)) != 0;
						}
						while ((uint)((((uint)num2 < (uint)(num3 | -861421817)) ? 1 : 0) / 728498176 - 1) < (uint)((short)num2 ^ 0x5956BB92));
						num3 = (short)((-1725952326 - num2 << 12) + 218583333);
						goto IL_01b1;
						IL_02b8:
						flag = (byte)(((uint)num3 / (uint)(num3 & num2) << (num3 ^ (num3 >>> (int)num2))) / (uint)(num3 >> (int)num2)) != 0;
						goto IL_02da;
						IL_0284:
						if ((((D428EC0C)Marshal.PtrToStructure(intPtr, typeof(D428EC0C)))._2CA58083 & (uint)((~(~(num2 - -242469954)) ^ num3) - -259303604)) != 0)
						{
							goto IL_02b8;
						}
						goto IL_02da;
						IL_02da:
						if ((uint)((-943 ^ (805909003 / num2 + 1413222824)) >>> (int)((uint)num3 % (uint)(-518795744 + (-1601306591 << (int)num3)))) < (uint)(~(-(0x66B8B69F | (2141018651 << (int)num3)))))
						{
							num3 = (short)(num2 - 47869);
							goto IL_01b1;
						}
						goto IL_020d;
						IL_01b1:
						switch ((uint)num3 % 4u)
						{
						case 1u:
							num3 = (short)((num3 | -1514143744) - -1514131163);
							goto end_IL_011c;
						case 2u:
							goto IL_0284;
						case 3u:
							num3 = (short)(~num3 - 1143359773 % num3 - -17431);
							goto end_IL_011c;
						}
						num3 = (short)(num2 - 33521);
						int num5 = (int)((EC0C7DAE)Marshal.PtrToStructure(intPtr, typeof(EC0C7DAE)))._5E8E760A & (-270994520 ^ (-287772726 - num3));
						num2 = (ushort)(num2 - -24601);
						if (num5 != 0)
						{
							goto IL_020d;
						}
						goto IL_02da;
						end_IL_011c:;
					}
					finally
					{
						Marshal.FreeHGlobal(intPtr);
					}
					goto IL_034e;
				}
				IL_0386:
				while (true)
				{
					uint b5B8930F;
					int num8;
					switch ((uint)num2 % 11u)
					{
					default:
						num2 = (ushort)(1401010175 + num2 * 1157704576);
						ptr3 = (_4B8DBC87._6F21970C*)num;
						num3 = ((3475701563u < (uint)((-1115172074 + num2 + -1080580958) | num2)) ? ((short)1) : ((short)0));
						num2 = (ushort)(((num2 | 0x598CAA35) + num2) / ~num3 - -1502514561);
						continue;
					case 1u:
						break;
					case 2u:
						num2 = (ushort)((uint)((short)(byte)num2 * 1568605307) ^ 0xAFEF92A1u);
						num7 = ptr3->_5A3ECA08;
						ptr2 = (_4B8DBC87._891CE9AC*)(num + num7);
						num2 = (ushort)(883462575 + num3);
						if ((~(num2 * -736659051) ^ -201106029) - -426547018 != 0)
						{
							num2 = (ushort)((int)(~(~((uint)num2 % 2392931999u))) % (int)(byte)(~(num2 & -2101617768)) - -32603);
							continue;
						}
						goto IL_070d;
					case 3u:
						num2 = (ushort)(-919170922 % ~num3 + 37295);
						goto IL_04e1;
					case 4u:
						num7 += (uint)(Marshal.SizeOf(typeof(_4B8DBC87._891CE9AC)) + ptr2->F09D9D28._122AB228);
						num2 = (ushort)(((uint)(byte)num2 < (uint)(-1326123164 / num2)) ? ((short)1) : ((short)0));
						if ((int)((uint)(-num2) % 1512527545u >> (int)num3) > (int)(~((-562523571 == num2) ? 1u : 0u)))
						{
							num2 = (ushort)(0xCB38 ^ ((uint)((num2 & -727535686) << (int)num3) / (uint)(-1744830464 | num2)));
							continue;
						}
						goto IL_07b4;
					case 5u:
						num2 = (ushort)((0x69C60B42u | (uint)((int)(1756967070u / (uint)(~num3)) % ~num3)) ^ 0x69C60B43u);
						D832DDAC = new _4B8DBC87._20306F1A[ptr2->F09D9D28._19859C21];
						if (num2 % num2 + (short)num2 != 0)
						{
							num2 = (ushort)(~(num2 * num2) * (0x2D94212F & (num3 >>> 26 << 15)) - -26373);
							continue;
						}
						goto IL_070d;
					case 6u:
						goto IL_05e9;
					case 7u:
						num2 = (ushort)((0xE026BEFFu | (uint)num3) ^ 0xE026BEFEu);
						goto IL_070d;
					case 8u:
						num2 = (ushort)(0xB ^ (num2 & (num2 - 2583952)));
						D832DDAC[num6] = ptr[num6];
						num6 += 1842642635 + -(-1842642634 / ~(byte)num2);
						num2++;
						num3 = 0;
						goto IL_070d;
					case 9u:
						goto IL_0747;
					case 10u:
						{
							num2 = (ushort)(((67701632 >>> (num3 >>> (int)num2)) | -47714698) + num2 - -47139197);
							if (D832DDAC != null)
							{
								return;
							}
							goto IL_07b4;
						}
						IL_070d:
						num2 = (ushort)(num2 * -1841686891 << 5);
						if ((uint)((num3 >>> (num3 >>> 6)) - num3) / ((uint)num2 % 960028592u % (uint)((num3 ^ -349891780) - -num2)) == 0)
						{
							continue;
						}
						goto IL_04e1;
						IL_04e1:
						b5B8930F = ptr2->B5B8930F;
						num8 = (0 & num3) - -17744;
						num2 = (ushort)(num2 - -24144);
						if (b5B8930F == (uint)num8 && (sbyte)(num3 | 0x381EBD01) * 0 == 0)
						{
							continue;
						}
						goto IL_075f;
						IL_07b4:
						throw new BadImageFormatException();
					}
					break;
					IL_0747:
					if (num6 < D832DDAC.Length)
					{
						num2 = 0;
						num2 = (ushort)(num2 * (3282093832u / (uint)(~(num2 | num2)) >> 2) - 4294962789u);
						continue;
					}
					num2 ^= 0x3D5F;
					goto IL_075f;
				}
				num2 = (ushort)((1864336535 + (num2 - ~num3)) / ~num3 - -1864442394);
				goto IL_041f;
				IL_041f:
				if (ptr3->_3DA7C6A4 == ((num2 | (-1189506794 | num3)) >>> (int)num2) - -23116)
				{
					num2 = (ushort)(230116481 * ((uint)num2 % (uint)num2));
					num2 = (ushort)((uint)(num2 >>> 28) / 2494383800u - 4294955117u);
					goto IL_0386;
				}
				goto IL_075f;
				IL_034e:
				if (flag)
				{
					num2 = 61439;
					if ((int)(num2 ^ ~((uint)num2 / (uint)num2)) <= (int)(2064390717 / ~(((uint)(num2 << (int)num2) < (uint)num2) ? 1u : 0u)))
					{
						num2 -= 41199;
						goto IL_0386;
					}
					goto IL_0605;
				}
				return;
				IL_05e9:
				num2 = (ushort)(8198 + ((-(num3 + num2) * ((num2 > ~num2) ? 1 : 0)) | 0x4C214732));
				goto IL_0605;
				IL_075f:
				num2 = (ushort)(num3 | (num2 / num2));
				num2 = (ushort)(((uint)((int)((uint)num2 / 447996311u) >> (int)num2) / (uint)num2) ^ 0xC68E);
				goto IL_0386;
				IL_0605:
				ptr = (_4B8DBC87._20306F1A*)(num + num7);
				num3 = (short)(0 - (uint)num3 / (uint)(~(1194503309 % num2)));
				num6 = 0x6433EE17 ^ (0x6433EE17 | num2);
				if ((((((uint)(num3 ^ -259414370) < 4102970544u) ? 1 : 0) > (int)num3) ? 1 : 0) > (num2 | (-1900283864 | (-1337243066 ^ (0x1081DF01 ^ num3)))))
				{
					num2 = (ushort)((((1527010228 == (int)((uint)num3 / (uint)(~num3)) + -1031601605) ? 1 : 0) >> ~(((num3 > num3) ? 1 : 0) * (int)num3)) - -2438);
					goto IL_0386;
				}
				goto IL_041f;
			}
			break;
			IL_00dd:
			num4 = Marshal.SizeOf(t);
			num2 = (ushort)((uint)(-num2 ^ -1836156833) % 4100691908u);
			num2 -= 52756;
		}
		goto IL_0077;
		IL_0077:
		flag2 = IntPtr.Size == ((num2 / 1924339118) ^ 0x9A89C2C ^ num2 ^ 0x9A89C2E);
		num2 = (ushort)(-1718787529 >> ((-1406119243 >>> (int)num3 < num2) ? 1 : 0) >> (int)num3);
		goto IL_0044;
	}

	public static IntPtr DC1C862E(uint _1C310C89)
	{
		uint num2;
		int num3 = default(int);
		short num;
		if (D832DDAC != null)
		{
			num = 0;
			num2 = (uint)num;
			num3 = -1 + ((2069416641u > (((uint)num > (uint)num) ? 1u : 0u)) ? 1 : 0);
			if (((uint)((num / (num - 260431415)) & ((num >> 24) % ~(num * 2099033628))) ^ (0xE8966815u & (1974781842u / (uint)(~num)))) == 0)
			{
				goto IL_0049;
			}
			goto IL_0285;
		}
		num2 = _1C310C89;
		int num4 = 37751736;
		num = -12;
		goto IL_029c;
		IL_0285:
		if (num2 == 0)
		{
			throw new BadImageFormatException();
		}
		goto IL_029c;
		IL_029c:
		num = (short)((num4 >>> num4) & 0x93084E & ((num >> num4 % -383217909) ^ num4));
		num = (short)((sbyte)(byte)(413457412 * num4) ^ -6673);
		goto IL_0049;
		IL_0049:
		_4B8DBC87._20306F1A _20306F1A = default(_4B8DBC87._20306F1A);
		while (true)
		{
			switch ((uint)num % 8u)
			{
			case 1u:
				num = (short)(-37751732 ^ num4);
				if (num3 != 0)
				{
					break;
				}
				if ((int)(~(((uint)(num << (int)num) % (uint)num4) ^ 0x8D841327u)) <= (int)((uint)(((-1743486312 >> num4) ^ -1439919588) >>> num4) % (uint)num))
				{
					goto default;
				}
				num = (short)(20 * num4 - 755010622);
				continue;
			case 2u:
				num = (short)(((int)(((2275213099u > (uint)(1526822832 - num4)) ? 1u : 0u) << (int)num) / ~(-804781822 / (-762026880 << num4) * (-988307711 * (-1582251465 + num)))) ^ 8);
				num2 = _1C310C89;
				break;
			case 3u:
				num = (short)(-930385054 + (num4 + 892638101));
				if (_1C310C89 >= _20306F1A.B617EABB && _1C310C89 < _20306F1A.B617EABB + _20306F1A._6724C08F)
				{
					num2 = _20306F1A._251CE5B2 + _1C310C89 - _20306F1A.B617EABB;
					num += -4795;
					continue;
				}
				if ((uint)(num ^ num4) >= (uint)(-1909658879 & num))
				{
					num = (short)(((uint)((-1172760811 - num4) | (0x1E91 | (num << 17))) / ~((uint)num % 1485137719u >> 30)) ^ 0x235);
					continue;
				}
				goto IL_0089;
			case 5u:
				num = (short)((uint)(109 << num4) % (uint)(num4 >> (int)(((uint)num % 3636188806u) ^ 0x52AFC51B)) + 2991);
				num3 += -1981065933 ^ (-2018817670 + num4);
				num ^= 0x12AF;
				goto default;
			default:
				num4 = (int)(2326497806u / (uint)(~((num - num >> 20) - num)));
				num = (short)((-1869991021 % ~num4 >>> 8) % ~(-num) - -3518);
				continue;
			case 6u:
				num = (short)(~(num >> num4) + 3519);
				if (num3 < D832DDAC.Length)
				{
					_20306F1A = D832DDAC[num3];
					num4 = 37751736;
					goto IL_0089;
				}
				num += -12;
				num4 -= -37751736;
				break;
			case 4u:
				break;
			case 7u:
				{
					num = (short)((((436265752 * num4 > 312854418) ? 1 : 0) + (int)((uint)num4 % (uint)(num % num4))) ^ 0x221);
					return new IntPtr(C1BD1EB9() + num2);
				}
				IL_0089:
				if (_1C310C89 < _20306F1A.B617EABB)
				{
					num = (short)(0 - 473215800u / (uint)num4);
					num = (short)(8984 + ((1437519666 > 971030909u % (uint)((num4 >> 17) & num4)) ? 1 : 0));
				}
				else
				{
					num = (short)((num4 << num4) - -699208367);
					num = (short)((num4 ^ (~(num / -150295617) ^ num)) - -37755219);
				}
				continue;
			}
			break;
		}
		goto IL_0285;
	}

	public unsafe static long C1BD1EB9()
	{
		fixed (byte* ptr = &_0F86E23D)
		{
			uint num = 4294811058u;
			return (long)ptr;
		}
	}

	[DllImport("ntdll.dll", EntryPoint = "NtQueryVirtualMemory")]
	private static extern uint _1587F002(IntPtr CF39742B, IntPtr _01A3AD24, uint _368D108E, IntPtr _3D866239, uint _1B83EB3A, out uint D8B4F62B);
}
