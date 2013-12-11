using Newtonsoft.Json.Linq;

namespace BtcE
{
    public class TradeAnswer
    {
        private TradeAnswer()
        {
        }

        public Funds Funds { get; private set; }
        public int OrderId { get; private set; }
        public decimal Received { get; private set; }
        public decimal Remains { get; private set; }

        public static TradeAnswer ReadFromJObject(JObject o)
        {
            if (o == null)
                return null;
            return new TradeAnswer
                {
                    Funds = Funds.ReadFromJObject(o["funds"] as JObject),
                    Received = o.Value<decimal>("received"),
                    Remains = o.Value<decimal>("remains"),
                    OrderId = o.Value<int>("order_id")
                };
        }

        public override string ToString()
        {
            return string.Format("recv:{0} rem:{1} oid:{2} funds:{{{3}}}", Received, Remains, OrderId, Funds);
        }
    }
}