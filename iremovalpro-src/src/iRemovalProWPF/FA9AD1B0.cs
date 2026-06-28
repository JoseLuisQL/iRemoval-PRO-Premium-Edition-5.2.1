internal abstract class FA9AD1B0
{
	public struct _3A93322F
	{
		public uint F5B5CD02;

		public void _42AFCC0A()
		{
			F5B5CD02 = ((F5B5CD02 < 7) ? 8u : 11u);
		}

		public void _0606113D()
		{
			F5B5CD02 = ((F5B5CD02 < 7) ? 9u : 11u);
		}

		public bool A6951E81()
		{
			return F5B5CD02 < 7;
		}

		public void _5BBA721E()
		{
			F5B5CD02 = ((F5B5CD02 < 7) ? 7u : 10u);
		}

		public void _860B540F()
		{
			if (F5B5CD02 < 4)
			{
				F5B5CD02 = 0u;
			}
			else if (F5B5CD02 < 10)
			{
				F5B5CD02 -= 3u;
			}
			else
			{
				F5B5CD02 -= 6u;
			}
		}

		public void _9901F3AC()
		{
			F5B5CD02 = 0u;
		}
	}

	public static uint _848D1984(uint _5F80E820)
	{
		_5F80E820 -= 2;
		if (_5F80E820 < 4)
		{
			return _5F80E820;
		}
		return 3u;
	}
}
