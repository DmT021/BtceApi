C# API for btc-e.com
====================

Authors
------------
  
- Galimzyanov Dmitry - first version(https://github.com/DmT021/BtceApi)
- kasthack - this fork.

Example/Non-login methods
------------

Synchronous:

```c#
var ticker = BtceApi.GetTicker(BtcePair.BtcUsd);
var trades = BtceApi.GetTrades(BtcePair.BtcUsd);
var btcusdDepth = BtceApi.GetDepth(BtcePair.UsdRur);
var fee = BtceApi.GetFee(BtcePair.UsdRur);
```

Async:

```c#
var ticker = await BtceApi.GetTickerAsync(BtcePair.BtcUsd);
var trades = await BtceApi.GetTradesAsync(BtcePair.BtcUsd);
var btcusdDepth = await BtceApi.GetDepthAsync(BtcePair.UsdRur);
var fee = await BtceApi.GetFeeAsync(BtcePair.UsdRur);
```

Example/Login-requiring methods
------------

```c#
var btceApi = new BtceApi("YOUR-API-KEY", "your_secret_key");
```

Synchronous:

```c#
var info = btceApi.GetInfo();
var transHistory = btceApi.GetTransHistory();
var tradeHistory = btceApi.GetTradeHistory(count: 20);
var orderList = btceApi.GetOrderList();
var tradeAnswer = btceApi.Trade(BtcePair.BtcUsd, TradeType.Sell, 20, 0.1m);
var cancelAnswer = btceApi.CancelOrder(tradeAnswer.OrderId);
```

Asynchronous:

```c#
var info = await btceApi.GetInfo();
var transHistory = await btceApi.GetTransHistoryAsync();
var tradeHistory = await btceApi.GetTradeHistoryAsync(count: 20);
var orderList = await btceApi.GetOrderListAsync();
var tradeAnswer = await btceApi.TradeAsync(BtcePair.BtcUsd, TradeType.Sell, 20, 0.1m);
var cancelAnswer = await btceApi.CancelOrderAsync(tradeAnswer.OrderId);
```
