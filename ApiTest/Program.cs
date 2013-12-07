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
			var ticker = BtceApi.GetTicker(BtcePair.btc_usd);
			Console.WriteLine(ticker);
			var trades = BtceApi.GetTrades(BtcePair.btc_usd);
			foreach (var trade in trades)
			{
				Console.WriteLine(trade);
			}
			var btcusdDepth = BtceApi.GetDepth(BtcePair.usd_rur);
			Console.WriteLine(btcusdDepth);
			var fee = BtceApi.GetFee(BtcePair.usd_rur);
			Console.WriteLine(fee);

			var btceApi = new BtceApi("API_KEY", "API_SECRET");
			var info = btceApi.GetInfo();
			Console.WriteLine(info);
			var transHistory = btceApi.GetTransHistory();
			var tradeHistory = btceApi.GetTradeHistory(count: 20);
			Console.WriteLine(tradeHistory);
			//var orderList = btceApi.GetOrderList();  ** DEPRICATED ** use GetActiveOrders() instead!
			var orderList = btceApi.GetActiveOrders();
			Console.WriteLine(orderList);
			var tradeAnswer = btceApi.Trade(BtcePair.btc_usd, TradeType.Sell, 20, 0.1m);
			Console.WriteLine(tradeAnswer);
			var cancelAnswer = btceApi.CancelOrder(tradeAnswer.OrderId);
			Console.WriteLine(cancelAnswer);
		}
	}
}
