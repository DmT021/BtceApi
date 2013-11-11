using System;
using System.Diagnostics.Contracts;
using System.Linq;

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
			Contract.Requires(btcPair != null);

			var pairPrecision = precisions.SingleOrDefault(x => x.Item1 == btcPair);
			if (pairPrecision == default(Tuple<BtcePair, int, int>))
			{
				return Tuple.Create(4, 8); // ex. 12.3456 USD for 0.12345678 BTC: 
			}
			else
			{
				return Tuple.Create(pairPrecision.Item2, pairPrecision.Item3);
			}
		}
	}
}