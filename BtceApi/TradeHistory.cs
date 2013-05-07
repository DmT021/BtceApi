using System;
using System.Collections.Generic;
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

            var r = new Trade
                {
                    Pair = BtcePairHelper.FromString(o.Value<string>("pair")),
                    Type = TradeTypeHelper.FromString(o.Value<string>("type")),
                    Amount = o.Value<decimal>("amount"),
                    Rate = o.Value<decimal>("rate"),
                    Timestamp = o.Value<UInt32>("timestamp"),
                    IsYourOrder = o.Value<int>("is_your_order") == 1,
                    OrderId = o.Value<int>("order_id")
                };

            return r;
        }
    }

    public class TradeHistory
    {
        public Dictionary<int, Trade> List { get; private set; }

        public static TradeHistory ReadFromJObject(JObject o)
        {
            var r = new TradeHistory();
            r.List = new Dictionary<int, Trade>();
            foreach (var item in o)
            {
                int tradeId = int.Parse(item.Key);
                Trade trade = Trade.ReadFromJObject(item.Value as JObject);
                r.List.Add(tradeId, trade);
            }

            return r;
        }
    }
}