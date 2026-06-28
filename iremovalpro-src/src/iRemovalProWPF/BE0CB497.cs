public class BE0CB497
{
	private uint _4705F3B8;

	public uint _8C8BDF26(uint _0E0F5B31)
	{
		uint num = _0E0F5B31 ^ _4705F3B8;
		_4705F3B8 = ((_4705F3B8 << 7) | (_4705F3B8 >> 25)) ^ num;
		return num;
	}

	public BE0CB497()
	{
		_4705F3B8 = 67315479u;
	}
}
