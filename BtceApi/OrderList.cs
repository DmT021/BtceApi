using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
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
		public static Order ReadFromJObject(JObject o) {
			if ( o == null )
				return null;
			return new Order() {
				Pair = BtcePairHelper.FromString(o.Value<string>("pair")),
				Type = TradeTypeHelper.FromString(o.Value<string>("type")),
				Amount = o.Value<decimal>("amount"),
				Rate = o.Value<decimal>("rate"),
				TimestampCreated = o.Value<UInt32>("timestamp_created"),
				Status = o.Value<int>("status")
			};
		}

        public Order() { }
        public Order(BtcePair pair, TradeType type, decimal amount, decimal rate, UInt32 timestamp, int status)
        {
            Pair = pair;
            Amount = amount;
            Rate = rate;
            TimestampCreated = timestamp;
            Status = status;
        }
	}

	public class OrderList
	{
		public Dictionary<int, Order> List { get; private set; }
		public static OrderList ReadFromJObject(JObject o)
        {
            OrderList order_list = new OrderList();
            order_list.List = new Dictionary<int, Order>();

            foreach (KeyValuePair<string, JToken> element in o)
            {
                order_list.List.Add(int.Parse(element.Key), Order.ReadFromJObject(element.Value as JObject));
            }

            return order_list;
		}
	}
}
