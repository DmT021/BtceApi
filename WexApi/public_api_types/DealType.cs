//
// https://github.com/multiprogramm/WexAPI
//

using System;

namespace Wex
{
	public enum DealType
	{
		Ask,
		Bid
	}

	public class DealTypeHelper
	{
		public static DealType FromString(string s)
		{
			switch (s.ToLowerInvariant())
			{
				case "ask":
					return DealType.Ask;
				case "bid":
					return DealType.Bid;
				default:
					throw new ArgumentException();
			}
		}
	}
}
