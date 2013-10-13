using Newtonsoft.Json.Linq;
namespace BtcE {
	public class Ticker {
		public decimal Average { get; private set; }
		public decimal Buy { get; private set; }
		public decimal High { get; private set; }
		public decimal Last { get; private set; }
		public decimal Low { get; private set; }
		public decimal Sell { get; private set; }
		public decimal Volume { get; private set; }
		public decimal VolumeCurrent { get; private set; }
		public uint ServerTime { get; private set; }
		public static Ticker ReadFromJObject( JObject o ) {
			return o == null?null:new Ticker() {
				Average = o.Value<decimal>( "avg" ),
				Buy = o.Value<decimal>( "buy" ),
				High = o.Value<decimal>( "high" ),
				Last = o.Value<decimal>( "last" ),
				Low = o.Value<decimal>( "low" ),
				Sell = o.Value<decimal>( "sell" ),
				Volume = o.Value<decimal>( "vol" ),
				VolumeCurrent = o.Value<decimal>( "vol_cur" ),
				ServerTime = o.Value<uint>( "server_time" ),
			};
		}
	}
}
