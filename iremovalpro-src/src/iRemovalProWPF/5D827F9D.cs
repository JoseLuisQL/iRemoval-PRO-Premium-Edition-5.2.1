using System;
using System.IO;

public class _5D827F9D
{
	private class B425693D
	{
		private _0F0B9EAC _7E87D709;

		private uint _1522578A;

		private readonly _29A49A08[] _0FB8D9A4 = new _29A49A08[16];

		private _29A49A08 _519F792B = new _29A49A08(8);

		private readonly _29A49A08[] D60DE2B2 = new _29A49A08[16];

		private _0F0B9EAC _85A12115;

		public void _0922F5BC()
		{
			_85A12115._23BF982B();
			for (uint num = 0u; num < _1522578A; num++)
			{
				_0FB8D9A4[num].C7A13639();
				D60DE2B2[num].C7A13639();
			}
			_7E87D709._23BF982B();
			_519F792B.C7A13639();
		}

		public uint _4125733D(_7C902113 F1977623, uint _6BA76CAC)
		{
			if (_85A12115._72153F06(F1977623) == 0)
			{
				return _0FB8D9A4[_6BA76CAC]._39206523(F1977623);
			}
			uint num = 8u;
			if (_7E87D709._72153F06(F1977623) == 0)
			{
				return num + D60DE2B2[_6BA76CAC]._39206523(F1977623);
			}
			num += 8;
			return num + _519F792B._39206523(F1977623);
		}

		public void _0503CB88(uint _3A307607)
		{
			for (uint num = _1522578A; num < _3A307607; num++)
			{
				_0FB8D9A4[num] = new _29A49A08(3);
				D60DE2B2[num] = new _29A49A08(3);
			}
			_1522578A = _3A307607;
		}
	}

	private class BE84A927
	{
		private struct _51064527
		{
			private _0F0B9EAC[] _0B32A12C;

			public byte _9BA19EB7(_7C902113 _22B7340C)
			{
				uint num = 1u;
				do
				{
					num = (num << 1) | _0B32A12C[num]._72153F06(_22B7340C);
				}
				while (num < 256);
				return (byte)num;
			}

			public void C4359707()
			{
				for (int i = 0; i < 768; i++)
				{
					_0B32A12C[i]._23BF982B();
				}
			}

			public byte F2A8F529(_7C902113 _5F85DA04, byte D6A62327)
			{
				uint num = 1u;
				do
				{
					uint num2 = (uint)(D6A62327 >> 7) & 1u;
					D6A62327 <<= 1;
					uint num3 = _0B32A12C[(1 + num2 << 8) + num]._72153F06(_5F85DA04);
					num = (num << 1) | num3;
					if (num2 != num3)
					{
						while (num < 256)
						{
							num = (num << 1) | _0B32A12C[num]._72153F06(_5F85DA04);
						}
						break;
					}
				}
				while (num < 256);
				return (byte)num;
			}

			public void A6961E8D()
			{
				_0B32A12C = new _0F0B9EAC[768];
			}
		}

		private _51064527[] D43D388D;

		private uint _8881969A;

		private int D0A41E96;

		private int _110A3500;

		private uint _03B6F98F = 1u;

		private uint _0D2B631B(uint _0527AA24, byte _6E222424)
		{
			return ((_0527AA24 & _8881969A) << _110A3500) + (uint)(_6E222424 >> 8 - _110A3500);
		}

		public byte F0B945A1(_7C902113 _3A04F1B5, uint _8B36FF81, byte F7BB2C07)
		{
			return D43D388D[_0D2B631B(_8B36FF81, F7BB2C07)]._9BA19EB7(_3A04F1B5);
		}

		public byte _2D1D08AF(_7C902113 _3E842019, uint _140974A5, byte DD3C8B89, byte D10E97A2)
		{
			return D43D388D[_0D2B631B(_140974A5, DD3C8B89)].F2A8F529(_3E842019, D10E97A2);
		}

		public void _9E265330()
		{
			uint num = (uint)(1 << _110A3500 + D0A41E96);
			for (uint num2 = 0u; num2 < num; num2++)
			{
				D43D388D[num2].C4359707();
			}
		}

