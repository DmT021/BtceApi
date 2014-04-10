using System.Collections.Generic;
using System.Linq;
using System;
using BtcE.Extensions;

namespace BtcE
{
  public class BtceApiPublicClientV3 : IBtceApiPublicClient
  {
    private ApiInfo apiInfoCache;

    public decimal GetFee(BtcePair pair, bool refreshCache = false)
    {
      var currencyPairInfo = GetPairInfo(pair, refreshCache);

      return currencyPairInfo.Fee;
    }

    public decimal RoundAmount(BtcePair pair, decimal amount, bool refreshCache = false)
    {
      var currencyPairInfo = GetPairInfo(pair, refreshCache);
      var amountFixed = Math.Max(amount, currencyPairInfo.MinAmount);
      amountFixed = Math.Round(amountFixed, currencyPairInfo.MinAmount.GetPrecision());

      return amountFixed;
    }

    public decimal RoundPrice(BtcePair pair, decimal price, bool refreshCache = false)
    {
      var currencyPairInfo = GetPairInfo(pair, refreshCache);
      var priceFixed = Math.Min(Math.Max(price, currencyPairInfo.MinPrice), currencyPairInfo.MaxPrice);
      priceFixed = Math.Round(priceFixed, currencyPairInfo.MinPrice.GetPrecision());

      return priceFixed;
    }

    private BtcePairInfo GetPairInfo(BtcePair pair, bool refreshCache)
    {
      if (apiInfoCache == null || refreshCache)
      {
        apiInfoCache = GetApiInfo();
      }
      var currencyPairInfo = apiInfoCache.Pairs[pair];

      return currencyPairInfo;
    }

    #region IBtceApiPublicClient implicit interface implementation

    public ApiInfo GetApiInfo()
    {
      return BtceApiV3.GetApiInfo();
    }

    public IDictionary<BtcePair, Depth> GetDepth(IEnumerable<BtcePair> pairlist, int limit = 150)
    {
      return BtceApiV3.GetDepth(pairlist.ToArray(), limit);
    }

    public Depth GetDepth(BtcePair pair)
    {
      return GetDepth(new[] { pair }).FirstOrDefault().Value;
    }

    public decimal GetFee(BtcePair pair)
    {
      return GetFee(pair, false);
    }

    public IDictionary<BtcePair, Ticker> GetTicker(IEnumerable<BtcePair> pairlist)
    {
      return BtceApiV3.GetTicker(pairlist.ToArray());
    }

    public Ticker GetTicker(BtcePair pair)
    {
      return GetTicker(new[] { pair }).FirstOrDefault().Value;
    }

    public IDictionary<BtcePair, TradeInfo[]> GetTrades(IEnumerable<BtcePair> pairlist, int limit = 150)
    {
      return BtceApiV3.GetTrades(pairlist.ToArray(), limit).ToDictionary(k => k.Key, v => v.Value);
    }

    public TradeInfo[] GetTrades(BtcePair pair)
    {
      return BtceApiV3.GetTrades(new BtcePair[] { pair }).FirstOrDefault().Value;
    }

    #endregion IBtceApiPublicClient implicit interface implementation
  }
}
