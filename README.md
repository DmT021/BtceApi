C# API for btc-e.com
====================

Author
------------
  Galimzyanov Dmitry
  Russia, Ekaterinburg city

Example
------------
var ticker = BtceApi.GetTicker(BtcePair.BtcUsd);
var trades = BtceApi.GetTrades(BtcePair.BtcUsd);
var btcusdDepth = BtceApi.GetDepth(BtcePair.BtcUsd);

var btceApi = new BtceApi("YOUR-API-KEY", "your_secret_key");
var info = btceApi.GetInfo();
var transHistory = btceApi.GetTransHistory();
var tradeHistory = btceApi.GetTradeHistory(count: 20);
var orderList = btceApi.GetOrderList();
var tradeAnswer = btceApi.Trade(BtcePair.BtcUsd, TradeType.Sell, 20, 0.1m);
var cancelAnswer = btceApi.CancelOrder(tradeAnswer.OrderId);
