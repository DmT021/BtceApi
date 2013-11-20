using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

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

		public static Trade ReadFromJObject(JObject o)
		{
			if (o == null)
				return null;
			return new Trade()
			{
				Pair = BtcePairHelper.FromString(o.Value<string>("pair")),
				Type = TradeTypeHelper.FromString(o.Value<string>("type")),
				Amount = o.Value<decimal>("amount"),
				Rate = o.Value<decimal>("rate"),
				Timestamp = o.Value<UInt32>("timestamp"),
				IsYourOrder = o.Value<int>("is_your_order") == 1,
				OrderId = o.Value<int>("order_id")
			};
		}

		public override string ToString()
		{
			return string.Format("{0} {1} [{2}] {3}x{4} {{{5}}} {6}",
				this.Pair,
				this.Timestamp,
				this.Type,
				this.Rate,
				this.Amount,
				this.OrderId,
				this.IsYourOrder);
		}

		public Trade()
		{
		}

		public Trade(BtcePair pair, TradeType type, decimal amount, decimal rate, int orderId, bool isYourOrder, UInt32 timestamp)
		{
			Pair = pair;
			Type = type;
			Amount = amount;
			Rate = rate;
			OrderId = orderId;
			IsYourOrder = isYourOrder;
			Timestamp = timestamp;
		}
	}

	public class TradeHistory
	{
		public Dictionary<int, Trade> List { get; private set; }

		public static TradeHistory ReadFromJObject(JObject o)
		{
			return new TradeHistory()
			{
				List = ((IDictionary<string, JToken>)o).ToDictionary(item => int.Parse(item.Key), item => Trade.ReadFromJObject(item.Value as JObject))
			};
		}

		public override string ToString()
		{
			return string.Format("Trades: {0}", this.List.Count);
		}
	}
}