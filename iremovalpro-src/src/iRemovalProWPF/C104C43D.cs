using System;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

public class C104C43D
{
	public class _55266B0E : _803BA502
	{
		private string B3ABEF3C;
	}

	public class _803BA502
	{
		[Serializable]
		private sealed class _23B55582
		{
			public static RemoteCertificateValidationCallback _003C_003E9__4_0;

			public static readonly _23B55582 _003C_003E9 = new _23B55582();

			internal bool FB8AFC1E(object _951062B4, X509Certificate CE330681, X509Chain _59B57D1F, SslPolicyErrors _72041FBB)
			{
				return true;
			}
		}

		private string EEB33899;

		private string _800D48B8;

		public string _9EB16F3D
		{
			get
			{
				return EEB33899;
			}
			private set
			{
				EEB33899 = value;
			}
		}

		public string _8D0CCB29
		{
			get
			{
				return _800D48B8;
			}
			private set
			{
				_800D48B8 = value;
			}
		}

		private void _6C167EAB(string _57B6450D, bool C60A5194)
		{
			if (C60A5194)
			{
				StringBuilder stringBuilder = new StringBuilder(_8D0CCB29);
				foreach (char c in _57B6450D)
				{
					switch (c)
					{
					case '+':
						stringBuilder.Append("%2B");
						break;
					case '/':
						stringBuilder.Append("%2F");
						break;
					case '=':
						stringBuilder.Append("%3D");
						break;
					default:
						stringBuilder.Append(c);
						break;
					}
				}
				_8D0CCB29 = stringBuilder.ToString();
			}
			else
			{
				_8D0CCB29 += _57B6450D;
			}
		}

		protected void _942050BC()
		{
			_8D0CCB29 = Convert.ToBase64String(Encoding.UTF8.GetBytes(_8D0CCB29));
		}

		protected void _85150F09(string _8FB95787, string F8B1FAB8)
		{
			_6C167EAB(_8FB95787, C60A5194: false);
			_6C167EAB(F8B1FAB8, C60A5194: true);
		}

		protected bool _4ABB30B0(byte[] DDA0D9A3)
		{
			int num = (int)_6503BD19._9B3AA997(DDA0D9A3, 32);
			if (num == 0)
			{
				return false;
			}
			int index = (int)_6503BD19._9B3AA997(DDA0D9A3, 28);
			_8D0CCB29 = Encoding.UTF8.GetString(DDA0D9A3, index, num);
			if (_8D0CCB29[_8D0CCB29.Length - 1] != '/')
			{
				_8D0CCB29 += "/";
			}
			return true;
		}
	}

	public class _790D4F0D : _803BA502
	{
	}

	private readonly int _3F20AC84;

	private _7604AA8F A1B8A230;

	private ulong _4A11BF19;

	private readonly object _1F8F573F;

	private int _1432110A;

	private uint _14285A10;

	private byte[] D590DC87;

	private readonly byte[] C9334A2A;

	public C104C43D()
	{
		new C834A786()._3CB74B1B(new object[1] { this }, 151480);
	}
}
