namespace BtcE {
	public static class BtcePairHelper {
		public static BtcePair FromString( string s ) {
			BtcePair ret = BtcePair.Unknown;
			System.Enum.TryParse<BtcePair>( s.ToLowerInvariant(), out ret );
			return ret;
		}
		public static string ToString( BtcePair v ) { return System.Enum.GetName( typeof( BtcePair ), v ).ToLowerInvariant(); }
	}
}