		public void B2391DA5(int _8BA5830B, int CFAA6D28)
		{
			if (D43D388D == null || _110A3500 != CFAA6D28 || D0A41E96 != _8BA5830B)
			{
				D0A41E96 = _8BA5830B;
				_8881969A = (uint)((1 << _8BA5830B) - 1);
				_110A3500 = CFAA6D28;
				uint num = (uint)(1 << _110A3500 + D0A41E96);
				D43D388D = new _51064527[num];
				for (uint num2 = 0u; num2 < num; num2++)
				{
					D43D388D[num2].A6961E8D();
				}
			}
		}
	}

	private readonly _29A49A08[] _563A8F20 = new _29A49A08[4];

	private readonly _0F0B9EAC[] _1A9DE0B0 = new _0F0B9EAC[12];

	private readonly _7C902113 _77327793 = new _7C902113();

	private readonly _0F0B9EAC[] FB1E8F1F = new _0F0B9EAC[12];

	private uint _450CB508;

	private readonly _0F0B9EAC[] _0EB23199 = new _0F0B9EAC[192];

	private readonly _0F0B9EAC[] _0CA72603 = new _0F0B9EAC[114];

	private readonly _0F0B9EAC[] _5A922A3C = new _0F0B9EAC[192];

	private readonly _0F0B9EAC[] C61745B9 = new _0F0B9EAC[12];

	private readonly BE84A927 EB995622 = new BE84A927();

	private readonly _0F0B9EAC[] B834F39B = new _0F0B9EAC[12];

	private uint _89B52B32;

	private readonly _9481CE26 B8A4611B = new _9481CE26();

	private readonly B425693D _090EEF8A = new B425693D();

	private readonly B425693D _0E99188F = new B425693D();

	private _29A49A08 _3D371627 = new _29A49A08(4);

	private uint F5AC2992;

	private uint _893CB408 = 1u;

	public void F310B920(byte[] _52953007)
	{
		if (_52953007.Length < 5)
		{
			throw new ArgumentException();
		}
		int _6016273C = _52953007[0] % 9;
		int num = _52953007[0] / 9;
		int f80EC = num % 5;
		int num2 = num / 5;
		if (num2 > 4)
		{
			throw new ArgumentException();
		}
		uint num3 = 0u;
		for (int i = 0; i < 4; i++)
		{
			num3 += (uint)(_52953007[1 + i] << i * 8);
		}
		B219C98F(num3);
		_9894BA38(f80EC, _6016273C);
		_869E7019(num2);
	}

