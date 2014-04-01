﻿using System.Collections.Generic;
using System.Linq;

namespace BtcE
{
    public class BtceApiPublicClientV3 : IBtceApiPublicClient
    {
        public IDictionary<BtcePair, Depth> GetDepth(IEnumerable<BtcePair> pairlist, int limit = 150)
        {
            return BtceApiV3.GetDepth(pairlist.ToArray(), limit);
        }

        public IDictionary<BtcePair, Ticker> GetTicker(IEnumerable<BtcePair> pairlist)
        {
            return BtceApiV3.GetTicker(pairlist.ToArray());
        }

        public Ticker GetTicker(BtcePair pair)
        {
            return GetTicker(new[] {pair}).FirstOrDefault().Value;
        }

        public Depth GetDepth(BtcePair pair)
        {
            return GetDepth(new[] {pair}).FirstOrDefault().Value;
        }

        public IDictionary<BtcePair, IEnumerable<TradeInfo>> GetTrades(IEnumerable<BtcePair> pairlist, int limit = 150)
        {
            return BtceApiV3.GetTrades(pairlist.ToArray(), limit).ToDictionary(k => k.Key, v => (IEnumerable<TradeInfo>)v.Value);
        }

        public decimal GetFee(BtcePair pair)
        {
            return BtceApi.GetFee(pair);
        }

        public IEnumerable<TradeInfo> GetTrades(BtcePair pair)
        {
            return BtceApiV3.GetTrades(new BtcePair[] {pair}).FirstOrDefault().Value;
        }
    }
}