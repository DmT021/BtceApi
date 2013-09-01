C# API for btc-e.com
====================

Author
------------
  
  Galimzyanov Dmitry - first version(https://github.com/DmT021/BtceApi);
  kasthack - this fork.

Example/Non-login methods
------------

Synchronous:

var ticker = BtceApi.GetTicker(BtcePair.BtcUsd);
var trades = BtceApi.GetTrades(BtcePair.BtcUsd);
var btcusdDepth = BtceApi.GetDepth(BtcePair.UsdRur);
var fee = BtceApi.GetFee(BtcePair.UsdRur);

Async:

var ticker = await BtceApi.GetTickerAsync(BtcePair.BtcUsd);
var trades = await BtceApi.GetTradesAsync(BtcePair.BtcUsd);
var btcusdDepth = await BtceApi.GetDepthAsync(BtcePair.UsdRur);
var fee = await BtceApi.GetFeeAsync(BtcePair.UsdRur);

Example/Login-requiring methods
------------

var btceApi = new BtceApi("YOUR-API-KEY", "your_secret_key");

Synchronous:

var info = btceApi.GetInfo();
var transHistory = btceApi.GetTransHistory(new BtceApiTransHistoryParams());
var tradeHistory = btceApi.GetTradeHistory(new BtceApiTransHistoryParams(count: 20));
var orderList = btceApi.GetOrderList(new BtceApiTransHistoryParams());
var tradeAnswer = btceApi.Trade(new BtceApiTradeParams(BtcePair.BtcUsd, TradeType.Sell, 20, 0.1m));
var cancelAnswer = btceApi.CancelOrder(tradeAnswer.OrderId);

Asynchronous:

var info = await btceApi.GetInfo();
var transHistory = await btceApi.GetTransHistoryAsync(new BtceApiTransHistoryParams());
var tradeHistory = await btceApi.GetTradeHistoryAsync(new BtceApiTransHistoryParams(count: 20));
var orderList = await btceApi.GetOrderListAsync(new BtceApiTransHistoryParams());
var tradeAnswer = await btceApi.TradeAsync(new BtceApiTradeParams(BtcePair.BtcUsd, TradeType.Sell, 20, 0.1m));
var cancelAnswer = await btceApi.CancelOrderAsync(tradeAnswer.OrderId);
