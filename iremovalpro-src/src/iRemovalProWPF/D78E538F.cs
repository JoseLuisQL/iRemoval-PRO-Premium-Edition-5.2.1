using System;

internal struct D78E538F
{
	private readonly int _8F9A3C8B;

	private readonly uint[] _1D0E9C93;

	private static readonly D78E538F _39847B9C = new D78E538F(-1, new uint[1] { 2147483648u });

	private static readonly D78E538F A3B80C27 = new D78E538F(1);

	private static readonly D78E538F _4EAB0938 = new D78E538F(0);

	private static readonly D78E538F _24003217 = new D78E538F(-1);

	private int FE23829C(D78E538F _7E11459D)
	{
		if ((_8F9A3C8B ^ _7E11459D._8F9A3C8B) < 0)
		{
			if (_8F9A3C8B >= 0)
			{
				return 1;
			}
			return -1;
		}
		if (_1D0E9C93 == null)
		{
			if (_7E11459D._1D0E9C93 == null)
			{
				if (_8F9A3C8B >= _7E11459D._8F9A3C8B)
				{
					if (_8F9A3C8B <= _7E11459D._8F9A3C8B)
					{
						return 0;
					}
					return 1;
				}
				return -1;
			}
			return -_7E11459D._8F9A3C8B;
		}
		int num;
		int num2;
		if (_7E11459D._1D0E9C93 == null || (num = EE0CA3B5(_1D0E9C93)) > (num2 = EE0CA3B5(_7E11459D._1D0E9C93)))
		{
			return _8F9A3C8B;
		}
		if (num < num2)
		{
			return -_8F9A3C8B;
		}
		int num3 = _4013410D(_1D0E9C93, _7E11459D._1D0E9C93, num);
		if (num3 == 0)
		{
			return 0;
		}
		if (_1D0E9C93[num3 - 1] >= _7E11459D._1D0E9C93[num3 - 1])
		{
			return _8F9A3C8B;
		}
		return -_8F9A3C8B;
	}

	internal static void _47AF421C(ref _6C3F5820 _01B1D624, ref _6C3F5820 _58113AAA)
	{
		_6C3F5820 _6C3F5821 = _01B1D624;
		_01B1D624 = _58113AAA;
		_58113AAA = _6C3F5821;
	}

	public static int E729DCA2(D78E538F _0418BEAC, D78E538F FF93A021)
	{
		return _0418BEAC.FE23829C(FF93A021);
	}

	private static int _4013410D(uint[] _878ADD89, uint[] C12215B9, int _0E91D483)
	{
		int num = _0E91D483;
		while (--num >= 0)
		{
			if (_878ADD89[num] != C12215B9[num])
			{
				return num + 1;
			}
		}
		return 0;
	}

	internal static D78E538F _3F85B332(D78E538F _861ED989, D78E538F _9E09F037, D78E538F C6B127AE)
	{
		if (_9E09F037._360D6E87() < 0)
		{
			throw new ArgumentOutOfRangeException();
		}
		bool flag = _9E09F037._701E993A();
		_6C3F5820 D2A32F2C = new _6C3F5820(A3B80C27._8F9A3C8B, A3B80C27._1D0E9C93);
		_6C3F5820 _3089A50C = new _6C3F5820(_861ED989._8F9A3C8B, _861ED989._1D0E9C93);
		_6C3F5820 EB86DB = new _6C3F5820(C6B127AE._8F9A3C8B, C6B127AE._1D0E9C93);
		_6C3F5820 _7129A48D = new _6C3F5820(_3089A50C._25384E0E());
		D2A32F2C._4638D390(EB86DB);
		uint num;
		if (_9E09F037._1D0E9C93 == null)
		{
			num = (uint)_9E09F037._8F9A3C8B;
		}
		else
		{
			int num2 = EE0CA3B5(_9E09F037._1D0E9C93);
			for (int i = 0; i < num2 - 1; i++)
			{
				num = _9E09F037._1D0E9C93[i];
				int num3 = 0;
				while (num3 < 32)
				{
					if ((num & 1) == 1)
					{
						_203084B1(ref D2A32F2C, ref _3089A50C, ref EB86DB, ref _7129A48D);
					}
					_39AF4203(ref _3089A50C, ref EB86DB, ref _7129A48D);
					num >>= 1;
					i++;
				}
			}
			num = _9E09F037._1D0E9C93[num2 - 1];
		}
		while (num != 0)
		{
			if ((num & 1) == 1)
			{
				_203084B1(ref D2A32F2C, ref _3089A50C, ref EB86DB, ref _7129A48D);
			}
			if (num == 1)
			{
				break;
			}
			_39AF4203(ref _3089A50C, ref EB86DB, ref _7129A48D);
			num >>= 1;
		}
		return D2A32F2C._6908E326((_861ED989._8F9A3C8B > 0) ? 1 : (flag ? 1 : (-1)));
	}

