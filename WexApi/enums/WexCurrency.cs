//
// https://github.com/multiprogramm/WexAPI
//

using System;

namespace Wex
{
	public enum WexCurrency
	{
		Unknown = 0,

		// Crypto:
		CRYPTO_BEGIN, // bound only
		btc,
		ltc,
		nmc,
		nvc,
		ppc,
		dsh,
		eth,
		bch,
		zec,
		CRYPTO_END, // bound only

		// Fiat:
		FIAT_BEGIN, // bound only
		usd,
		rur,
		eur,
		FIAT_END,

		// Tokens:
		TOKENS_BEGIN, // bound only
		usdet,
		ruret,
		euret,
		btcet,
		ltcet,
		ethet,
		nmcet,
		nvcet,
		ppcet,
		dshet,
		bchet,
		TOKENS_END // bound only
	}

	public enum CurrencyType
	{
		Unknown = 0,

		Fiat,
		Crypto,
		Token
	}

	public class WexCurrencyHelper
	{
		public static WexCurrency FromString(string s)
		{
			WexCurrency ret = WexCurrency.Unknown;
			Enum.TryParse<WexCurrency>(s.ToLowerInvariant(), out ret);
			return ret;
		}

		public static string ToString(WexCurrency v)
		{
			return Enum.GetName(typeof(WexCurrency), v);
		}

		public static CurrencyType GetCurrencyType(WexCurrency v)
		{
			int key = (int)v;
			if (key > (int)WexCurrency.CRYPTO_BEGIN && key < (int)WexCurrency.CRYPTO_END)
				return CurrencyType.Crypto;
			else if (key > (int)WexCurrency.FIAT_BEGIN && key < (int)WexCurrency.FIAT_END)
				return CurrencyType.Fiat;
			else if (key > (int)WexCurrency.TOKENS_BEGIN && key < (int)WexCurrency.TOKENS_END)
				return CurrencyType.Token;

			return CurrencyType.Unknown;
		}
	}
}
