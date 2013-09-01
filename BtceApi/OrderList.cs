using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Linq;
namespace BtcE {
	public class Order {
		public BtcePair Pair { get; private set; }
		public TradeType Type { get; private set; }
		public decimal Amount { get; private set; }
		public decimal Rate { get; private set; }
		public uint TimestampCreated { get; private set; }
		public int Status { get; private set; }
		public static Order ReadFromJObject( JObject o ) {
			return o == null?null:new Order() {
				Pair = BtcePairHelper.FromString( o.Value<string>( "pair" ) ),
				Type = TradeTypeHelper.FromString( o.Value<string>( "type" ) ),
				Amount = o.Value<decimal>( "amount" ),
				Rate = o.Value<decimal>( "rate" ),
				TimestampCreated = o.Value<uint>( "timestamp_created" ),
				Status = o.Value<int>( "status" )
			};
		}
	}

	public class OrderList {
		public Dictionary<int, Order> List { get; private set; }
		public static OrderList ReadFromJObject( JObject o ) {
			return new OrderList() {
				List = ( ( IDictionary<string, JToken> ) o ).ToDictionary( item => int.Parse( item.Key ), item => Order.ReadFromJObject( item.Value as JObject ) )
			};
		}
	}
}
