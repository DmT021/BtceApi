//
// https://github.com/multiprogramm/WexAPI
//

using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Wex.Utils;

namespace Wex
{
	/// <summary>
	/// This api provides access to such information as tickers of currency pairs,
	///    active orders on different pairs, the latest trades for each pair etc.
	/// See https://wex.nz/api/3/docs
	/// </summary>
	public static class WexPublicApi
	{
		// PUBLIC METHODS:

		/// <summary>
		/// This method provides all the information about currently active pairs, 
		///     such as the maximum number of digits after the decimal point, 
		///     the minimum price, the maximum price, the minimum transaction size, 
		///     whether the pair is hidden, the commission for each pair.
		/// See https://wex.nz/api/3/docs#info
		/// </summary>
		public static InfoAnswer Info()
		{
			string query = BuildQuery("info", null, null);
			string query_answer = ExecQuery(query);
			var json_result = ParseAnswer(query_answer);
			return InfoAnswer.ReadFromJObject(json_result);
		}

		/// <summary>
		/// This method provides all the information about currently active pairs, such as:
		///     the maximum price, the minimum price, average price, trade volume, trade
		///     volume in currency, the last trade, Buy and Sell price.
		/// See https://wex.nz/api/3/docs#ticker
		/// </summary>
		/// <param name="pairlist"></param>
		/// <param name="is_ignore_invalid"></param>
		public static Dictionary<WexPair, TickerAnswer> Ticker(
			WexPair[] pairlist,
			bool is_ignore_invalid = false)
		{
			return MakeRequest<TickerAnswer>("ticker", pairlist,
				x => TickerAnswer.ReadFromJObject(x as JObject),
				null,
				is_ignore_invalid
			);
		}

		/// <summary>
		/// This method provides the information about active orders on the pair.
		/// See https://wex.nz/api/3/docs#depth
		/// </summary>
		/// <param name="pairlist"></param>
		/// <param name="limit"></param>
		/// <param name="is_ignore_invalid"></param>
		public static Dictionary<WexPair, DepthAnswer> Depth(
			WexPair[] pairlist,
			int limit = 150,
			bool is_ignore_invalid = false)
		{
			return MakeRequest<DepthAnswer>("depth", pairlist,
				new Func<JContainer, DepthAnswer>(x => DepthAnswer.ReadFromJObject(x as JObject)),
				new Dictionary<string, string>() { { "limit", limit.ToString() } },
				is_ignore_invalid
			);
		}

		/// <summary>
		/// This method provides the information about the last trades.
		/// See https://wex.nz/api/3/docs#trades
		/// </summary>
		/// <param name="pairlist"></param>
		/// <param name="limit"></param>
		/// <param name="is_ignore_invalid"></param>
		public static Dictionary<WexPair, List<Deal>> Trades(
			WexPair[] pairlist,
			int limit = 150,
			bool is_ignore_invalid = false)
		{
			return MakeRequest<List<Deal>>("trades", pairlist,
				x => x.OfType<JObject>().Select(Deal.ReadFromJObject).ToList(),
				new Dictionary<string, string>() { { "limit", limit.ToString() } },
				is_ignore_invalid
			);
		}

		/// <summary>
		/// Info request, but without JSON result parsing
		/// </summary>
		/// <returns></returns>
		public static JObject InfoRequestWithoutParse()
		{
			string query = BuildQuery("info", null, null);
			string query_answer = ExecQuery(query);
			var json_result = ParseAnswer(query_answer);
			return json_result;
		}


		// PRIVATE METHODS

		private static Dictionary<WexPair, T> MakeRequest<T>(
			string method,
			WexPair[] pairlist,
			Func<JContainer, T> value_reader,
			Dictionary<string, string> args = null,
			bool ignore_invalid = true)
		{
			if( args == null )
				args = new Dictionary<string, string>();
			if (ignore_invalid)
				args.Add("ignore_invalid", "1");


			string query = BuildQuery(method, pairlist, args);
			string query_answer = ExecQuery(query);
			var json_result = ParseAnswer(query_answer);
			return JsonHelpers.ReadPairDict<T>(json_result, value_reader);
		}

		private static string BuildQuery(
			string method,
			WexPair[] pairlist,
			Dictionary<string, string> args )
		{
			StringBuilder query_builder = new StringBuilder();
			query_builder.Append(Global.Site + "/api/3/");
			query_builder.Append(method);
			query_builder.Append("/");
			query_builder.Append(MakePairListString(pairlist));

			if (args != null)
			{
				bool is_first_arg = true;
				foreach (var cur_arg in args)
				{
					if (is_first_arg)
					{
						query_builder.Append('?');
						is_first_arg = false;
					}
					else
						query_builder.Append('&');

					query_builder.AppendFormat("{0}={1}",
						HttpUtility.UrlEncode(cur_arg.Key),
						HttpUtility.UrlEncode(cur_arg.Value)
					);
				}
			}

			return query_builder.ToString();
		}

		private static string MakePairListString(WexPair[] pairlist)
		{
			if (pairlist == null)
				return "";
			return string.Join(
				"-",
				pairlist.Select(x => WexPairHelper.ToString(x)).ToArray()
			);
		}

		private static string ExecQuery(string url)
		{
			var request = WebRequest.Create(url);
			request.Proxy = WebRequest.DefaultWebProxy;
			request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
			if (request == null)
				throw new Exception("Non HTTP WebRequest");

			using(var resp = request.GetResponse() )
			using(var reader = new StreamReader(resp.GetResponseStream()))
				return reader.ReadToEnd();
		}

		private static JObject ParseAnswer(string result_str)
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
				throw new Exception(result_str);
			}

			if (json_root["success"] != null && json_root.Value<int>("success") == 0)
			{
				// Regular json response with error
				throw new Exception(json_root.Value<string>("error"));
			}

			return json_root;
		}
	}

}