	internal D78E538F(int _3E014F29, uint[] _1B2DAF3B)
	{
		_8F9A3C8B = _3E014F29;
		_1D0E9C93 = _1B2DAF3B;
	}

	private static void _39AF4203(ref _6C3F5820 _1C3430B0, ref _6C3F5820 _6F052F10, ref _6C3F5820 _4D005A88)
	{
		_47AF421C(ref _1C3430B0, ref _4D005A88);
		_1C3430B0._3A23EE8E(_4D005A88, _4D005A88);
		_1C3430B0._4638D390(_6F052F10);
	}

	private int _360D6E87()
	{
		return (_8F9A3C8B >> 31) - (-_8F9A3C8B >> 31);
	}

	private static void _203084B1(ref _6C3F5820 D2A32F2C, ref _6C3F5820 _3089A50C, ref _6C3F5820 EB86DB07, ref _6C3F5820 _7129A48D)
	{
		_47AF421C(ref D2A32F2C, ref _7129A48D);
		D2A32F2C._3A23EE8E(_7129A48D, _3089A50C);
		D2A32F2C._4638D390(EB86DB07);
	}

	internal D78E538F(byte[] _09BFFA84)
	{
		if (_09BFFA84 == null)
		{
			throw new ArgumentNullException();
		}
		int num = _09BFFA84.Length;
		bool flag = num > 0 && (_09BFFA84[num - 1] & 0x80) == 128;
		while (num > 0 && _09BFFA84[num - 1] == 0)
		{
			num--;
		}
		if (num == 0)
		{
			_8F9A3C8B = 0;
			_1D0E9C93 = null;
			return;
		}
		if (num <= 4)
		{
			if (flag)
			{
				_8F9A3C8B = -1;
			}
			else
			{
				_8F9A3C8B = 0;
			}
			for (int num2 = num - 1; num2 >= 0; num2--)
			{
				_8F9A3C8B <<= 8;
				_8F9A3C8B |= _09BFFA84[num2];
			}
			_1D0E9C93 = null;
			if (_8F9A3C8B < 0 && !flag)
			{
				_1D0E9C93 = new uint[1];
				_1D0E9C93[0] = (uint)_8F9A3C8B;
				_8F9A3C8B = 1;
			}
			if (_8F9A3C8B == int.MinValue)
			{
				this = _39847B9C;
			}
			return;
		}
		int num3 = num % 4;
		int num4 = num / 4 + ((num3 != 0) ? 1 : 0);
		bool flag2 = true;
		uint[] array = new uint[num4];
		int num5 = 3;
		int i;
		for (i = 0; i < num4 - ((num3 != 0) ? 1 : 0); i++)
		{
			for (int j = 0; j < 4; j++)
			{
				if (_09BFFA84[num5] != 0)
				{
					flag2 = false;
				}
				array[i] = (array[i] << 8) | _09BFFA84[num5];
				num5--;
			}
			num5 += 8;
		}
		if (num3 != 0)
		{
			if (flag)
			{
				array[num4 - 1] = uint.MaxValue;
			}
			for (num5 = num - 1; num5 >= num - num3; num5--)
			{
				if (_09BFFA84[num5] != 0)
				{
					flag2 = false;
				}
				array[i] = (array[i] << 8) | _09BFFA84[num5];
			}
		}
		if (flag2)
		{
			this = _4EAB0938;
		}
		else if (flag)
		{
			_5EA8F230(array);
			int num6 = array.Length;
			while (num6 > 0 && array[num6 - 1] == 0)
			{
				num6--;
			}
			if (num6 == 1 && (int)array[0] > 0)
			{
				if (array[0] == 1)
				{
					this = _24003217;
					return;
				}
				if (array[0] == 2147483648u)
				{
					this = _39847B9C;
					return;
				}
				_8F9A3C8B = -1 * (int)array[0];
				_1D0E9C93 = null;
			}
			else if (num6 != array.Length)
			{
				_8F9A3C8B = -1;
				_1D0E9C93 = new uint[num6];
				Array.Copy(array, _1D0E9C93, num6);
			}
			else
			{
				_8F9A3C8B = -1;
				_1D0E9C93 = array;
			}
		}
		else
		{
			_8F9A3C8B = 1;
			_1D0E9C93 = array;
		}
	}

