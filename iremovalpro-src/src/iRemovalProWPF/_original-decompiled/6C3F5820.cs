using System;

internal struct _6C3F5820
{
	private int _6A8E922A;

	private uint _1B159A0E;

	private uint[] _4B33C13D;

	private bool FF195302;

	private void AF22A223(int FB1F5717)
	{
		if (FB1F5717 <= 1)
		{
			_6A8E922A = 0;
			_1B159A0E = 0u;
			return;
		}
		if (!FF195302 || _4B33C13D.Length < FB1F5717)
		{
			_4B33C13D = new uint[FB1F5717];
			FF195302 = true;
		}
		else
		{
			Array.Clear(_4B33C13D, 0, FB1F5717);
		}
		_6A8E922A = FB1F5717 - 1;
	}

	private void FD00A022(ulong _3039083C)
	{
		uint num = (uint)(_3039083C >> 32);
		if (num == 0)
		{
			_1B159A0E = (uint)_3039083C;
			_6A8E922A = 0;
		}
		else
		{
			_8B3A74A7(2);
			_4B33C13D[0] = (uint)_3039083C;
			_4B33C13D[1] = num;
		}
	}

	internal void _3A23EE8E(_6C3F5820 FC8A241A, _6C3F5820 E3156DA7)
	{
		if (FC8A241A._6A8E922A == 0)
		{
			if (E3156DA7._6A8E922A == 0)
			{
				FD00A022((ulong)FC8A241A._1B159A0E * (ulong)E3156DA7._1B159A0E);
				return;
			}
			_2C8237A2(E3156DA7, 1);
			_2B946D14(FC8A241A._1B159A0E);
			return;
		}
		if (E3156DA7._6A8E922A == 0)
		{
			_2C8237A2(FC8A241A, 1);
			_2B946D14(E3156DA7._1B159A0E);
			return;
		}
		AF22A223(FC8A241A._6A8E922A + E3156DA7._6A8E922A + 2);
		uint[] array;
		int num;
		uint[] array2;
		int num2;
		if (FC8A241A._9FAE8E19() <= E3156DA7._9FAE8E19())
		{
			array = FC8A241A._4B33C13D;
			num = FC8A241A._6A8E922A + 1;
			array2 = E3156DA7._4B33C13D;
			num2 = E3156DA7._6A8E922A + 1;
		}
		else
		{
			array = E3156DA7._4B33C13D;
			num = E3156DA7._6A8E922A + 1;
			array2 = FC8A241A._4B33C13D;
			num2 = FC8A241A._6A8E922A + 1;
		}
		for (int i = 0; i < num; i++)
		{
			uint num3 = array[i];
			if (num3 != 0)
			{
				uint num4 = 0u;
				int num5 = i;
				int num6 = 0;
				while (num6 < num2)
				{
					ulong num7 = (ulong)(_4B33C13D[num5] + (long)num3 * (long)array2[num6] + num4);
					_4B33C13D[num5] = (uint)num7;
					num4 = (uint)(num7 >> 32);
					num6++;
					num5++;
				}
				while (num4 != 0)
				{
					ulong num8 = (ulong)_4B33C13D[num5] + (ulong)num4;
					_4B33C13D[num5++] = (uint)num8;
					num4 = (uint)(num8 >> 32);
				}
			}
		}
		_94A53411();
	}

	internal int _25384E0E()
	{
		return _6A8E922A + 1;
	}

	internal _6C3F5820(int _95B103A2, uint[] _929B9A89)
	{
		FF195302 = false;
		_4B33C13D = _929B9A89;
		int num = _95B103A2 >> 31;
		if (_4B33C13D == null)
		{
			_6A8E922A = 0;
			_1B159A0E = (uint)((_95B103A2 ^ num) - num);
			return;
		}
		_6A8E922A = _4B33C13D.Length - 1;
		_1B159A0E = _4B33C13D[0];
		while (_6A8E922A > 0 && _4B33C13D[_6A8E922A] == 0)
		{
			_6A8E922A--;
		}
	}

	internal static int _7B838205(uint _060DEE2A)
	{
		if (_060DEE2A == 0)
		{
			return 32;
		}
		int num = 0;
		if ((_060DEE2A & 0xFFFF0000u) == 0)
		{
			num += 16;
			_060DEE2A <<= 16;
		}
		if ((_060DEE2A & 0xFF000000u) == 0)
		{
			num += 8;
			_060DEE2A <<= 8;
		}
		if ((_060DEE2A & 0xF0000000u) == 0)
		{
			num += 4;
			_060DEE2A <<= 4;
		}
		if ((_060DEE2A & 0xC0000000u) == 0)
		{
			num += 2;
			_060DEE2A <<= 2;
		}
		if ((_060DEE2A & 0x80000000u) == 0)
		{
			num++;
		}
		return num;
	}

