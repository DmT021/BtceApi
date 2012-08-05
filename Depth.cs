using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

            var r = new OrderInfo()
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
            var r = new Depth();
            r.Asks = new List<OrderInfo>();
            r.Bids = new List<OrderInfo>();

            foreach (var item in o["asks"] as JArray)
            {
                var order = OrderInfo.ReadFromJObject(item as JArray);
                r.Asks.Add(order);
            }
            foreach (var item in o["bids"] as JArray)
            {
                var order = OrderInfo.ReadFromJObject(item as JArray);
                r.Bids.Add(order);
            }

            return r;
        }
    }
}