	internal byte[] EA1851AB()
	{
		if (_1D0E9C93 == null && _8F9A3C8B == 0)
		{
			return new byte[1];
		}
		uint[] array;
		byte b;
		if (_1D0E9C93 == null)
		{
			array = new uint[1] { (uint)_8F9A3C8B };
			b = (byte)((_8F9A3C8B < 0) ? 255u : 0u);
		}
		else if (_8F9A3C8B == -1)
		{
			array = (uint[])_1D0E9C93.Clone();
			_5EA8F230(array);
			b = byte.MaxValue;
		}
		else
		{
			array = _1D0E9C93;
			b = 0;
		}
		int num = ((array[^1] >> 31 != b >> 7) ? 1 : 0);
		int num2 = num + 4 * array.Length;
		byte[] array2 = new byte[num2];
		uint[] array3 = array;
		for (int i = 0; i < array3.Length; i++)
		{
			uint num3 = array3[i];
			for (int j = 0; j < 4; j++)
			{
				array2[--num2] = (byte)num3;
				num3 >>= 8;
			}
		}
		if (num != 0)
		{
			array2[0] = b;
		}
		return array2;
	}

	internal static void _5EA8F230(uint[] A6A4741E)
	{
		int i = 0;
		uint num = 0u;
		for (; i < A6A4741E.Length; i++)
		{
			num = (A6A4741E[i] = ~A6A4741E[i] + 1);
			if (num != 0)
			{
				i++;
				break;
			}
		}
		if (num != 0)
		{
			for (; i < A6A4741E.Length; i++)
			{
				A6A4741E[i] = ~A6A4741E[i];
			}
		}
		else
		{
			Array.Resize(ref A6A4741E, A6A4741E.Length + 1);
			A6A4741E[^1] = 1u;
		}
	}

	private D78E538F(int D61A0D94)
	{
		if (D61A0D94 == int.MinValue)
		{
			this = _39847B9C;
			return;
		}
		_8F9A3C8B = D61A0D94;
		_1D0E9C93 = null;
	}

	private static int EE0CA3B5(uint[] _993C9EBF)
	{
		int num = _993C9EBF.Length;
		if (_993C9EBF[num - 1] != 0)
		{
			return num;
		}
		return num - 1;
	}

	private bool _701E993A()
	{
		if (_1D0E9C93 != null)
		{
			return (_1D0E9C93[0] & 1) == 0;
		}
		return (_8F9A3C8B & 1) == 0;
	}
}
