using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace BtcE {
	public class Transaction {
		public int Type { get; private set; }
		public decimal Amount { get; private set; }
		public BtceCurrency Currency { get; private set; }
		public string Description { get; private set; }
		public int Status { get; private set; }
		public uint Timestamp { get; private set; }
		public static Transaction ReadFromJObject( JObject o ) {
			return o == null ? null : new Transaction() {
				Type = o.Value<int>( "type" ),
				Amount = o.Value<decimal>( "amount" ),
				Currency = BtceCurrencyHelper.FromString( o.Value<string>( "currency" ) ),
				Timestamp = o.Value<uint>( "timestamp" ),
				Status = o.Value<int>( "status" ),
				Description = o.Value<string>( "desc" )
			};
		}
	}
	public class TransHistory {
		public Dictionary<int, Transaction> List { get; private set; }
		public static TransHistory ReadFromJObject( JObject o ) {
			return new TransHistory() { List = ( ( IDictionary<string, JToken> ) o ).ToDictionary( a => int.Parse( a.Key ), a => Transaction.ReadFromJObject( a.Value as JObject ) ) };
		}
	}
}
