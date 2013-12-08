/*
 * Base for making api class for btc-e.com
 * DmT
 * 2012
 */

namespace BtcE
{
  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.IO;
  using System.Linq;
  using System.Net;
  using System.Security.Cryptography;
  using System.Text;
  using System.Web;
  using BtcE.Utils;
  using Newtonsoft.Json.Linq;

  public class BtceApi : IDisposable
  {
    public static string ExchangeHost = "https://btc-e.com/";
    private readonly string instanseExchangeHost;
    private bool disposed/* = false*/;
    private object disposeLock = new object();
    private HMACSHA512 hashMaker;
    private string key;
    private uint nonce;

    public BtceApi(string key, string secret, string exchangeHost = null)
    {
      this.key = key;
      hashMaker = new HMACSHA512(Encoding.ASCII.GetBytes(secret));
      nonce = UnixTime.Now;
      this.instanseExchangeHost = exchangeHost ?? ExchangeHost;
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="BtceApi" /> class.
    /// </summary>
    ~BtceApi()
    {
      Dispose(false);
    }

    public static Depth GetDepth(BtcePair pair)
    {
      return Depth.ReadFromJObject(JObject.Parse(Query(string.Format("{1}api/2/{0}/depth", BtcePairHelper.ToString(pair), ExchangeHost))));
    }

    /// <summary>
    /// Returns the fee for the supplied currnecy pair
    /// </summary>
    /// <param name="pair">Currency pair to obtain fee for </param>
    /// <returns>Fee amount for trading the supplied currency pair</returns>
    public static decimal GetFee(BtcePair pair)
    {
      return JObject.Parse(Query(string.Format("{1}api/2/{0}/fee", BtcePairHelper.ToString(pair), ExchangeHost))).Value<decimal>("trade");
    }

    public static Ticker GetTicker(BtcePair pair)
    {
      return Ticker.ReadFromJObject(JObject.Parse(Query(string.Format("{1}api/2/{0}/ticker", BtcePairHelper.ToString(pair), ExchangeHost)))["ticker"] as JObject);
    }

    public static TradeInfo[] GetTrades(BtcePair pair)
    {
      return JArray.Parse(Query(string.Format("{1}api/2/{0}/trades", BtcePairHelper.ToString(pair), ExchangeHost))).OfType<JObject>().Select(TradeInfo.ReadFromJObject).ToArray();
    }

    public CancelOrderAnswer CancelOrder(int orderId)
    {
      var args = new Dictionary<string, string>()
            {
                { "method", "CancelOrder" },
                { "order_id", orderId.ToString() }
            };
      var result = JObject.Parse(Query(args));
      if (result.Value<int>("success") == 0)
      {
        throw new BtceException(result.Value<string>("error"));
      }

      return CancelOrderAnswer.ReadFromJObject(result["return"] as JObject);
    }

    /// <summary>
    /// Clean up
    /// </summary>
    public void Dispose()
    {
      Dispose(true);
      GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Get active Buy & Sell orders
    /// </summary>
    /// <param name="pair">Currency to botain orders for, if null all currencies are used</param>
    /// <returns>A list of both buy and sell orders for the supplied currency</returns>
    /// <exception cref="BtceException">Throws  when an eror occurs</exception>
    public OrderList GetActiveOrders(BtcePair? pair = null)
    {
      var args = new Dictionary<string, string>()
            {
                { "method", "ActiveOrders" }
            };
      if (pair != null)
      {
        args.Add("pair", BtcePairHelper.ToString(pair.Value));
      }

      var result = JObject.Parse(Query(args));
      if (result.Value<int>("success") == 0)
        throw new BtceException(result.Value<string>("error"));
      return OrderList.ReadFromJObject(result["return"] as JObject);
    }

    /// <summary>
    /// Get the Information about a users account, orders and funds
    /// </summary>
    /// <returns>A populated UserInfo object</returns>
    public UserInfo GetInfo()
    {
      var resultStr = Query(new Dictionary<string, string>() { { "method", "getInfo" } });
      var result = JObject.Parse(resultStr);
      if (result.Value<int>("success") == 0)
        throw new BtceException(result.Value<string>("error"));
      return UserInfo.ReadFromJObject(result["return"] as JObject);
    }

    /// <summary>
    /// GetOrderList() is depricated and will no longer be supported as of Nov. 1st 2013. Please use GetActiveOrders() instead."
    /// </summary>
    /// <returns>Since the method is no longer supported it always throws BtceException</returns>
    /// <exception cref="BtceException">GetOrderList() is depricated and will no longer be supported as of Nov. 1st 2013. Please use GetActiveOrders() instead."</exception>
    [Obsolete("GetOrderList() is depricated and will no longer be supported as of Nov. 1st 2013. Please use GetActiveOrders() instead.")]
    public OrderList GetOrderList(
      int? from = null,
      int? count = null,
      int? fromId = null,
      int? endId = null,
      bool? orderAsc = null,
      DateTime? since = null,
      DateTime? end = null,
      BtcePair? pair = null,
      bool? active = null)
    {
      throw new BtceException("GetOrderList() is depricated and will no longer be supported as of Nov. 1st 2013. Please use GetActiveOrders() instead.");
    }

    public TradeHistory GetTradeHistory(
      int? from = null,
      int? count = null,
      int? fromId = null,
      int? endId = null,
      bool? orderAsc = null,
      DateTime? since = null,
      DateTime? end = null,
      BtcePair? pair = null)
    {
      var args = new Dictionary<string, string>() { { "method", "TradeHistory" } };

      if (from != null)
      {
        args.Add("from", from.Value.ToString());
      }

      if (count != null)
      {
        args.Add("count", count.Value.ToString());
      }

      if (fromId != null)
      {
        args.Add("from_id", fromId.Value.ToString());
      }

      if (endId != null)
      {
        args.Add("end_id", endId.Value.ToString());
      }

      if (orderAsc != null)
      {
        args.Add("order", orderAsc.Value ? "ASC" : "DESC");
      }

      if (since != null)
      {
        args.Add("since", UnixTime.GetFromDateTime(since.Value).ToString());
      }

      if (end != null)
      {
        args.Add("end", UnixTime.GetFromDateTime(end.Value).ToString());
      }

      if (pair != null)
      {
        args.Add("pair", BtcePairHelper.ToString(pair.Value));
      }

      var result = JObject.Parse(Query(args));
      if (result.Value<int>("success") == 0)
        throw new BtceException(result.Value<string>("error"));
      return TradeHistory.ReadFromJObject(result["return"] as JObject);
    }

    public TransHistory GetTransHistory(
      int? from = null,
      int? count = null,
      int? fromId = null,
      int? endId = null,
      bool? orderAsc = null,
      DateTime? since = null,
      DateTime? end = null)
    {
      var args = new Dictionary<string, string>() { { "method", "TransHistory" } };

      if (from != null)
      {
        args.Add("from", from.Value.ToString());
      }

      if (count != null)
      {
        args.Add("count", count.Value.ToString());
      }

      if (fromId != null)
      {
        args.Add("from_id", fromId.Value.ToString());
      }

      if (endId != null)
      {
        args.Add("end_id", endId.Value.ToString());
      }

      if (orderAsc != null)
      {
        args.Add("order", orderAsc.Value ? "ASC" : "DESC");
      }

      if (since != null)
      {
        args.Add("since", UnixTime.GetFromDateTime(since.Value).ToString());
      }

      if (end != null)
      {
        args.Add("end", UnixTime.GetFromDateTime(end.Value).ToString());
      }

      var result = JObject.Parse(Query(args));
      if (result.Value<int>("success") == 0)
      {
        throw new BtceException(result.Value<string>("error"));
      }

      return TransHistory.ReadFromJObject(result["return"] as JObject);
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
      var result = JObject.Parse(Query(args));
      if (result.Value<int>("success") == 0)
      {
        throw new BtceException(result.Value<string>("error"));
      }

      return TradeAnswer.ReadFromJObject(result["return"] as JObject);
    }

    /// <summary>
    /// Clean up
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
      // make thread safe
      lock (disposeLock)
      {
        // conform to reference example: http://msdn.microsoft.com/en-us/library/fs2xkftw.aspx
        if (!disposed)
        {
          if (disposing)
          {
            // null check
            if (hashMaker != null)
            {
              // invoke disposing
              hashMaker.Dispose();
              hashMaker = null; // null just to br safe
            }
          }
        }
      }
    }

    private static string BuildPostData(Dictionary<string, string> d)
    {
      StringBuilder s = new StringBuilder();
      foreach (var item in d)
      {
        s.AppendFormat("{0}={1}", item.Key, HttpUtility.UrlEncode(item.Value));
        s.Append("&");
      }

      if (s.Length > 0)
      {
        s.Remove(s.Length - 1, 1);
      }

      return s.ToString();
    }

    private static string ByteArrayToString(byte[] ba)
    {
      return BitConverter.ToString(ba).Replace("-", string.Empty);
    }

    private static string DecimalToString(decimal d)
    {
      return d.ToString(CultureInfo.InvariantCulture);
    }

    private static string Query(string url)
    {
      var request = WebRequest.Create(url);
      request.Proxy = WebRequest.DefaultWebProxy;
      request.Proxy.Credentials = System.Net.CredentialCache.DefaultCredentials;
      if (request == null)
      {
        throw new HttpException("Non HTTP WebRequest");
      }

      return new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
    }

    private uint GetNonce()
    {
      return nonce++;
    }

    private string Query(Dictionary<string, string> args)
    {
      args.Add("nonce", GetNonce().ToString());

      var dataStr = BuildPostData(args);
      var data = Encoding.ASCII.GetBytes(dataStr);

      var request = WebRequest.Create(new Uri(this.instanseExchangeHost + "tapi")) as HttpWebRequest;
      if (request == null)
        throw new HttpException("Non HTTP WebRequest");

      request.Method = "POST";
      request.Timeout = 15000;
      request.ContentType = "application/x-www-form-urlencoded";
      request.ContentLength = data.Length;

      request.Headers.Add("Key", key);
      request.Headers.Add("Sign", ByteArrayToString(hashMaker.ComputeHash(data)).ToLower());
      var reqStream = request.GetRequestStream();
      reqStream.Write(data, 0, data.Length);
      reqStream.Close();
      return new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
    }
  }
}