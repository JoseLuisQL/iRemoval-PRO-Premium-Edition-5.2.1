using System;

[Serializable]
public class _190F0B08
{
	public byte[] UserData;

	public string UserName;

	public string EMail;

	public DateTime Expires;

	public DateTime MaxBuild;

	public int RunningTime;

	public _7604AA8F State;

	public _190F0B08()
	{
		State = _7604AA8F.Invalid;
		Expires = DateTime.MaxValue;
		MaxBuild = DateTime.MaxValue;
		RunningTime = 0;
		UserData = new byte[0];
		UserName = string.Empty;
		EMail = string.Empty;
	}
}
