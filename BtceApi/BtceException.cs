using System;

namespace BtcE
{
	[Serializable]
	public class BtceException : Exception
	{
		public BtceException(string p)
			: base(p) { }
	}
}