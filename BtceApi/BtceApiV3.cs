// -----------------------------------------------------------------------
// <copyright file="BtceApiV3.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BtcE
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using Newtonsoft.Json.Linq;
	using System.Web;

	/// <summary>
	/// TODO: Update summary.
	/// </summary>
	public class BtceApiV3
	{
		private static string MakePairListString(BtcePair[] pairlist)
		{
			return string.Join("-", pairlist.Select(x => BtcePairHelper.ToString(x)).ToArray());
		}

		private static string Query(string method, BtcePair[] pairlist, Dictionary<string, string> args = null)
		{
			var pairliststr = MakePairListString(pairlist);
			StringBuilder sb = new StringBuilder();
			sb.Append("https://btc-e.com/api/3/");
			sb.Append(method);
			sb.Append("/");
			sb.Append(pairliststr);
			if (args != null && args.Count > 0)
			{
				sb.Append("?");
				var arr = args.Select(x => string.Format("{0}={1}", HttpUtility.UrlEncode(x.Key), HttpUtility.UrlEncode(x.Value))).ToArray();
				sb.Append(string.Join("&", arr));
			}
			var queryStr = sb.ToString();
			return WebApi.Query(queryStr);
		}

		private static string QueryIgnoreInvalid(string method, BtcePair[] pairlist, Dictionary<string, string> args = null)
		{
			var newargs = new Dictionary<string, string>() { { "ignore_invalid", "1" } };
			if (args != null)
				newargs.Concat(args);
			return Query(method, pairlist, newargs);
		}

		private static Dictionary<BtcePair, T> ReadPairDict<T>(JObject o, Func<JContainer, T> valueReader)
		{
			return o.OfType<JProperty>().Select(x => new KeyValuePair<BtcePair, T>(BtcePairHelper.FromString(x.Name), valueReader(x.Value as JContainer))).ToDictionary(x => x.Key, x => x.Value);
		}

		private static Dictionary<BtcePair, T> MakeRequest<T>(string method, BtcePair[] pairlist, Func<JContainer, T> valueReader, Dictionary<string, string> args = null, bool ignoreInvalid = true)
		{
			string queryresult;
			if (ignoreInvalid)
				queryresult = QueryIgnoreInvalid(method, pairlist, args);
			else
				queryresult = Query(method, pairlist, args);
			var resobj = JObject.Parse(queryresult);

			if (resobj["success"] != null && resobj.Value<int>("success") == 0)
				throw new Exception(resobj.Value<string>("error"));

			var r = ReadPairDict<T>(resobj, valueReader);
			return r;
		}

		public static Dictionary<BtcePair, Depth> GetDepth(BtcePair[] pairlist, int limit = 150)
		{
			return MakeRequest<Depth>("depth", pairlist, new Func<JContainer, Depth>(x => Depth.ReadFromJObject(x as JObject)), new Dictionary<string, string>() { { "limit", limit.ToString() } }, true);
		}

		public static Dictionary<BtcePair, Ticker> GetTicker(BtcePair[] pairlist)
		{
			return MakeRequest<Ticker>("ticker", pairlist, x => Ticker.ReadFromJObject(x as JObject), null, true);
		}

		public static Dictionary<BtcePair, List<TradeInfoV3>> GetTrades(BtcePair[] pairlist, int limit = 150)
		{
			Func<JContainer, List<TradeInfoV3>> tradeInfoListReader = (x => x.OfType<JObject>().Select(TradeInfoV3.ReadFromJObject).ToList());
			return MakeRequest<List<TradeInfoV3>>("trades", pairlist, tradeInfoListReader, new Dictionary<string, string>() { { "limit", limit.ToString() } }, true);
		}


	}

}
