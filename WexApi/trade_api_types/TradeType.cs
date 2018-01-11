//
// https://github.com/multiprogramm/WexAPI
//

using System;

namespace Wex
{
	public enum TradeType
	{
		Sell,
		Buy
	}

	public class TradeTypeHelper
	{
		public static TradeType FromString(string s)
		{
			switch ( s.ToLowerInvariant() ) 
			{
				case "sell":
					return TradeType.Sell;

				case "buy":
					return TradeType.Buy;

				default:
					throw new ArgumentException();
			}
		}

		public static string ToString(TradeType v)
		{
			return Enum.GetName(typeof(TradeType), v).ToLowerInvariant();
		}
	}
}