	private void _94A53411()
	{
		if (_6A8E922A > 0 && _4B33C13D[_6A8E922A] == 0)
		{
			_1B159A0E = _4B33C13D[0];
			while (--_6A8E922A > 0 && _4B33C13D[_6A8E922A] == 0)
			{
			}
		}
	}

	private void _2B946D14(uint _4BBE58BB)
	{
		switch (_4BBE58BB)
		{
		case 0u:
			_0E34723C(0u);
			return;
		case 1u:
			return;
		}
		if (_6A8E922A == 0)
		{
			FD00A022((ulong)_1B159A0E * (ulong)_4BBE58BB);
			return;
		}
		DC23D89A(1);
		uint num = 0u;
		for (int i = 0; i <= _6A8E922A; i++)
		{
			ulong num2 = (ulong)((long)_4B33C13D[i] * (long)_4BBE58BB + num);
			_4B33C13D[i] = (uint)num2;
			num = (uint)(num2 >> 32);
		}
		if (num != 0)
		{
			_86B5BD32(_6A8E922A + 2, 0);
			_4B33C13D[_6A8E922A] = num;
		}
	}

	private void _183B7134(int _051B8987, out int _560B8806, out uint[] _76854181)
	{
		if (_6A8E922A == 0)
		{
			if (_1B159A0E <= int.MaxValue)
			{
				_560B8806 = _051B8987 * (int)_1B159A0E;
				_76854181 = null;
				return;
			}
			if (_4B33C13D == null)
			{
				_4B33C13D = new uint[1] { _1B159A0E };
			}
			else if (FF195302)
			{
				_4B33C13D[0] = _1B159A0E;
			}
			else if (_4B33C13D[0] != _1B159A0E)
			{
				_4B33C13D = new uint[1] { _1B159A0E };
			}
		}
		_560B8806 = _051B8987;
		int num = _4B33C13D.Length - _6A8E922A - 1;
		if (num <= 1)
		{
			if (num == 0 || _4B33C13D[_6A8E922A + 1] == 0)
			{
				FF195302 = false;
				_76854181 = _4B33C13D;
				return;
			}
			if (FF195302)
			{
				_4B33C13D[_6A8E922A + 1] = 0u;
				FF195302 = false;
				_76854181 = _4B33C13D;
				return;
			}
		}
		_76854181 = _4B33C13D;
		Array.Resize(ref _76854181, _6A8E922A + 1);
		if (!FF195302)
		{
			_4B33C13D = _76854181;
		}
	}

	internal void _4638D390(_6C3F5820 _7B2D0788)
	{
		if (_7B2D0788._6A8E922A == 0)
		{
			BA0F7E31(_7B2D0788._1B159A0E);
		}
		else
		{
			if (_6A8E922A == 0 || _6A8E922A < _7B2D0788._6A8E922A)
			{
				return;
			}
			int num = _7B2D0788._6A8E922A + 1;
			int num2 = _6A8E922A - _7B2D0788._6A8E922A;
			int num3 = num2;
			int num4 = _6A8E922A;
			while (true)
			{
				if (num4 < num2)
				{
					num3++;
					break;
				}
				if (_7B2D0788._4B33C13D[num4 - num2] != _4B33C13D[num4])
				{
					if (_7B2D0788._4B33C13D[num4 - num2] < _4B33C13D[num4])
					{
						num3++;
					}
					break;
				}
				num4--;
			}
			if (num3 == 0)
			{
				return;
			}
			uint num5 = _7B2D0788._4B33C13D[num - 1];
			uint num6 = _7B2D0788._4B33C13D[num - 2];
			int num7 = _7B838205(num5);
			int num8 = 32 - num7;
			if (num7 > 0)
			{
				num5 = (num5 << num7) | (num6 >> num8);
				num6 <<= num7;
				if (num > 2)
				{
					num6 |= _7B2D0788._4B33C13D[num - 3] >> num8;
				}
			}
			DC23D89A();
			int num9 = num3;
			while (--num9 >= 0)
			{
				uint num10 = ((num9 + num <= _6A8E922A) ? _4B33C13D[num9 + num] : 0u);
				ulong num11 = ((ulong)num10 << 32) | _4B33C13D[num9 + num - 1];
				uint num12 = _4B33C13D[num9 + num - 2];
				if (num7 > 0)
				{
					num11 = (num11 << num7) | (num12 >> num8);
					num12 <<= num7;
					if (num9 + num >= 3)
					{
						num12 |= _4B33C13D[num9 + num - 3] >> num8;
					}
				}
				ulong num13 = num11 / num5;
				ulong num14 = (uint)(num11 % num5);
				if (num13 > uint.MaxValue)
				{
					num14 += num5 * (num13 - uint.MaxValue);
					num13 = 4294967295uL;
				}
				for (; num14 <= uint.MaxValue && num13 * num6 > ((num14 << 32) | num12); num14 += num5)
				{
					num13--;
				}
				if (num13 == 0)
				{
					continue;
				}
				ulong num15 = 0uL;
				for (int i = 0; i < num; i++)
				{
					num15 += _7B2D0788._4B33C13D[i] * num13;
					uint num16 = (uint)num15;
					num15 >>= 32;
					if (_4B33C13D[num9 + i] < num16)
					{
						num15++;
					}
					_4B33C13D[num9 + i] -= num16;
				}
				if (num10 < num15)
				{
					uint num17 = 0u;
					for (int j = 0; j < num; j++)
					{
						ulong num18 = (ulong)((long)_4B33C13D[num9 + j] + (long)_7B2D0788._4B33C13D[j] + num17);
						_4B33C13D[num9 + j] = (uint)num18;
						num17 = (uint)(num18 >> 32);
					}
				}
				_6A8E922A = num9 + num - 1;
			}
			_6A8E922A = num - 1;
			_94A53411();
		}
	}

