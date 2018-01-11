//
// https://github.com/multiprogramm/WexAPI
//

using System;

namespace Wex
{
	public enum WexPair
	{
		Unknown = 0,

		btc_usd,
		btc_rur,
		btc_eur,
		ltc_btc,
		ltc_usd,
		ltc_rur,
		ltc_eur,
		nmc_btc,
		nmc_usd,
		nvc_btc,
		nvc_usd,
		usd_rur,
		eur_usd,
		eur_rur,
		ppc_btc,
		ppc_usd,
		dsh_btc,
		dsh_usd,
		dsh_rur,
		dsh_eur,
		dsh_ltc,
		dsh_eth,
		eth_btc,
		eth_usd,
		eth_eur,
		eth_ltc,
		eth_rur,
		bch_usd,
		bch_btc,
		bch_rur,
		bch_eur,
		bch_ltc,
		bch_eth,
		bch_dsh,
		zec_btc,
		zec_usd,
		bch_zec,
		dsh_zec,
		eth_zec,
		zec_ltc,

		usdet_usd,
		ruret_rur,
		euret_eur,
		btcet_btc,
		ltcet_ltc,
		ethet_eth,
		nmcet_nmc,
		nvcet_nvc,
		ppcet_ppc,
		dshet_dsh,
		bchet_bch
	}

	public class PairCurrencies
	{
		public WexCurrency BaseCurrency { get; set; }
		public WexCurrency QuoteCurrency  { get; set; }
	}

	public class WexPairHelper
	{
		public static WexPair FromString(string s)
		{
			WexPair ret = WexPair.Unknown;
			Enum.TryParse<WexPair>(s.ToLowerInvariant(), out ret);
			return ret;
		}

		public static string ToString(WexPair v)
		{
			return Enum.GetName(typeof(WexPair), v).ToLowerInvariant();
		}

		public static PairCurrencies GetPairCurrencies(WexPair p)
		{
			if (p == WexPair.Unknown)
				return null;
			string str_p = ToString(p);
			char[] SLPIT_CHARS = new char[] { '_' };
			string[] arr = str_p.Split(SLPIT_CHARS);

			return new PairCurrencies
			{
				BaseCurrency = WexCurrencyHelper.FromString(arr[0]),
				QuoteCurrency = WexCurrencyHelper.FromString(arr[1])
			};
		}
	}
}
