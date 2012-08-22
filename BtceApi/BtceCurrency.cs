using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        Unknown
    }

    class BtceCurrencyHelper
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

            throw new NotSupportedException();
        }
    }
}
