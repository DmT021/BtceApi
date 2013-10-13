using System;
namespace BtcE {
	public enum TradeType {
		Sell,
		Buy
	}
	public class TradeTypeHelper {
		public static TradeType FromString( string s ) { return ( TradeType ) Enum.Parse( typeof( TradeType ), s, true ); }
		public static string ToString( TradeType v ) { return Enum.GetName( typeof( TradeType ), v ).ToLowerInvariant(); }
	}
}
