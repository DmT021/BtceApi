using System;
using Newtonsoft.Json.Linq;

namespace BtcE
{
	public class Transaction
	{
		public int Type { get; private set; }
		public decimal Amount { get; private set; }
		public BtceCurrency Currency { get; private set; }
		public string Description { get; private set; }
		public int Status { get; private set; }
		public UInt32 Timestamp { get; private set; }

		public static Transaction ReadFromJObject(JObject o)
		{
			if (o == null)
				return null;
			return new Transaction()
			{
				Type = o.Value<int>("type"),
				Amount = o.Value<decimal>("amount"),
				Currency = BtceCurrencyHelper.FromString(o.Value<string>("currency")),
				Timestamp = o.Value<UInt32>("timestamp"),
				Status = o.Value<int>("status"),
				Description = o.Value<string>("desc")
			};
		}
	}
}