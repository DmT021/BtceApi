using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BtcE;

namespace ApiTest
{
	class Program
	{
		static void Main(string[] args) {
			var ticker = BtceApi.GetTicker(BtcePair.Btc_Usd);
			var trades = BtceApi.GetTrades(BtcePair.Btc_Usd);
			var btcusdDepth = BtceApi.GetDepth(BtcePair.Usd_Rur);
			var fee = BtceApi.GetFee(BtcePair.Usd_Rur);

			var btceApi = new BtceApi("API_KEY", "API_SECRET");
			var info = btceApi.GetInfo();
			var transHistory = btceApi.GetTransHistory();
			var tradeHistory = btceApi.GetTradeHistory(count: 20);
			var orderList = btceApi.GetOrderList();
			var tradeAnswer = btceApi.Trade(BtcePair.Btc_Usd, TradeType.Sell, 20, 0.1m);
			var cancelAnswer = btceApi.CancelOrder(tradeAnswer.OrderId);
		}
	}
}
