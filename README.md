C# API for btc-e.com
====================

Authors
------------
  
- Galimzyanov Dmitry - Russia, Ekaterinburg city - https://github.com/DmT021/BtceApi
- Gnosis SuperFund - Australia, Adelaide - https://github.com/GnosisSuperFund/BtceApi

Example - Public Mehtods
------------

Synchronous:

```c#
var ticker = BtceApi.GetTicker(BtcePair.btc_usd);
var trades = BtceApi.GetTrades(BtcePair.btc_usd);
var btcusdDepth = BtceApi.GetDepth(BtcePair.usd_rur);
var fee = BtceApi.GetFee(BtcePair.usd_rur);
```

Example - Authenticated Methods
------------

```c#
var btceApi = new BtceApi("API_KEY", "API_SECRET");
```

Synchronous:

```c#
var info = btceApi.GetInfo();
var transHistory = btceApi.GetTransHistory();
var tradeHistory = btceApi.GetTradeHistory(count: 20);
//var orderList = btceApi.GetOrderList();  ** DEPRICATED ** use GetActiveOrders() instead!
var orderList = btceApi.GetActiveOrders();
var tradeAnswer = btceApi.Trade(BtcePair.btc_usd, TradeType.Sell, 20, 0.1m);
var cancelAnswer = btceApi.CancelOrder(tradeAnswer.OrderId);
```
