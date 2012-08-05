using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public static Order ReadFromJObject(JObject o)
        {
            if (o == null)
                return null;

            var r = new Order()
            {
                Pair = BtcePairHelper.FromString(o.Value<string>("pair")),
                Type = TradeTypeHelper.FromString(o.Value<string>("type")),
                Amount = o.Value<decimal>("amount"),
                Rate = o.Value<decimal>("rate"),
                TimestampCreated = o.Value<UInt32>("timestamp_created"),
                Status = o.Value<int>("status")
            };

            return r;
        }
    }

    public class OrderList
    {
        public Dictionary<int, Order> List { get; private set; }

        public static OrderList ReadFromJObject(JObject o)
        {
            var r = new OrderList();
            r.List = new Dictionary<int, Order>();
            foreach (var item in o)
            {
                var orderId = int.Parse(item.Key);
                var order = Order.ReadFromJObject(item.Value as JObject);
                r.List.Add(orderId, order);
            }

            return r;
        }
    }
}
