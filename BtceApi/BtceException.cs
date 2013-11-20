using System;

namespace BtcE
{
	public class BtceException : Exception
	{
		public BtceException(string p)
			: base(p)
		{
		}
	}
}