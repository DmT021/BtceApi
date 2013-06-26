using System;
namespace BtcE
{
	public enum BtceCurrency
	{
		btc,
		ltc,
		nmc,
		nvc,
		trc,
		ppc,
		ftc,
		usd,
		rur,
		eur,
		Unknown
	}
	class BtceCurrencyHelper
	{
		public static BtceCurrency FromString(string s) {
			BtceCurrency ret = BtceCurrency.Unknown;
			Enum.TryParse<BtceCurrency>(s, out ret);
			return ret;
		}
		public static string ToString(BtceCurrency v) {
			return Enum.GetName(typeof(BtceCurrency), v);
		}
	}
}
