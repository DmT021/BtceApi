using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BtcE;

namespace ApiTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var ticker = BtceApi.GetTicker(BtcePair.BtcUsd);
            var trades = BtceApi.GetTrades(BtcePair.BtcUsd);
            var btcusdDepth = BtceApi.GetDepth(BtcePair.UsdRur);

            var btceApi = new BtceApi("YOUR-API-KEY", "your_secret_key");
            var info = btceApi.GetInfo();
            var transHistory = btceApi.GetTransHistory();
            var tradeHistory = btceApi.GetTradeHistory(count: 20);
            var orderList = btceApi.GetOrderList();
            var tradeAnswer = btceApi.Trade(BtcePair.BtcUsd, TradeType.Sell, 20, 0.1m);
            var cancelAnswer = btceApi.CancelOrder(tradeAnswer.OrderId);
        }
    }
}
