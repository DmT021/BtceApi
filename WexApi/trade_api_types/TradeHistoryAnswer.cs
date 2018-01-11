//
// https://github.com/multiprogramm/WexAPI
//

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Wex
{
	public class Trade
	{
		public WexPair Pair { get; private set; }
		public TradeType Type { get; private set; }
		public decimal Amount { get; private set; }
		public decimal Rate { get; private set; }
		public int OrderId { get; private set; }
		public bool IsYourOrder { get; private set; }
		public UInt32 Timestamp { get; private set; }

		public static Trade ReadFromJObject(JObject o) 
		{
			if ( o == null )
				return null;

			return new Trade() {
				Pair = WexPairHelper.FromString(o.Value<string>("pair")),
				Type = TradeTypeHelper.FromString(o.Value<string>("type")),
				Amount = o.Value<decimal>("amount"),
				Rate = o.Value<decimal>("rate"),
				Timestamp = o.Value<UInt32>("timestamp"),
				IsYourOrder = o.Value<int>("is_your_order") == 1,
				OrderId = o.Value<int>("order_id")
			};
		}
	}
	public class TradeHistoryAnswer
	{
		public Dictionary<int, Trade> List { get; private set; }
		public static TradeHistoryAnswer ReadFromJObject(JObject o)
		{
			var sel = o.OfType<JProperty>().Select(
				x => new KeyValuePair<int, Trade>(
					int.Parse(x.Name),
					Trade.ReadFromJObject(x.Value as JObject)
				)
			);

			return new TradeHistoryAnswer()
			{
				List = sel.ToDictionary(x => x.Key, x => x.Value)
			};
		}
	}
}
