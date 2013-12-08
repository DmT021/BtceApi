using System;
using BtcE;

namespace ApiTest
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			var pairDepths3 = BtceApiV3.GetDepth(new BtcePair[] { BtcePair.btc_usd });
			foreach (var depth3 in pairDepths3)
			{
				Console.WriteLine("{0}: {1}", depth3.Key, depth3.Value);
			}
			var pairTickers3 = BtceApiV3.GetTicker(new BtcePair[] { BtcePair.btc_usd });
			foreach (var ticker3 in pairTickers3)
			{
				Console.WriteLine("{0}: {1}", ticker3.Key, ticker3.Value);
			}
			var pairTrades3 = BtceApiV3.GetTrades(new BtcePair[] { BtcePair.btc_usd });
			foreach (var pairTrade3 in pairTrades3)
			{
				foreach (var trade3 in pairTrade3.Value)
				{
					Console.WriteLine(trade3);
				}
			}

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