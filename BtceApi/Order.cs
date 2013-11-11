using System;
using Newtonsoft.Json.Linq;

namespace BtcE
{
	public class Order
	{
		public BtcePair Pair { get; private set; }
		public TradeType Type { get; private set; }
		public decimal Amount { get; private set; }
		public decimal Rate { get; private set; }
		public UInt32 TimestampCreated { get; private set; }
		public int Status { get; private set; }

		public Order() { }
		public Order(BtcePair pair, TradeType type, decimal amount, decimal rate, UInt32 timestamp, int status)
		{
			Pair = pair;
			Amount = amount;
			Rate = rate;
			TimestampCreated = timestamp;
			Status = status;
		}

		public static Order ReadFromJObject(JObject o)
		{
			if (o == null)
				return null;
			return new Order()
			{
				Pair = BtcePairHelper.FromString(o.Value<string>("pair")),
				Type = TradeTypeHelper.FromString(o.Value<string>("type")),
				Amount = o.Value<decimal>("amount"),
				Rate = o.Value<decimal>("rate"),
				TimestampCreated = o.Value<UInt32>("timestamp_created"),
				Status = o.Value<int>("status")
			};
		}

		public override string ToString()
		{
			return string.Format("Ts:{0} {1} {2} {3}@{4} Status:{5}", TimestampCreated, Pair, Type, Amount, Rate, Status);
		}
	}
}
