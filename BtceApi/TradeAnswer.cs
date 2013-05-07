using Newtonsoft.Json.Linq;

namespace BtcE
{
    public class TradeAnswer
    {
        private TradeAnswer()
        {
        }

        public decimal Received { get; private set; }
        public decimal Remains { get; private set; }
        public int OrderId { get; private set; }
        public Funds Funds { get; private set; }

        public static TradeAnswer ReadFromJObject(JObject o)
        {
            if (o == null)
                return null;

            var r = new TradeAnswer
                {
                    Funds = Funds.ReadFromJObject(o["funds"] as JObject),
                    Received = o.Value<decimal>("received"),
                    Remains = o.Value<decimal>("remains"),
                    OrderId = o.Value<int>("order_id")
                };

            return r;
        }
    }
}