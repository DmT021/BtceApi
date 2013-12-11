using System.Collections.Generic;

namespace BtcE
{
    public interface IBtceApiPublicClient
    {
        IDictionary<BtcePair, Depth> GetDepth(IEnumerable<BtcePair> pairlist, int limit = 150);
        IDictionary<BtcePair, Ticker> GetTicker(IEnumerable<BtcePair> pairlist);
        Ticker GetTicker(BtcePair pair);
        Depth GetDepth(BtcePair pair);
        IDictionary<BtcePair, IEnumerable<TradeInfo>> GetTrades(IEnumerable<BtcePair> pairlist, int limit = 150);
        IEnumerable<TradeInfo> GetTrades(BtcePair btcUsd);

        decimal GetFee(BtcePair pair);
    }
}
