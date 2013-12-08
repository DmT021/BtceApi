C# API for btc-e.com
====================

Authors
------------
  
- Galimzyanov Dmitry - Russia, Ekaterinburg city - https://github.com/DmT021/BtceApi Donate: 131W3PYbiydd7K8XhYo7YqPodUPVhztFMi
- Gnosis SuperFund - Australia, Adelaide - https://github.com/GnosisSuperFund/BtceApi
- n074v41l4bl34u - Poland, Warsaw - https://github.com/n074v41l4bl34u/BtceApi

Example - Public Mehtods
------------

Synchronous:

Example - V3 API, Non-Authenticated Methods

```c#
var depths3 = BtceApiV3.GetDepth(new BtcePair[] { BtcePair.btc_usd });
foreach (var depth3 in depths3)
{
	Console.WriteLine("{0}: {1}",depth3.Key, depth3.Value);
}
var tickers3 = BtceApiV3.GetTicker(new BtcePair[] { BtcePair.btc_usd });
foreach (var ticker3 in tickers3)
{
	Console.WriteLine("{0}: {1}", ticker3.Key, ticker3.Value);
}	
var trades3 = BtceApiV3.GetTrades(new BtcePair[] { BtcePair.btc_usd });
foreach (var trade3 in trades3)
{
	Console.WriteLine(trade3);
}
```

Example - V2 API, Non-Authenticated Methods

```c#
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
```

Example - Authenticated Methods, creation

```c#
var btceApi = new BtceApi("API_KEY", "API_SECRET");
```

Example - Authenticated Methods, synchronous:

```c#
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
```
