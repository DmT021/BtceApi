/*
 * Base for making api class for btc-e.com
 * DmT
 * 2012
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BtcE.Utils;
using System.Web;
using System.Globalization;

namespace BtcE
{
    public class BtceApi
    {
        string key;
        HMACSHA512 hashMaker;
        UInt32 nonce;

        public BtceApi(string key, string secret)
        {
            this.key = key;
            hashMaker = new HMACSHA512(Encoding.ASCII.GetBytes(secret));
            nonce = UnixTime.Now;
        }

        public UserInfo GetInfo()
        {
            var resultStr = Query(new Dictionary<string, string>()
            {
                { "method", "getInfo" }
            });
            var result = JObject.Parse(resultStr);
            if (result.Value<int>("success") == 0)
            {
                throw new Exception(result.Value<string>("error"));
            }

            return UserInfo.ReadFromJObject(result["return"] as JObject);
        }

        public TransHistory GetTransHistory(
            int? from = null,
            int? count = null,
            int? fromId = null,
            int? endId = null,
            bool? orderAsc = null,
            DateTime? since = null,
            DateTime? end = null
            )
        {
            var args = new Dictionary<string, string>()
            {
                { "method", "TransHistory" }
            };

            if (from != null) args.Add("from", from.Value.ToString());
            if (count != null) args.Add("count", count.Value.ToString());
            if (fromId != null) args.Add("from_id", fromId.Value.ToString());
            if (endId != null) args.Add("end_id", endId.Value.ToString());
            if (orderAsc != null) args.Add("order", orderAsc.Value ? "ASC" : "DESC");
            if (since != null) args.Add("since", UnixTime.GetFromDateTime(since.Value).ToString());
            if (end != null) args.Add("end", UnixTime.GetFromDateTime(end.Value).ToString());

            var resultStr = Query(args);
            var result = JObject.Parse(resultStr);
            if (result.Value<int>("success") == 0)
            {
                throw new Exception(result.Value<string>("error"));
            }

            return TransHistory.ReadFromJObject(result["return"] as JObject);
        }

        public TradeHistory GetTradeHistory(
            int? from = null,
            int? count = null,
            int? fromId = null,
            int? endId = null,
            bool? orderAsc = null,
            DateTime? since = null,
            DateTime? end = null
            )
        {
            var args = new Dictionary<string, string>()
            {
                { "method", "TradeHistory" }
            };

            if (from != null) args.Add("from", from.Value.ToString());
            if (count != null) args.Add("count", count.Value.ToString());
            if (fromId != null) args.Add("from_id", fromId.Value.ToString());
            if (endId != null) args.Add("end_id", endId.Value.ToString());
            if (orderAsc != null) args.Add("order", orderAsc.Value ? "ASC" : "DESC");
            if (since != null) args.Add("since", UnixTime.GetFromDateTime(since.Value).ToString());
            if (end != null) args.Add("end", UnixTime.GetFromDateTime(end.Value).ToString());

            var resultStr = Query(args);
            var result = JObject.Parse(resultStr);
            if (result.Value<int>("success") == 0)
            {
                throw new Exception(result.Value<string>("error"));
            }

            return TradeHistory.ReadFromJObject(result["return"] as JObject);
        }

        public OrderList GetOrderList(
            int? from = null,
            int? count = null,
            int? fromId = null,
            int? endId = null,
            bool? orderAsc = null,
            DateTime? since = null,
            DateTime? end = null,
            BtcePair? pair = null,
            bool? active = null
            )
        {
            var args = new Dictionary<string, string>()
            {
                { "method", "OrderList" }
            };

            if (from != null) args.Add("from", from.Value.ToString());
            if (count != null) args.Add("count", count.Value.ToString());
            if (fromId != null) args.Add("from_id", fromId.Value.ToString());
            if (endId != null) args.Add("end_id", endId.Value.ToString());
            if (orderAsc != null) args.Add("order", orderAsc.Value ? "ASC" : "DESC");
            if (since != null) args.Add("since", UnixTime.GetFromDateTime(since.Value).ToString());
            if (end != null) args.Add("end", UnixTime.GetFromDateTime(end.Value).ToString());
            if (pair != null) args.Add("pair", BtcePairHelper.ToString(pair.Value));
            if (active != null) args.Add("active", active.Value ? "1" : "0");

            var resultStr = Query(args);
            var result = JObject.Parse(resultStr);
            if (result.Value<int>("success") == 0)
            {
                throw new Exception(result.Value<string>("error"));
            }

            return OrderList.ReadFromJObject(result["return"] as JObject);
        }

        public TradeAnswer Trade(BtcePair pair, TradeType type, decimal rate, decimal amount)
        {
            var args = new Dictionary<string, string>()
            {
                { "method", "Trade" },
                { "pair", BtcePairHelper.ToString(pair) },
                { "type", TradeTypeHelper.ToString(type) },
                { "rate", DecimalToString(rate) },
                { "amount", DecimalToString(amount) }
            };

            var resultStr = Query(args);
            var result = JObject.Parse(resultStr);
            if (result.Value<int>("success") == 0)
            {
                throw new Exception(result.Value<string>("error"));
            }

            return TradeAnswer.ReadFromJObject(result["return"] as JObject);
        }

        public CancelOrderAnswer CancelOrder(int orderId)
        {
            var args = new Dictionary<string, string>()
            {
                { "method", "CancelOrder" },
                { "order_id", orderId.ToString() }
            };

            var resultStr = Query(args);
            var result = JObject.Parse(resultStr);
            if (result.Value<int>("success") == 0)
            {
                throw new Exception(result.Value<string>("error"));
            }

            return CancelOrderAnswer.ReadFromJObject(result["return"] as JObject);
        }

        string Query(Dictionary<string, string> args)
        {
            args.Add("nonce", GetNonce().ToString());

            var dataStr = BuildPostData(args);
            var data = Encoding.ASCII.GetBytes(dataStr);

            var request = WebRequest.Create(new Uri("https://btc-e.com/tapi")) as HttpWebRequest;
            if (request == null)
                throw new Exception("Non HTTP WebRequest");

            request.Method = "POST";
            request.Timeout = 15000;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            request.Headers.Add("Key", key);
            var sign = ByteArrayToString(hashMaker.ComputeHash(data)).ToLower();
            request.Headers.Add("Sign", sign);
            var reqStream = request.GetRequestStream();
            reqStream.Write(data, 0, data.Length);
            reqStream.Close();

            var response = request.GetResponse();
            var resStream = response.GetResponseStream();
            var resStreamReader = new StreamReader(resStream);
            var resString = resStreamReader.ReadToEnd();

            return resString;
        }

        static string ByteArrayToString(byte[] ba)
        {
            string hex = BitConverter.ToString(ba);
            return hex.Replace("-", "");
        }

        static string BuildPostData(Dictionary<string, string> d)
        {
            string s = "";
            for (int i = 0; i < d.Count; i++)
            {
                var item = d.ElementAt(i);
                var key = item.Key;
                var val = item.Value;

                s += String.Format("{0}={1}", key, HttpUtility.UrlEncode(val));

                if (i != d.Count - 1)
                    s += "&";
            }
            return s;
        }

        UInt32 GetNonce()
        {
            return nonce++;
        }

        static string DecimalToString(decimal d)
        {
            return d.ToString(CultureInfo.InvariantCulture);
        }

        public static Depth GetDepth(BtcePair pair)
        {
            string resStr;
            string queryStr = string.Format("https://btc-e.com/api/2/{0}/depth", BtcePairHelper.ToString(pair));
            resStr = Query(queryStr);
            

            var res = JObject.Parse(resStr);
            return Depth.ReadFromJObject(res);
        }

        public static Ticker GetTicker(BtcePair pair)
        {
            string resStr;
            string queryStr = string.Format("https://btc-e.com/api/2/{0}/ticker", BtcePairHelper.ToString(pair));
            resStr = Query(queryStr);

            var res = JObject.Parse(resStr);
            return Ticker.ReadFromJObject(res["ticker"] as JObject);
        }

        public static List<TradeInfo> GetTrades(BtcePair pair)
        {
            string resStr;

            string queryStr = string.Format("https://btc-e.com/api/2/{0}/trades", BtcePairHelper.ToString(pair));
            resStr = Query(queryStr);

            var res = JArray.Parse(resStr);
            var ret = new List<TradeInfo>();
            foreach (var item in res)
            {
                ret.Add(TradeInfo.ReadFromJObject(item as JObject));
            }
            return ret;
        }

        public static decimal GetFee(BtcePair pair)
        {
            string resStr;

            string queryStr = string.Format("https://btc-e.com/api/2/{0}/fee", BtcePairHelper.ToString(pair));
            resStr = Query(queryStr);

            var res = JObject.Parse(resStr);
            return res.Value<decimal>("trade");
        }
        
        static string Query(string url)
        {
            var request = WebRequest.Create(new Uri(url)) as HttpWebRequest;
            if (request == null)
                throw new Exception("Non HTTP WebRequest");

            var response = request.GetResponse();
            var resStream = response.GetResponseStream();
            var resStreamReader = new StreamReader(resStream);
            var resString = resStreamReader.ReadToEnd();

            return resString; 
        }
    }
}
