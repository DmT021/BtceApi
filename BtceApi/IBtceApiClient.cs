using System;
using BtcE.Data;

namespace BtcE
{
    public interface IBtceApiClient : IBtceApiPublicClient, IDisposable
    {
        CancelOrderAnswer CancelOrder(int orderId);

        OrderList GetActiveOrders(BtcePair? pair = null);

        UserInfo GetInfo();

        TradeHistory GetTradeHistory(
            int? from = null,
            int? count = null,
            int? fromId = null,
            int? endId = null,
            bool? orderAsc = null,
            DateTime? since = null,
            DateTime? end = null,
            BtcePair? pair = null);

        TransHistory GetTransHistory(
            int? from = null,
            int? count = null,
            int? fromId = null,
            int? endId = null,
            bool? orderAsc = null,
            DateTime? since = null,
            DateTime? end = null);

        TradeAnswer Trade(BtcePair pair, TradeType type, decimal rate, decimal amount);
    }
}