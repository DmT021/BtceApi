/*
 * Base for making api class for btc-e.com
 * DmT
 * 2012
 */

using System.Globalization;
using System.Threading.Tasks;
using BtcE.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace BtcE {
	public class BtceApi {
		string key;
		HMACSHA512 hashMaker;
		UInt32 nonce;
		readonly string instanseExchangeHost;
		public static string ExchangeHost = "https://btc-e.com/";
		public BtceApi( string key, string secret, string exchangeHost = null ) {
			this.key = key;
			hashMaker = new HMACSHA512( Encoding.ASCII.GetBytes( secret ) );
			nonce = UnixTime.Now;
			this.instanseExchangeHost = exchangeHost ?? ExchangeHost;
		}
		
		public UserInfo GetInfo() {return UserInfo.ReadFromJObject( QueryObject( new Dictionary<string, string>() { { "method", "getInfo" } } ) ); }
		public async Task<UserInfo> GetInfoAsync() { return UserInfo.ReadFromJObject( await QueryObjectAsync( new Dictionary<string, string>() { { "method", "getInfo" } } ) ); }

		public TransHistory GetTransHistory(int? from = null, int? count = null, int? fromId = null, int? endId = null,bool? orderAsc = null, DateTime? since = null, DateTime? end = null){
			return GetTransHistory(new BtceApiTransHistoryParams(from, count, fromId, endId, orderAsc, since, end));}
		public TransHistory GetTransHistory( BtceApiTransHistoryParams args ) { return TransHistory.ReadFromJObject( QueryObject( args.ToDictionary() ) ); }
		public async Task<TransHistory> GetTransHistoryAsync(int? from = null, int? count = null, int? fromId = null, int? endId = null,bool? orderAsc = null, DateTime? since = null, DateTime? end = null){
			return await GetTransHistoryAsync(new BtceApiTransHistoryParams(from, count, fromId, endId, orderAsc, since, end));}
		public async Task<TransHistory> GetTransHistoryAsync( BtceApiTransHistoryParams args ) { return TransHistory.ReadFromJObject( await QueryObjectAsync( args.ToDictionary() ) ); }

		public TradeHistory GetTradeHistory(int? from = null, int? count = null, int? fromId = null, int? endId = null, bool? orderAsc = null, DateTime? since = null, DateTime? end = null){
			return GetTradeHistory( new BtceApiTradeHistoryParams( from, count, fromId, endId, orderAsc, since, end ) );}
		public TradeHistory GetTradeHistory( BtceApiTradeHistoryParams args ) { return TradeHistory.ReadFromJObject( QueryObject( args.ToDictionary() ) ); }
		public async Task<TradeHistory> GetTradeHistoryAsync( int? from = null, int? count = null, int? fromId = null, int? endId = null, bool? orderAsc = null, DateTime? since = null, DateTime? end = null ) {
			return await GetTradeHistoryAsync( new BtceApiTradeHistoryParams( from, count, fromId, endId, orderAsc, since, end ) );}
		public async Task<TradeHistory> GetTradeHistoryAsync( BtceApiTradeHistoryParams args ) { return TradeHistory.ReadFromJObject( await QueryObjectAsync( args.ToDictionary() ) ); }

		public OrderList GetOrderList(int? from = null, int? count = null, int? fromId = null, int? endId = null,bool? orderAsc = null, DateTime? since = null, DateTime? end = null){
			return GetOrderList(new BtceApiOrderListParams(from, count, fromId, endId, orderAsc, since, end ) );}
		public OrderList GetOrderList( BtceApiOrderListParams args ) { return OrderList.ReadFromJObject( QueryObject( args.ToDictionary() ) ); }
		public async Task<OrderList> GetOrderListAsync( int? from = null, int? count = null, int? fromId = null, int? endId = null, bool? orderAsc = null, DateTime? since = null, DateTime? end = null ) {
			return await GetOrderListAsync( new BtceApiOrderListParams( from, count, fromId, endId, orderAsc, since, end ) );}
		public async Task<OrderList> GetOrderListAsync( BtceApiOrderListParams args ) { return OrderList.ReadFromJObject( await QueryObjectAsync( args.ToDictionary() ) ); }

		public TradeAnswer Trade(BtcePair pair, TradeType type, decimal rate, decimal amount ){ return Trade(new BtceApiTradeParams(pair,type, rate, amount));}
		public TradeAnswer Trade( BtceApiTradeParams args ) { return TradeAnswer.ReadFromJObject( QueryObject( args.ToDictionary() ) ); }
		public async Task<TradeAnswer> TradeAsync( BtcePair pair, TradeType type, decimal rate, decimal amount ) { return await TradeAsync( new BtceApiTradeParams( pair, type, rate, amount ) ); }
		public async Task<TradeAnswer> TradeAsync( BtceApiTradeParams args ) { return TradeAnswer.ReadFromJObject( await QueryObjectAsync( args.ToDictionary() ) ); }


		public CancelOrderAnswer CancelOrder( int orderId ) {
			return CancelOrderAnswer.ReadFromJObject( QueryObject( new Dictionary<string, string>() { { "method", "CancelOrder" }, { "order_id", orderId.ToString() } } ) );}
		public async Task<CancelOrderAnswer> CancelOrderAsync( int orderId ) {
			return CancelOrderAnswer.ReadFromJObject( await QueryObjectAsync( new Dictionary<string, string>() { { "method", "CancelOrder" }, { "order_id", orderId.ToString() } } ) ); }
		
		UInt32 GetNonce() { return nonce++; }
		string Query( Dictionary<string, string> args ) {
			args.Add( "nonce", GetNonce().ToString() );
			var data = Encoding.ASCII.GetBytes( Helper.BuildPostData( args ) );
			var request = _prepareQueryRequest( data );
			var reqStream = request.GetRequestStream();
			reqStream.Write( data, 0, data.Length );
			reqStream.Close();
			return new StreamReader( request.GetResponse().GetResponseStream() ).ReadToEnd();
		}
		async Task<string> QueryAsync( Dictionary<string, string> args ) {
			args.Add( "nonce", GetNonce().ToString() );
			var data = Encoding.ASCII.GetBytes( Helper.BuildPostData( args ) );
			var request = _prepareQueryRequest( data );
			var reqStream = await request.GetRequestStreamAsync();
			await reqStream.WriteAsync( data, 0, data.Length );
			reqStream.Close();
			return await new StreamReader( ( await request.GetResponseAsync() ).GetResponseStream() ).ReadToEndAsync();
		}

		static string Query( string url ) { return new StreamReader( Helper.PrepareRequest( url ).GetResponse().GetResponseStream() ).ReadToEnd();}
		static async Task<string> QueryAsync( string url ) { return await new StreamReader( ( await Helper.PrepareRequest( url ).GetResponseAsync() ).GetResponseStream() ).ReadToEndAsync();}
		private JObject QueryObject( Dictionary<string, string> args ) {
			var result = JObject.Parse( Query( args ) );
			Helper.CheckResponse( result );
			return result[ "return" ] as JObject;
		}
		private async Task<JObject> QueryObjectAsync( Dictionary<string, string> args ) {
			var result = JObject.Parse( await QueryAsync( args ) );
			Helper.CheckResponse( result );
			return result[ "return" ] as JObject;
		}
		
		private HttpWebRequest _prepareQueryRequest( byte[] data ) {
			var request = Helper.PrepareRequest( this.instanseExchangeHost + "tapi" );
			request.Method = "POST";
			request.Timeout = 15000;
			request.ContentType = "application/x-www-form-urlencoded";
			request.ContentLength = data.Length;
			request.Headers.Add( "Key", key );
			request.Headers.Add( "Sign", Helper.ByteArrayToString( hashMaker.ComputeHash( data ) ).ToLower() );
			return request;
		}
		public static Depth GetDepth( BtcePair pair ) { return _parseDepth( Query( _prepareDepthUrl( pair ) ) ); }
		public static async Task<Depth> GetDepthAsync( BtcePair pair ) { return _parseDepth( await QueryAsync( _prepareDepthUrl( pair ) ) ); }

		public static Ticker GetTicker( BtcePair pair ) { return _parseTicker( Query( _prepareTickerUrl( pair ) ) ); }
		public static async Task<Ticker> GetTickerAsync( BtcePair pair ) { return _parseTicker( await QueryAsync( _prepareTickerUrl( pair ) ) ); }

		public static TradeInfo[] GetTrades( BtcePair pair ) { return _parseTrades( Query( _prepareTradesUrl( pair ) ) ); }
		public static async Task<TradeInfo[]> GetTradesAsync( BtcePair pair ) { return _parseTrades( await QueryAsync( _prepareTradesUrl( pair ) ) ); }

		public static decimal GetFee( BtcePair pair ) { return _parseFee( Query( _prepareFeeUrl( pair ) ) ); }
		public static async Task<decimal> GetFeeAsync( BtcePair pair ) { return _parseFee( await QueryAsync( _prepareFeeUrl( pair ) ) ); }
		
		private static Depth _parseDepth( string s ) { return Depth.ReadFromJObject( JObject.Parse( s ) ); }
		private static Ticker _parseTicker( string s ) { return Ticker.ReadFromJObject( JObject.Parse( s )[ "ticker" ] as JObject ); }
		private static TradeInfo[] _parseTrades( string s ) { return JArray.Parse( s ).OfType<JObject>().Select( TradeInfo.ReadFromJObject ).ToArray(); }
		private static decimal _parseFee( string s ) { return JObject.Parse( s ).Value<decimal>( "trade" ); }
		private static string _prepareFeeUrl( BtcePair pair ) { return _prepareUrl( pair, "fee" ); }
		private static string _prepareTradesUrl( BtcePair pair ) { return _prepareUrl( pair, "trades" ); }
		private static string _prepareDepthUrl( BtcePair pair ) { return _prepareUrl( pair, "depth" ); }
		private static string _prepareTickerUrl( BtcePair pair ) { return _prepareUrl( pair, "ticker" ); }
		private static string _prepareUrl( BtcePair pair, string method ) { return string.Format( "{1}api/2/{0}/{2}", BtcePairHelper.ToString( pair ), ExchangeHost, method ); }
	}
}
