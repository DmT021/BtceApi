//
// https://github.com/multiprogramm/WexAPI
//

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using Wex.Utils;
namespace Wex
{
	public class Order
	{
		public WexPair Pair { get; private set; }
		public TradeType Type { get; private set; }
		public decimal? StartAmount { get; private set; }
		public decimal Amount { get; private set; }
		public decimal Rate { get; private set; }
		public DateTime TimestampCreated { get; private set; }
		public int Status { get; private set; }

		public static Order ReadFromJObject(JObject o)
		{
			if ( o == null )
				return null;

			return new Order()
			{
				Pair = WexPairHelper.FromString(o.Value<string>("pair")),
				Type = TradeTypeHelper.FromString(o.Value<string>("type")),
				Amount = o.Value<decimal>("amount"),
				StartAmount = o.Value<decimal>("start_amount"),
				Rate = o.Value<decimal>("rate"),
				TimestampCreated = UnixTime.ConvertToDateTime( o.Value<UInt32>("timestamp_created") ),
				Status = o.Value<int>("status"),
			};
		}
	}

	public class OrderList
	{
		public Dictionary<int, Order> List { get; private set; }

		public static OrderList ReadFromJObject(JObject o)
		{
			var sel = o.OfType<JProperty>().Select(
				x => new KeyValuePair<int, Order>(
					int.Parse(x.Name),
					Order.ReadFromJObject(x.Value as JObject)
				)
			);

			return new OrderList() {
				List = sel.ToDictionary(x => x.Key, x => x.Value)
			};
		}
	}
}
