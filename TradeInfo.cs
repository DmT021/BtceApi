using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
using BtcE.Utils;

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

            var r = new TradeInfo()
            {
                Amount = o.Value<decimal>("amount"),
                Price = o.Value<decimal>("price"),
                Date = UnixTime.ConvertToDateTime(o.Value<UInt32>("date")),
                Item = BtceCurrencyHelper.FromString(o.Value<string>("item")),
                PriceCurrency = BtceCurrencyHelper.FromString(o.Value<string>("price_currency")),
                Tid = o.Value<UInt32>("tid"),
                Type = TradeInfoTypeHelper.FromString(o.Value<string>("trade_type"))
            };

            return r;
        }
    }
}
