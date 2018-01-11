//
// https://github.com/multiprogramm/WexAPI
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using Wex.Utils;
using System.Globalization;
using System.Net;
using System.Web;
using System.IO;
using Newtonsoft.Json.Linq;

namespace Wex
{
	/// <summary>
	/// This API allows to trade on the exchange and receive information about the account.
	/// See https://wex.nz/tapi/docs
	/// </summary>
	public class WexTradeApi
	{
		// PUBLIC METHODS:

		/// <summary>
		/// See:
		/// https://wex.nz/tapi/docs#main
		/// https://wex.nz/tapi/docs#auth
		/// </summary>
		/// <param name="key">API-key</param>
		/// <param name="secret">Secret API-key</param>
		public WexTradeApi(string key, string secret)
		{
			m_ApiKey = key;
			m_HashSecret = new HMACSHA512(Encoding.ASCII.GetBytes(secret));
			m_Nonce = UnixTime.Now;
		}

		/// <summary>
		/// Returns information about the user’s current balance, API-key privileges, the number of open orders and Server Time.
		/// See https://wex.nz/tapi/docs#getInfo
		/// </summary>
		public UserInfo GetInfo()
		{
			var args = new Dictionary<string, string>()
			{
				{ "method", "getInfo" }
			};
			string query_answer = QueryExec(args);
			var json_result = ParseAnswer(query_answer);
			return UserInfo.ReadFromJObject(json_result);
		}

		/// <summary>
		/// The basic method that can be used for creating orders and trading on the exchange.
		/// See https://wex.nz/tapi/docs#Trade
		/// </summary>
		/// <param name="pair">Pair</param>
		/// <param name="type">Order type: buy or sell</param>
		/// <param name="rate">The rate at which you need to buy/sell</param>
		/// <param name="amount">The amount you need to buy/sell</param>
		public TradeAnswer Trade(
			WexPair pair,
			TradeType type,
			decimal rate,
			decimal amount )
		{
			var args = new Dictionary<string, string>()
			{
				{ "method", "Trade" },
				{ "pair", WexPairHelper.ToString(pair) },
				{ "type", TradeTypeHelper.ToString(type) },
				{ "rate", DecimalToString(rate) },
				{ "amount", DecimalToString(amount) }
			};
			string query_answer = QueryExec(args);
			var json_result = ParseAnswer(query_answer);
			return TradeAnswer.ReadFromJObject(json_result);
		}

		/// <summary>
		/// Returns the list of your active orders.
		/// See https://wex.nz/tapi/docs#ActiveOrders
		/// </summary>
		/// <param name="pair">pair, default all pairs</param>
		public OrderList ActiveOrders(WexPair pair = WexPair.Unknown)
		{
			var args = new Dictionary<string, string>()
			{
				{ "method", "ActiveOrders" }
			};

			if (pair != WexPair.Unknown)
				args.Add( "pair", WexPairHelper.ToString(pair) );

			string query_answer = QueryExec(args);
			var json_result = ParseAnswer(query_answer);
			return OrderList.ReadFromJObject(json_result);
		}

		/// <summary>
		/// Returns the information on particular order.
		/// See https://wex.nz/tapi/docs#OrderInfo
		/// </summary>
		/// <param name="order_id">order ID</param>
		public Order OrderInfo(int order_id)
		{
			var args = new Dictionary<string, string>()
			{
				{ "method", "OrderInfo" },
				{ "order_id", order_id.ToString() }
			};
			string query_answer = QueryExec(args);
			var json_result = ParseAnswer(query_answer);

			OrderList ord_list = OrderList.ReadFromJObject(json_result);
			return ord_list.List.Values.First();
		}

		/// <summary>
		/// This method is used for order cancelation.
		/// See https://wex.nz/tapi/docs#CancelOrder
		/// </summary>
		/// <param name="order_id">order ID</param>
		public CancelOrderAnswer CancelOrder(int order_id)
		{
			var args = new Dictionary<string, string>()
			{
				{ "method", "CancelOrder" },
				{ "order_id", order_id.ToString() }
			};
			string query_answer = QueryExec(args);
			var json_result = ParseAnswer(query_answer);
			return CancelOrderAnswer.ReadFromJObject(json_result);
		}

		/// <summary>
		/// Returns trade history.
		/// See https://wex.nz/tapi/docs#TradeHistory
		/// </summary>
		/// <param name="from">Trade ID, from which the display starts</param>
		/// <param name="count">The number of trades for display</param>
		/// <param name="from_id">Trade ID, from which the display starts</param>
		/// <param name="end_id">Trade ID on which the display ends</param>
		/// <param name="order_asc">Sorting</param>
		/// <param name="since">The time to start the display</param>
		/// <param name="end">The time to end the display</param>
		/// <param name="pair">Pair to be displayed</param>
		public TradeHistoryAnswer TradeHistory(
			int? from = null,
			int? count = null,
			int? from_id = null,
			int? end_id = null,
			bool? order_asc = null,
			DateTime? since = null,
			DateTime? end = null,
			WexPair pair = WexPair.Unknown )
		{
			var args = new Dictionary<string, string>()
			{
				{ "method", "TradeHistory" }
			};

			if (from != null)
				args.Add("from", from.Value.ToString());
			if (count != null)
				args.Add("count", count.Value.ToString());
			if (from_id != null)
				args.Add("from_id", from_id.Value.ToString());
			if (end_id != null)
				args.Add("end_id", end_id.Value.ToString());
			if (order_asc != null)
				args.Add("order", (order_asc.Value ? "ASC" : "DESC"));
			if (since != null)
				args.Add("since", UnixTime.GetFromDateTime(since.Value).ToString());
			if (end != null)
				args.Add("end", UnixTime.GetFromDateTime(end.Value).ToString());
			if (pair != WexPair.Unknown)
				args.Add("pair", WexPairHelper.ToString(pair));

			string query_answer = QueryExec(args);
			var json_result = ParseAnswer(query_answer);
			return TradeHistoryAnswer.ReadFromJObject(json_result);
		}