	private void DC23D89A(int B93F8432 = 0)
	{
		if (!FF195302)
		{
			uint[] destinationArray = new uint[_6A8E922A + 1 + B93F8432];
			Array.Copy(_4B33C13D, destinationArray, _6A8E922A + 1);
			_4B33C13D = destinationArray;
			FF195302 = true;
		}
	}

	private void _2C8237A2(_6C3F5820 E592C21D, int _9B12198E)
	{
		if (E592C21D._6A8E922A == 0)
		{
			_1B159A0E = E592C21D._1B159A0E;
			_6A8E922A = 0;
			return;
		}
		if (!FF195302 || _4B33C13D.Length <= E592C21D._6A8E922A)
		{
			_4B33C13D = new uint[E592C21D._6A8E922A + 1 + _9B12198E];
			FF195302 = true;
		}
		_6A8E922A = E592C21D._6A8E922A;
		Array.Copy(E592C21D._4B33C13D, _4B33C13D, _6A8E922A + 1);
	}

	private int _9FAE8E19()
	{
		int num = 0;
		for (int num2 = _6A8E922A; num2 >= 0; num2--)
		{
			if (_4B33C13D[num2] != 0)
			{
				num++;
			}
		}
		return num;
	}

	internal D78E538F _6908E326(int A31FD481)
	{
		_183B7134(A31FD481, out A31FD481, out var _54169891);
		return new D78E538F(A31FD481, _54169891);
	}

	private void _86B5BD32(int EB3E9F97, int C794DC36)
	{
		if (EB3E9F97 <= 1)
		{
			if (_6A8E922A > 0)
			{
				_1B159A0E = _4B33C13D[0];
			}
			_6A8E922A = 0;
			return;
		}
		if (!FF195302 || _4B33C13D.Length < EB3E9F97)
		{
			uint[] array = new uint[EB3E9F97 + C794DC36];
			if (_6A8E922A == 0)
			{
				array[0] = _1B159A0E;
			}
			else
			{
				Array.Copy(_4B33C13D, array, Math.Min(EB3E9F97, _6A8E922A + 1));
			}
			_4B33C13D = array;
			FF195302 = true;
		}
		else if (_6A8E922A + 1 < EB3E9F97)
		{
			Array.Clear(_4B33C13D, _6A8E922A + 1, EB3E9F97 - _6A8E922A - 1);
			if (_6A8E922A == 0)
			{
				_4B33C13D[0] = _1B159A0E;
			}
		}
		_6A8E922A = EB3E9F97 - 1;
	}

	private void _0E34723C(uint AA8D23A2)
	{
		_1B159A0E = AA8D23A2;
		_6A8E922A = 0;
	}

	private void _8B3A74A7(int _2E115C2C)
	{
		if (_2E115C2C <= 1)
		{
			_6A8E922A = 0;
			return;
		}
		if (!FF195302 || _4B33C13D.Length < _2E115C2C)
		{
			_4B33C13D = new uint[_2E115C2C];
			FF195302 = true;
		}
		_6A8E922A = _2E115C2C - 1;
	}

	private void BA0F7E31(uint DF28DBA5)
	{
		if (DF28DBA5 == 1)
		{
			_0E34723C(0u);
			return;
		}
		if (_6A8E922A == 0)
		{
			_0E34723C(_1B159A0E % DF28DBA5);
			return;
		}
		ulong num = 0uL;
		for (int num2 = _6A8E922A; num2 >= 0; num2--)
		{
			num = (num << 32) | _4B33C13D[num2];
			num %= DF28DBA5;
		}
		_0E34723C((uint)num);
	}

	internal _6C3F5820(int DC264B2C)
	{
		_6A8E922A = 0;
		_1B159A0E = 0u;
		if (DC264B2C > 1)
		{
			_4B33C13D = new uint[DC264B2C];
			FF195302 = true;
		}
		else
		{
			_4B33C13D = null;
			FF195302 = false;
		}
	}
}
