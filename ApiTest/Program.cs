using System;
using System.Collections.Generic;
using BtcE;

namespace ApiTest
{
  internal static class Program
  {
    private static void Main(string[] args)
    {
      // Change this.
      const string API_KEY = "API_KEY";
      const string API_SECRET = "API_SECRET";

      IClientFactory factory = new ClientFactory();
      IBtceApiPublicClient publicClient = factory.CreatePublicClient();

      ApiInfo apiInfo = publicClient.GetInfo();
      foreach (var info3 in apiInfo.Pairs)
      {
        Console.WriteLine("{0}: {1}", info3.Key, info3.Value);
      }

      IDictionary<BtcePair, Depth> pairDepths3 = publicClient.GetDepth(new[] { BtcePair.btc_usd });
      foreach (var depth3 in pairDepths3)
      {
        Console.WriteLine("{0}: {1}", depth3.Key, depth3.Value);
      }

      IDictionary<BtcePair, Ticker> pairTickers3 = publicClient.GetTicker(new[] { BtcePair.btc_usd });
      foreach (var ticker3 in pairTickers3)
      {
        Console.WriteLine("{0}: {1}", ticker3.Key, ticker3.Value);
      }

      IDictionary<BtcePair, IEnumerable<TradeInfo>> pairTrades3 = publicClient.GetTrades(new[] { BtcePair.btc_usd });
      foreach (var pairTrade3 in pairTrades3)
      {
        Console.WriteLine("{0}:", pairTrade3.Key);
        foreach (TradeInfo trade3 in pairTrade3.Value)
        {
          Console.WriteLine(trade3);
        }
      }

      Ticker ticker = publicClient.GetTicker(BtcePair.btc_usd);
      Console.WriteLine(ticker);
      IEnumerable<TradeInfo> trades = publicClient.GetTrades(BtcePair.btc_usd);
      Console.WriteLine("{0}:", BtcePair.btc_usd);
      foreach (TradeInfo trade in trades)
      {
        Console.WriteLine(trade);
      }

      IBtceApiClient client = factory.CreateClient(API_KEY, API_SECRET);

      Depth btcusdDepth = publicClient.GetDepth(BtcePair.usd_rur);
      Console.WriteLine(btcusdDepth);
      decimal fee = publicClient.GetFee(BtcePair.usd_rur);
      Console.WriteLine(fee);

      try
      {
        UserInfo info = client.GetInfo();
        Console.WriteLine(info);
        TransHistory transHistory = client.GetTransHistory();
        Console.WriteLine(transHistory);
        TradeHistory tradeHistory = client.GetTradeHistory(count: 20);
        Console.WriteLine(tradeHistory);
        //// var orderList = btceApi.GetOrderList();  ** DEPRICATED ** use GetActiveOrders() instead!
        OrderList orderList = client.GetActiveOrders();
        Console.WriteLine(orderList);
        TradeAnswer tradeAnswer = client.Trade(BtcePair.btc_usd, TradeType.Sell, 2500, 0.1m);
        Console.WriteLine(tradeAnswer);
        CancelOrderAnswer cancelAnswer = client.CancelOrder(tradeAnswer.OrderId);
        Console.WriteLine(cancelAnswer);
      }
      catch (BtceException e)
      {
        Console.WriteLine("An exception occured: {0}", e);
      }

      Console.WriteLine("Press any key to stop...");
      Console.ReadKey();
    }
  }
}