		/// <summary>
		/// Returns the history of transactions.
		/// See https://wex.nz/tapi/docs#TransHistory
		/// </summary>
		/// <param name="from">Transaction ID, from which the display starts</param>
		/// <param name="count">Number of transaction to be displayed</param>
		/// <param name="from_id">Transaction ID, from which the display starts</param>
		/// <param name="end_id">Transaction ID on which the display ends</param>
		/// <param name="order_asc">Sorting</param>
		/// <param name="since">The time to start the display</param>
		/// <param name="end">The time to end the display</param>
		public TransHistoryAnswer TransHistory(
			int? from = null,
			int? count = null,
			int? from_id = null,
			int? end_id = null,
			bool? order_asc = null,
			DateTime? since = null,
			DateTime? end = null )
		{
			var args = new Dictionary<string, string>()
			{
				{ "method", "TransHistory" }
			};

			if (from != null)
				args.Add("from", from.Value.ToString());
			if (count != null)
				args.Add("count", count.Value.ToString());
			if (from_id != null)
				args.Add("from_id", from_id.Value.ToString());
			if (end_id != null)
				args.Add("end_id", end_id.Value.ToString());
			if (order_asc != null)
				args.Add("order", order_asc.Value ? "ASC" : "DESC");
			if (since != null)
				args.Add("since", UnixTime.GetFromDateTime(since.Value).ToString());
			if (end != null)
				args.Add("end", UnixTime.GetFromDateTime(end.Value).ToString());

			string query_answer = QueryExec(args);
			var json_result = ParseAnswer(query_answer);
			return TransHistoryAnswer.ReadFromJObject(json_result);
		}

		/// <summary>
		/// This method can be used to retrieve the address for depositing crypto-currency.
		/// See https://wex.nz/tapi/docs#CoinDepositAddress
		/// </summary>
		/// <param name="crypto_currency">Crypto currency</param>
		/// <returns>address for deposits</returns>
		public string CoinDepositAddress(WexCurrency crypto_currency)
		{
			var args = new Dictionary<string, string>()
			{
				{ "method", "CoinDepositAddress" },
				{ "coinName", WexCurrencyHelper.ToString(crypto_currency).ToUpperInvariant() }
			};

			string query_answer = QueryExec(args);
			var json_result = ParseAnswer(query_answer);
			return json_result.Value<string>("address");
		}



		// PRIVATE MEMBERS:

		string m_ApiKey;
		HMACSHA512 m_HashSecret;
		UInt32 m_Nonce;


		// PRIVATE METHODS:

		string QueryExec(Dictionary<string, string> args)
		{
			args.Add("nonce", GetNonce().ToString());

			var data_str = BuildPostData(args);
			var data = Encoding.ASCII.GetBytes(data_str);

			var request = WebRequest.Create(new Uri(Global.Site + "/tapi")) as HttpWebRequest;
			if (request == null)
				throw new Exception("Non HTTP WebRequest");

			request.Method = "POST";
			request.Timeout = 15000;
			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = data.Length;

			request.Headers.Add("Key", m_ApiKey);
			request.Headers.Add("Sign", ByteArrayToString(m_HashSecret.ComputeHash(data)).ToLower());

			using (var req_stream = request.GetRequestStream())
				req_stream.Write(data, 0, data.Length);

			using (var resp = request.GetResponse())
			using (var reader = new StreamReader(resp.GetResponseStream()))
				return reader.ReadToEnd();
		}

		UInt32 GetNonce()
		{
			return m_Nonce++;
		}

		static string BuildPostData(Dictionary<string, string> dict_args)
		{
			StringBuilder result = new StringBuilder();
			bool is_first = true;
			foreach (var arg_pair in dict_args)
			{
				if (is_first)
					is_first = false;
				else
					result.Append("&");

				result.AppendFormat("{0}={1}", arg_pair.Key, HttpUtility.UrlEncode(arg_pair.Value));
			}

			return result.ToString();
		}

		static string ByteArrayToString(byte[] byte_arr)
		{
			return BitConverter.ToString(byte_arr).Replace("-", "");
		}

		static string DecimalToString(decimal value)
		{
			return value.ToString(CultureInfo.InvariantCulture);
		}

		private JObject ParseAnswer(string result_str)
		{
			JObject json_root = null;
			try
			{
				json_root = JObject.Parse(result_str);
			}
			catch { }

			if (json_root == null)
			{
				// If result_str is not json string, it's non-json error
				// See https://wex.nz/tapi/docs#main
				throw new Exception(result_str);
			}

			if (json_root.Value<int>("success") == 0)
			{
				// Regular json response with error
				// See https://wex.nz/tapi/docs#main
				throw new Exception(json_root.Value<string>("error"));
			}

			return (json_root["return"] as JObject);
		}
	}
}
