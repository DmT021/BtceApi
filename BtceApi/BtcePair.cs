using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BtcE
{
    /*public class BtcePair
    {
        public static BtcePair BtcUsd = new BtcePair();
        public static BtcePair LtcBtc = new BtcePair();
        public static BtcePair LtcUsd = new BtcePair();
        public static BtcePair NmcBtc = new BtcePair();
        public static BtcePair Unknown = new BtcePair();

        public static BtcePair Parse(string s)
        {
            switch (s)
            {
                case "btc_usd":
                    return BtcUsd;
                case "ltc_btc":
                    return LtcBtc;
                case "ltc_usd":
                    return LtcUsd;
                case "nmc_btc":
                    return NmcBtc;
                default:
                    return Unknown;
            }
        }

        private BtcePair() { }

        public override string ToString()
        {
            if (this == BtcUsd)
                return "btc_usd";
            if (this == LtcBtc)
                return "ltc_btc";
            if (this == LtcUsd)
                return "ltc_usd";
            if (this == NmcBtc)
                return "nmc_btc";

            throw new NotSupportedException();
        }
    }*/

    public enum BtcePair
    {
		Btc_Usd,
		Btc_Rur,
		Btc_Eur,
		Ltc_Btc,
		Ltc_Usd,
		Ltc_Rur,
		Nmc_Btc,
		Nvc_Btc,
		Usd_Rur,
		Eur_Usd,
		Trc_Btc,
		Ppc_Btc,
		Ftc_Btc,
		Cnc_Btc,
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
    }
}
