using System;

namespace BtcE
{
	public enum BtcePair
	{
		btc_usd,
		btc_rur,
		btc_eur,
		ltc_btc,
		ltc_usd,
		ltc_rur,
		nmc_btc,
		nvc_btc,
		usd_rur,
		eur_usd,
		trc_btc,
		ppc_btc,
		ftc_btc,
        Unknown
	}

	public class BtcePairHelper
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
