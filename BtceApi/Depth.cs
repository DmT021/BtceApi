using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace BtcE
{
    public class OrderInfo
    {
        public decimal Price { get; private set; }
        public decimal Amount { get; private set; }

        public static OrderInfo ReadFromJObject(JArray o)
        {
            if (o == null)
                return null;

            var r = new OrderInfo
                {
                    Price = o.Value<decimal>(0),
                    Amount = o.Value<decimal>(1),
                };

            return r;
        }
    }

    public class Depth
    {
        public List<OrderInfo> Asks { get; private set; }
        public List<OrderInfo> Bids { get; private set; }

        public static Depth ReadFromJObject(JObject o)
        {
            var r = new Depth
                {
                    Asks = new List<OrderInfo>(), 
                    Bids = new List<OrderInfo>()
                };

            foreach (JToken item in o["asks"] as JArray)
            {
                OrderInfo order = OrderInfo.ReadFromJObject(item as JArray);
                r.Asks.Add(order);
            }
            foreach (JToken item in o["bids"] as JArray)
            {
                OrderInfo order = OrderInfo.ReadFromJObject(item as JArray);
                r.Bids.Add(order);
            }

            return r;
        }
    }
}