using BtcE.Utils;
using Newtonsoft.Json.Linq;
using System;
namespace BtcE
{
	public class TradeInfo
	{
		public decimal Amount { get; private set; }
		public DateTime Date { get; private set; }
		public BtceCurrency Item { get; private set; }
		public decimal Price { get; private set; }
		public BtceCurrency PriceCurrency { get; private set; }
		public UInt32 Tid { get; private set; }
		public TradeInfoType Type { get; private set; }

		public static TradeInfo ReadFromJObject(JObject o)
        {
            if (o == null)
                return null;

			return new TradeInfo()
            {
                Amount = o.Value<decimal>("amount"),
                Price = o.Value<decimal>("price"),
                Date = UnixTime.ConvertToDateTime(o.Value<UInt32>("date")),
                Item = BtceCurrencyHelper.FromString(o.Value<string>("item")),
                PriceCurrency = BtceCurrencyHelper.FromString(o.Value<string>("price_currency")),
                Tid = o.Value<UInt32>("tid"),
                Type = TradeInfoTypeHelper.FromString(o.Value<string>("trade_type"))
            };
        }
	}

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
