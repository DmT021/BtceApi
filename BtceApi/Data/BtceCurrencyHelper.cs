using System;

namespace BtcE.Data
{
    public static class BtceCurrencyHelper
    {
        public static Tuple<string, string> FromBtcePair(BtcePair pair)
        {
            string[] currencyStrs = BtcePairHelper.ToString(pair).Split('_');
            return Tuple.Create(currencyStrs[0].ToUpperInvariant(), currencyStrs[1].ToUpperInvariant());
        }

        public static Tuple<BtceCurrency, BtceCurrency> FromBtcePair2(BtcePair pair)
        {
            Tuple<string, string> currencyStrs = FromBtcePair(pair);
            return Tuple.Create(FromString(currencyStrs.Item1), FromString(currencyStrs.Item2));
        }

        public static BtceCurrency FromString(string s)
        {
            var ret = BtceCurrency.Unknown;
            Enum.TryParse(s, out ret);
            return ret;
        }

        public static string ToString(BtceCurrency v)
        {
            return Enum.GetName(typeof (BtceCurrency), v);
        }
    }
}