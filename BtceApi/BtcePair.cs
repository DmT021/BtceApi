using System;
using System.Linq;

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
		public static BtcePair FromString(string s)
		{
			BtcePair ret = BtcePair.Unknown;
			Enum.TryParse<BtcePair>(s.ToLowerInvariant(), out ret);
			return ret;
		}
		public static string ToString(BtcePair v)
		{
			return Enum.GetName(typeof(BtcePair), v).ToLowerInvariant();
		}

		private static Tuple<BtcePair, int, int>[] precisions = new Tuple<BtcePair, int, int>[] 
		{ 
			Tuple.Create(BtcePair.btc_usd, 2, 3),
			Tuple.Create(BtcePair.ltc_btc, 1, 5),
			Tuple.Create(BtcePair.ltc_usd, 1, 6),
			Tuple.Create(BtcePair.nmc_btc, 1, 5),
			Tuple.Create(BtcePair.ppc_btc, 1, 5),
			Tuple.Create(BtcePair.trc_btc, 1, 5),
		};

		/// <summary>
		/// Returns decimal precision for Amount and Price
		/// </summary>
		/// <param name="btcPair">Btc-E Pair</param>
		/// <returns>Tuple of int,int where Item1 is Amount's precision and Item2 is Price's precision</returns>
		public static Tuple<int, int> GetPrecision(BtcePair btcPair)
		{
			var pairPrecision = precisions.SingleOrDefault(x => x.Item1 == btcPair);
			if (pairPrecision == default(Tuple<BtcePair, int, int>))
			{
				return Tuple.Create(1, 3);
			}
			else
			{
				return Tuple.Create(pairPrecision.Item2, pairPrecision.Item3);
			}
		}
	}
}
