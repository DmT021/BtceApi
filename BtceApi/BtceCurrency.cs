﻿namespace BtcE {
	public enum BtceCurrency { btc, eur, ftc, ltc, nmc, nvc, ppc, rur, trc, usd, Unknown }
	class BtceCurrencyHelper {
		public static BtceCurrency FromString(string s) {
			BtceCurrency ret = BtceCurrency.Unknown;
			System.Enum.TryParse<BtceCurrency>(s, out ret);
			return ret;
		}
		public static string ToString(BtceCurrency v) { return System.Enum.GetName(typeof(BtceCurrency), v);}
	}
}
