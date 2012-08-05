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
        BtcUsd ,
        LtcBtc ,
        LtcUsd ,
        NmcBtc ,
        Unknown
    }

    public class BtcePairHelper
    {
        public static BtcePair FromString(string s)
        {
            switch (s)
            {
                case "btc_usd":
                    return BtcePair.BtcUsd;
                case "ltc_btc":
                    return BtcePair.LtcBtc;
                case "ltc_usd":
                    return BtcePair.LtcUsd;
                case "nmc_btc":
                    return BtcePair.NmcBtc;
                default:
                    return BtcePair.Unknown;
            }
        }

        public static string ToString(BtcePair v)
        {
            if (v == BtcePair.BtcUsd)
                return "btc_usd";
            if (v == BtcePair.LtcBtc)
                return "ltc_btc";
            if (v == BtcePair.LtcUsd)
                return "ltc_usd";
            if (v == BtcePair.NmcBtc)
                return "nmc_btc";

            throw new NotSupportedException();
        }
    }
}
