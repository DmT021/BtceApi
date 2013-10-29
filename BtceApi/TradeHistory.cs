﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BtcE
{
	public class Trade
	{
		public BtcePair Pair { get; private set; }
		public TradeType Type { get; private set; }
		public decimal Amount { get; private set; }
		public decimal Rate { get; private set; }
		public int OrderId { get; private set; }
		public bool IsYourOrder { get; private set; }
		public UInt32 Timestamp { get; private set; }
		public static Trade ReadFromJObject(JObject o) {
			if ( o == null )
				return null;
			return new Trade() {
				Pair = BtcePairHelper.FromString(o.Value<string>("pair")),
				Type = TradeTypeHelper.FromString(o.Value<string>("type")),
				Amount = o.Value<decimal>("amount"),
				Rate = o.Value<decimal>("rate"),
				Timestamp = o.Value<UInt32>("timestamp"),
				IsYourOrder = o.Value<int>("is_your_order") == 1,
				OrderId = o.Value<int>("order_id")
			};
		}
	}
	public class TradeHistory
	{
		public Dictionary<int, Trade> List { get; private set; }
		public static TradeHistory ReadFromJObject(JObject o)
		{
			TradeHistory trade_hist = new TradeHistory();
            trade_hist.List = new Dictionary<int, Trade>();

            foreach (KeyValuePair<string, JToken> element in o)
            {
                trade_hist.List.Add(int.Parse(element.Key), Trade.ReadFromJObject(element.Value as JObject));
            }

            return trade_hist;
		}
	}
}
