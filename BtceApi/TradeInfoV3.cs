// -----------------------------------------------------------------------
// <copyright file="TradeInfoV3.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BtcE
{
	using System;
	using BtcE.Utils;
	using Newtonsoft.Json.Linq;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class TradeInfoV3
	{
		public decimal Amount { get; private set; }
		public DateTime Timestamp { get; private set; }
		public decimal Price { get; private set; }
		public UInt32 Tid { get; private set; }
		public TradeInfoType Type { get; private set; }

		public static TradeInfoV3 ReadFromJObject(JObject o)
		{
			if (o == null)
				return null;

			return new TradeInfoV3()
			{
				Amount = o.Value<decimal>("amount"),
				Price = o.Value<decimal>("price"),
				Timestamp = UnixTime.ConvertToDateTime(o.Value<UInt32>("timestamp")),
				Tid = o.Value<UInt32>("tid"),
				Type = TradeInfoTypeHelper.FromString(o.Value<string>("type"))
			};
		}
	}
}