	public void _258D1920(Stream _87B1FB8F, Stream E31C2AAC, long F7836B3F)
	{
		_1BB69C22(_87B1FB8F, E31C2AAC);
		FA9AD1B0._3A93322F _3A93322F = default(FA9AD1B0._3A93322F);
		_3A93322F._9901F3AC();
		uint num = 0u;
		uint num2 = 0u;
		uint num3 = 0u;
		uint num4 = 0u;
		ulong num5 = 0uL;
		if (num5 < (ulong)F7836B3F)
		{
			if (_5A922A3C[_3A93322F.F5B5CD02 << 4]._72153F06(_77327793) != 0)
			{
				throw new InvalidDataException();
			}
			_3A93322F._860B540F();
			byte c108DDBA = EB995622.F0B945A1(_77327793, 0u, 0);
			B8A4611B._59A0E127(c108DDBA);
			num5++;
		}
		while (num5 < (ulong)F7836B3F)
		{
			uint num6 = (uint)(int)num5 & _450CB508;
			if (_5A922A3C[(_3A93322F.F5B5CD02 << 4) + num6]._72153F06(_77327793) == 0)
			{
				byte b = B8A4611B.ABA37B35(0u);
				byte c108DDBA2 = (_3A93322F.A6951E81() ? EB995622.F0B945A1(_77327793, (uint)num5, b) : EB995622._2D1D08AF(_77327793, (uint)num5, b, B8A4611B.ABA37B35(num)));
				B8A4611B._59A0E127(c108DDBA2);
				_3A93322F._860B540F();
				num5++;
				continue;
			}
			uint num8;
			if (FB1E8F1F[_3A93322F.F5B5CD02]._72153F06(_77327793) == 1)
			{
				if (B834F39B[_3A93322F.F5B5CD02]._72153F06(_77327793) == 0)
				{
					if (_0EB23199[(_3A93322F.F5B5CD02 << 4) + num6]._72153F06(_77327793) == 0)
					{
						_3A93322F._0606113D();
						B8A4611B._59A0E127(B8A4611B.ABA37B35(num));
						num5++;
						continue;
					}
				}
				else
				{
					uint num7;
					if (_1A9DE0B0[_3A93322F.F5B5CD02]._72153F06(_77327793) == 0)
					{
						num7 = num2;
					}
					else
					{
						if (C61745B9[_3A93322F.F5B5CD02]._72153F06(_77327793) == 0)
						{
							num7 = num3;
						}
						else
						{
							num7 = num4;
							num4 = num3;
						}
						num3 = num2;
					}
					num2 = num;
					num = num7;
				}
				num8 = _0E99188F._4125733D(_77327793, num6) + 2;
				_3A93322F._42AFCC0A();
			}
			else
			{
				num4 = num3;
				num3 = num2;
				num2 = num;
				num8 = 2 + _090EEF8A._4125733D(_77327793, num6);
				_3A93322F._5BBA721E();
				uint num9 = _563A8F20[FA9AD1B0._848D1984(num8)]._39206523(_77327793);
				if (num9 >= 4)
				{
					int num10 = (int)((num9 >> 1) - 1);
					num = (2 | (num9 & 1)) << num10;
					if (num9 < 14)
					{
						num += _29A49A08._91A5A19F(_0CA72603, num - num9 - 1, _77327793, num10);
					}
					else
					{
						num += _77327793._6E1858AB(num10 - 4) << 4;
						num += _3D371627._16B72701(_77327793);
					}
				}
				else
				{
					num = num9;
				}
			}
			if (num >= B8A4611B._603A7C26 + num5 || num >= F5AC2992)
			{
				if (num == uint.MaxValue)
				{
					break;
				}
				throw new InvalidDataException();
			}
			B8A4611B._5D370A84(num, num8);
			num5 += num8;
		}
		B8A4611B._68B54AAD();
		B8A4611B.BDABB522();
		_77327793._57847F3E();
	}

	private void _1BB69C22(Stream A79B3C02, Stream B73CD488)
	{
		_77327793._9F07BBA7(A79B3C02);
		B8A4611B.E31A2B1F(B73CD488, A3861E33: false);
		for (uint num = 0u; num < 12; num++)
		{
			for (uint num2 = 0u; num2 <= _450CB508; num2++)
			{
				uint num3 = (num << 4) + num2;
				_5A922A3C[num3]._23BF982B();
				_0EB23199[num3]._23BF982B();
			}
			FB1E8F1F[num]._23BF982B();
			B834F39B[num]._23BF982B();
			_1A9DE0B0[num]._23BF982B();
			C61745B9[num]._23BF982B();
		}
		EB995622._9E265330();
		for (uint num = 0u; num < 4; num++)
		{
			_563A8F20[num].C7A13639();
		}
		for (uint num = 0u; num < 114; num++)
		{
			_0CA72603[num]._23BF982B();
		}
		_090EEF8A._0922F5BC();
		_0E99188F._0922F5BC();
		_3D371627.C7A13639();
	}

	private void B219C98F(uint _7232AB35)
	{
		if (_89B52B32 != _7232AB35)
		{
			_89B52B32 = _7232AB35;
			F5AC2992 = Math.Max(_89B52B32, 1u);
			uint _8D0D188B = Math.Max(F5AC2992, 4096u);
			B8A4611B.E811978B(_8D0D188B);
		}
	}

	private void _9894BA38(int F80EC992, int _6016273C)
	{
		if (F80EC992 > 8)
		{
			throw new ArgumentException();
		}
		if (_6016273C > 8)
		{
			throw new ArgumentException();
		}
		EB995622.B2391DA5(F80EC992, _6016273C);
	}

	private void _869E7019(int _8E3D7BB5)
	{
		if (_8E3D7BB5 > 4)
		{
			throw new ArgumentException();
		}
		uint num = (uint)(1 << _8E3D7BB5);
		_090EEF8A._0503CB88(num);
		_0E99188F._0503CB88(num);
		_450CB508 = num - 1;
	}

	public _5D827F9D()
	{
		_89B52B32 = uint.MaxValue;
		for (int i = 0; (long)i < 4L; i++)
		{
			_563A8F20[i] = new _29A49A08(6);
		}
	}
}
