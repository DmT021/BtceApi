namespace BtcE
{
  using System;

  public static class BtceCurrencyHelper
  {
    public static Tuple<string, string> FromBtcePair(BtcePair pair)
    {
      var currencyStrs = BtcePairHelper.ToString(pair).Split('_');
      return Tuple.Create(currencyStrs[0].ToUpperInvariant(), currencyStrs[1].ToUpperInvariant());
    }

    public static Tuple<BtceCurrency, BtceCurrency> FromBtcePair2(BtcePair pair)
    {
      var currencyStrs = BtceCurrencyHelper.FromBtcePair(pair);
      return Tuple.Create(BtceCurrencyHelper.FromString(currencyStrs.Item1), BtceCurrencyHelper.FromString(currencyStrs.Item2));
    }

    public static BtceCurrency FromString(string s)
    {
      BtceCurrency ret = BtceCurrency.Unknown;
      Enum.TryParse<BtceCurrency>(s, out ret);
      return ret;
    }

    public static string ToString(BtceCurrency v)
    {
      return Enum.GetName(typeof(BtceCurrency), v);
    }
  }
}