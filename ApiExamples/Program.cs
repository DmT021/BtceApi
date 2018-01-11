//
// https://github.com/multiprogramm/WexAPI
//

using Wex;
using System;
using System.Collections.Generic;

namespace ApiTest
{
	class Program
	{
		static void Main(string[] args)
		{
			//-----------------------------------------------------------------
			Console.WriteLine("0. Check WexApi code is relevant.");
			APIRelevantTest.Check();
			Console.WriteLine("Code is ok.");

			//-----------------------------------------------------------------
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("1. Public API Tests.");

			// info example:
			{
				Console.WriteLine();
				Console.WriteLine("1.1. info");

				var info = WexPublicApi.Info();

				decimal btc_min_price_in_usd = info.Pairs[WexPair.btc_usd].MinPrice;
				Console.WriteLine("Min BTC price in wex.nz is {0}$", btc_min_price_in_usd);
			}

			var BTC_USD_PAIR = new WexPair[] { WexPair.btc_usd };

			// ticker example:
			{
				Console.WriteLine();
				Console.WriteLine("1.2. ticker");

				var ticker = WexPublicApi.Ticker(BTC_USD_PAIR);

				decimal btc_usd_hight = ticker[WexPair.btc_usd].High;
				decimal btc_usd_low = ticker[WexPair.btc_usd].Low;

				Console.WriteLine("I'm hamster. I bought BTC for {0}$ and sold for {1}$", btc_usd_hight, btc_usd_low);
			}

			// depth example:
			{
				Console.WriteLine();
				Console.WriteLine("1.3. depth");

				var depth = WexPublicApi.Depth(BTC_USD_PAIR, limit: 1);

				decimal btc_ask = depth[WexPair.btc_usd].Asks[0].Price;
				decimal btc_bid = depth[WexPair.btc_usd].Bids[0].Price;

				Console.WriteLine("Now BTC ask {0}$ and bid {1}$", btc_ask, btc_bid);
			}

			// trades example:
			{
				Console.WriteLine();
				Console.WriteLine("1.4. trades");

				var trades = WexPublicApi.Trades(BTC_USD_PAIR, limit: 1);

				string str_deal = (trades[WexPair.btc_usd][0].Type == DealType.Ask) ? "buy" : "sell";
				decimal amount = trades[WexPair.btc_usd][0].Amount;
				decimal price = trades[WexPair.btc_usd][0].Price;

				Console.WriteLine("BTC/USD: Last deal is {0} {1}BTC by price {2}$", str_deal, amount, price);
			}

			//-----------------------------------------------------------------
			// 2. Trade API
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine();
			Console.WriteLine("2. Trade API Tests.");

			Console.WriteLine("Enter API-key:");
			string api_key = Console.ReadLine();
			Console.WriteLine("Enter API secret key:");
			string api_secret = Console.ReadLine();

			var wex_trade_api = new WexTradeApi(api_key, api_secret);

			// GetInfo example:
			{
				Console.WriteLine();
				Console.WriteLine("2.1. GetInfo");

				var account_info = wex_trade_api.GetInfo();

				Console.WriteLine("Our balance:");
				foreach (var fund in account_info.Funds.Amounts)
					Console.WriteLine("{0}: {1}", WexCurrencyHelper.ToString(fund.Key), fund.Value);
			}


			// Trade example:
			{
				Console.WriteLine();
				Console.WriteLine("2.2. Trade");
				Console.WriteLine("I need to buy 1BTC for 10$.");

				Console.WriteLine("Commented because it's operation with real money :)");

				//var trade_result = wex_trade_api.Trade(WexPair.btc_usd, TradeType.Buy, 10.0m, 1m);

				//if( trade_result.OrderId == 0 )
				//	Console.WriteLine("Done. Now I have {0}BTC.", trade_result.Funds.Amounts[WexCurrency.btc]);
				//else
				//	Console.WriteLine("New order with id {0}.", trade_result.OrderId);
			}

			// ActiveOrders example:
			{
				Console.WriteLine();
				Console.WriteLine("2.3. ActiveOrders");
				Console.WriteLine("My BTC/USD orders:");

				OrderList active_orders = null;
				try
				{
					active_orders = wex_trade_api.ActiveOrders(pair: WexPair.btc_usd);
				}
				catch (Exception e)
				{
					// Wex, for example, throw error if orders don't exists :D
					Console.WriteLine(e.ToString());
				}

				if (active_orders != null)
				{
					foreach (var ord in active_orders.List)
					{
						Console.WriteLine("{0}: {1} {2} BTC by price {3}.",
							ord.Key, // order id
							TradeTypeHelper.ToString(ord.Value.Type), // buy or sell
							ord.Value.Amount, // count btc
							ord.Value.Rate // price in usd
						);
					}
				}
			}

			// OrderInfo example:
			{
				Console.WriteLine();
				Console.WriteLine("2.4. OrderInfo");

				Console.WriteLine("Commented because most likely order with id 12345 does not exists :)");

				//var order_info = wex_trade_api.OrderInfo(order_id: 12345);
				//Console.WriteLine("Order id 12345 has amount " + order_info.Amount);
			}

			// CancelOrder example:
			{
				Console.WriteLine();
				Console.WriteLine("2.5. CancelOrder");

				Console.WriteLine("Commented because it's operation with real money :)");

				//var cancel_order_info = wex_trade_api.CancelOrder(order_id: 12345);
				//Console.WriteLine("Order id 12345 is canceled, now I have {0} RUR", cancel_order_info.Funds.GetAmount(WexCurrency.rur));
			}

			// TradeHistory example:
			{
				Console.WriteLine();
				Console.WriteLine("2.6. TradeHistory");

				var trade_history = wex_trade_api.TradeHistory(
					pair: WexPair.btc_usd,
					count: 10,
					order_asc: false
				);

				Console.WriteLine("My last 10 BTC/USD deals:");
				foreach (var t in trade_history.List)
				{
					Console.WriteLine("{0}: {1} {2} BTC by price {3}.",
						t.Key, // order id
						TradeTypeHelper.ToString(t.Value.Type), // buy or sell
						t.Value.Amount, // count btc
						t.Value.Rate // price in usd
						);
				}
			}

			// TransHistory example:
			{
				Console.WriteLine();
				Console.WriteLine("2.7. TransHistory");

				var trans_history = wex_trade_api.TransHistory(
					count: 10,
					order_asc: false
				);

				Console.WriteLine("My last 10 transactions:");
				foreach (var t in trans_history.List)
				{
					Console.WriteLine("{0}: {1} {2}, {3}: {4}",
						t.Key, // order id
						t.Value.Amount,
						WexCurrencyHelper.ToString( t.Value.Currency ),
						TransTypeHelper.ToString( t.Value.Type ),
						t.Value.Description
						);
				}
			}

			// CoinDepositAddress example:
			{
				Console.WriteLine();
				Console.WriteLine("2.8. CoinDepositAddress");

				var coin_dep_addr = wex_trade_api.CoinDepositAddress(WexCurrency.btc);

				Console.WriteLine("My BTC deposit address is " + coin_dep_addr);
			}

			Console.ReadKey();
		}
	}
}
