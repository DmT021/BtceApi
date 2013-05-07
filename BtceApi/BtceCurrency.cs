using System;

namespace BtcE
{
    /*public class BtceCurrency
    {
        public static BtceCurrency Btc = new BtceCurrency();
        public static BtceCurrency Usd = new BtceCurrency();
        public static BtceCurrency Ltc = new BtceCurrency();
        public static BtceCurrency Nmc = new BtceCurrency();
        public static BtceCurrency Unknown = new BtceCurrency();

        public static BtceCurrency Parse(string s)
        {
            switch (s)
            {
                case "BTC":
                    return Btc;
                case "USD":
                    return Usd;
                case "LTC":
                    return Ltc;
                case "NMC":
                    return Nmc;
                default:
                    return Unknown;
            }
        }

        private BtceCurrency() { }

        public override string ToString()
        {
            if (this == Btc)
                return "BTC";
            if (this == Usd)
                return "USD";
            if (this == Ltc)
                return "LTC";
            if (this == Nmc)
                return "NMC";

            throw new NotSupportedException();
        }
    }*/

    public enum BtceCurrency
    {
        Btc,
        Usd,
        Ltc,
        Nmc,
        Rur,
        Eur,
        Nvc,
        Trc,
        Ppc,
        Ftc,
        Cnc,
        Unknown
    }

    internal class BtceCurrencyHelper
    {
        public static BtceCurrency FromString(string s)
        {
            switch (s)
            {
                case "BTC":
                    return BtceCurrency.Btc;
                case "USD":
                    return BtceCurrency.Usd;
                case "LTC":
                    return BtceCurrency.Ltc;
                case "NMC":
                    return BtceCurrency.Nmc;
                case "RUR":
                    return BtceCurrency.Rur;
                case "EUR":
                    return BtceCurrency.Eur;
                case "NVC":
                    return BtceCurrency.Nvc;
                case "TRC":
                    return BtceCurrency.Trc;
                case "PPC":
                    return BtceCurrency.Ppc;
                case "FTC":
                    return BtceCurrency.Ftc;
                case "CNC":
                    return BtceCurrency.Cnc;
                default:
                    return BtceCurrency.Unknown;
            }
        }

        public static string ToString(BtceCurrency v)
        {
            if (v == BtceCurrency.Btc)
                return "BTC";
            if (v == BtceCurrency.Usd)
                return "USD";
            if (v == BtceCurrency.Ltc)
                return "LTC";
            if (v == BtceCurrency.Nmc)
                return "NMC";
            if (v == BtceCurrency.Rur)
                return "RUR";
            if (v == BtceCurrency.Eur)
                return "EUR";
            if (v == BtceCurrency.Nvc)
                return "NVC";
            if (v == BtceCurrency.Trc)
                return "TRC";
            if (v == BtceCurrency.Ppc)
                return "PPC";
            if (v == BtceCurrency.Ftc)
                return "FTC";
            if (v == BtceCurrency.Cnc)
                return "CNC";

            throw new NotSupportedException();
        }
    }
}