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
			var ticker = BtceApi.GetTicker(BtcePair.btc_usd);
			var trades = BtceApi.GetTrades(BtcePair.btc_usd);
			var btcusdDepth = BtceApi.GetDepth(BtcePair.usd_rur);
			var fee = BtceApi.GetFee(BtcePair.usd_rur);

			var btceApi = new BtceApi("API_KEY", "API_SECRET");
			var info = btceApi.GetInfo();
			var transHistory = btceApi.GetTransHistory();
			var tradeHistory = btceApi.GetTradeHistory(count: 20);
			var orderList = btceApi.GetOrderList();
			var tradeAnswer = btceApi.Trade(BtcePair.btc_usd, TradeType.Sell, 20, 0.1m);
			var cancelAnswer = btceApi.CancelOrder(tradeAnswer.OrderId);
		}
	}
}
