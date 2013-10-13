using System;
namespace BtcE {
	public enum TradeInfoType {
		Ask,
		Bid
	}
	public class TradeInfoTypeHelper {
		public static TradeInfoType FromString( string s ) { return ( TradeInfoType ) Enum.Parse( typeof( TradeInfoType ), s, true ); }
	}
}
