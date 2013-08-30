using System;

namespace BtcE
{
	public static class BtcePairHelper
	{
		public static BtcePair FromString(string s) {
			BtcePair ret = BtcePair.Unknown;
			Enum.TryParse<BtcePair>(s.ToLowerInvariant(), out ret);
			return ret;
		}
		public static string ToString(BtcePair v) {
			return Enum.GetName(typeof(BtcePair), v).ToLowerInvariant();
		}
	}
}