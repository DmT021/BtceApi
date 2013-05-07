using System;

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
        BtcUsd,
        LtcBtc,
        LtcUsd,
        NmcBtc,
        BtcRur,
        UsdRur,
        EurUsd,
        NvcBtc,
        TrcBtc,
        PpcBtc,
        FtcBtc,
        CncBtc,
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
                case "btc_rur":
                    return BtcePair.BtcRur;
                case "usd_rur":
                    return BtcePair.UsdRur;
                case "eur_usd":
                    return BtcePair.EurUsd;
                case "nvc_btc":
                    return BtcePair.NvcBtc;
                case "trc_btc":
                    return BtcePair.TrcBtc;
                case "ppc_btc":
                    return BtcePair.PpcBtc;
                case "ftc_btc":
                    return BtcePair.FtcBtc;
                case "cnc_btc":
                    return BtcePair.CncBtc;
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
            if (v == BtcePair.BtcRur)
                return "btc_rur";
            if (v == BtcePair.UsdRur)
                return "usd_rur";
            if (v == BtcePair.EurUsd)
                return "eur_usd";
            if (v == BtcePair.NvcBtc)
                return "nvc_btc";
            if (v == BtcePair.TrcBtc)
                return "trc_btc";
            if (v == BtcePair.PpcBtc)
                return "ppc_btc";
            if (v == BtcePair.FtcBtc)
                return "ftc_btc";
            if (v == BtcePair.CncBtc)
                return "cnc_btc";

            throw new NotSupportedException();
        }
    }
}