using Newtonsoft.Json.Linq;

namespace BtcE.Data
{
    public class Trade
    {
        public Trade()
        {
        }

        public Trade(BtcePair pair, TradeType type, decimal amount, decimal rate, int orderId, bool isYourOrder,
                     uint timestamp)
            : this()
        {
            Pair = pair;
            Type = type;
            Amount = amount;
            Rate = rate;
            OrderId = orderId;
            IsYourOrder = isYourOrder;
            Timestamp = timestamp;
        }

        public decimal Amount { get; private set; }
        public bool IsYourOrder { get; private set; }
        public int OrderId { get; private set; }
        public BtcePair Pair { get; private set; }
        public decimal Rate { get; private set; }
        public uint Timestamp { get; private set; }
        public TradeType Type { get; private set; }

        public static Trade ReadFromJObject(JObject o)
        {
            if (o == null)
                return null;
            return new Trade
                {
                    Pair = BtcePairHelper.FromString(o.Value<string>("pair")),
                    Type = TradeTypeHelper.FromString(o.Value<string>("type")),
                    Amount = o.Value<decimal>("amount"),
                    Rate = o.Value<decimal>("rate"),
                    Timestamp = o.Value<uint>("timestamp"),
                    IsYourOrder = o.Value<int>("is_your_order") == 1,
                    OrderId = o.Value<int>("order_id")
                };
        }

        public override string ToString()
        {
            return string.Format(
                "{0} {1} [{2}] {3}x{4} {{{5}}} {6}",
                Pair,
                Timestamp,
                Type,
                Rate,
                Amount,
                OrderId,
                IsYourOrder);
        }
    }
}