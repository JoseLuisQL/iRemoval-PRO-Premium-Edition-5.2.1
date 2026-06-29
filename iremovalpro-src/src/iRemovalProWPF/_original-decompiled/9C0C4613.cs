using System;
using System.Collections.Generic;

public class _9C0C4613
{
	private readonly Dictionary<string, uint> _88BA5C8B;

	public _9C0C4613()
	{
		new C834A786()._3CB74B1B(new object[1] { this }, 13496);
	}

	private unsafe byte[] _311BFC08(IntPtr _6095E50E, int CE1B1D8B, uint CAA89C0A)
	{
		byte[] array = new byte[CE1B1D8B];
		byte* ptr = (byte*)(void*)_6095E50E;
		int num = 0;
		sbyte b2 = default(sbyte);
		while (true)
		{
			byte b = 112;
			uint num2;
			if (num < CE1B1D8B)
			{
				num2 = 3099534260u;
				num2 = (uint)((1448760566 >> (int)(ushort)num2) + -719188778);
			}
			else
			{
				num2 = 3432989067u >> (int)((uint)(0x5CA7EC8E | (sbyte)b) / 2006677389u);
				num2 = (uint)(~(479093531 % (int)num2) + -475736516);
			}
			while (true)
			{
				switch (num2 % 3)
				{
				case 1u:
					goto end_IL_002f;
				case 2u:
					num2 = 100964497u % (uint)(~(((b == -73890245) ? 1 : 0) - 2074589204 / (int)num2)) + 3332024570u;
					return array;
				}
				num2 = ((0xEF3E0D19u ^ ((num2 < 2660739901u % num2) ? 1u : 0u)) | (num2 << (int)((0 - num2) % 1469657650))) + 3376469051u;
				array[num] = (byte)(ptr[num] ^ (((CAA89C0A << (num & (0x1F ^ ((int)((uint)((int)num2 / 1000886825 / (int)num2) % 630282280u) + (int)num2 / ((int)num2 - ((num2 == num2) ? 1 : 0)))))) | (CAA89C0A >> (((int)(0x80000020u ^ (((num2 << (int)num2) % num2 << 7) * 10619956)) - num) & (-164800468 ^ (-164800461 % (int)(0xEF14A590u ^ num2)))))) + num));
				b2 = (sbyte)((uint)((int)(short)(0 - num2) - (int)num2) % (uint)(110589606 >>> (int)num2));
				num2 = (uint)((2141126175 >> (int)((uint)((int)((uint)b2 % 3435056516u) + -1005042668) % (uint)(605880469 % (int)num2))) - -917950506);
				continue;
				end_IL_002f:
				break;
			}
			num2 = 2804480002u / (uint)(~(111415461 << (int)b2)) + 3099534259u;
			num += ((int)num2 >> 2) - -298858260;
		}
	}
}
