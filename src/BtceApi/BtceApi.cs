/*
 * Base for making api class for btc-e.com
 * DmT
 * kasthack
 * 2012-2013
 */

using BtcE.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BtcE {
    public class BtceApi {
        readonly string _key;
        readonly HMACSHA512 hashMaker;
        UInt32 _nonce;
        readonly string _instanseExchangeHost;
        public static string ExchangeHost = "https://btc-e.com/";

        private static Lazy<Dictionary<string, string>> _InfoQueryArgs = new Lazy<Dictionary<string, string>>( () => new Dictionary<string, string>() { { "method", "getInfo" } } );
        public BtceApi( string key, string secret, string exchangeHost = null ) {
            this._key = key;
            hashMaker = new HMACSHA512( Encoding.ASCII.GetBytes( secret ) );
            this._nonce = UnixTime.Now;
            this._instanseExchangeHost = exchangeHost ?? ExchangeHost;
        }

        public UserInfo GetInfo() { var v = this.GetInfoAsync(); v.Wait(); return v.Result; }
        public TransHistory GetTransHistory( int? from = null, int? count = null, int? fromId = null, int? endId = null, bool? orderAsc = null, DateTime? since = null, DateTime? end = null ) {
            return GetTransHistory( new BtceApiTransHistoryParams( from, count, fromId, endId, orderAsc, since, end ) );}
        public TransHistory GetTransHistory(BtceApiTransHistoryParams args) { var v = this.GetTransHistoryAsync( args ); v.Wait(); return v.Result; }
        public TradeHistory GetTradeHistory( int? from = null, int? count = null, int? fromId = null, int? endId = null, bool? orderAsc = null, DateTime? since = null, DateTime? end = null ) {
            return GetTradeHistory( new BtceApiTradeHistoryParams( from, count, fromId, endId, orderAsc, since, end ) );}
        public TradeHistory GetTradeHistory( BtceApiTradeHistoryParams args ) { var v = this.GetTradeHistoryAsync( args ); v.Wait(); return v.Result; }
        public OrderList GetOrderList( int? from = null, int? count = null, int? fromId = null, int? endId = null, bool? orderAsc = null, DateTime? since = null, DateTime? end = null ) {
            return GetOrderList( new BtceApiOrderListParams( from, count, fromId, endId, orderAsc, since, end ) );}
        public OrderList GetOrderList( BtceApiOrderListParams args ) { var v = this.GetOrderListAsync( args ); v.Wait(); return v.Result; }
        public TradeAnswer Trade( BtcePair pair, TradeType type, decimal rate, decimal amount ) { return Trade( new BtceApiTradeParams( pair, type, rate, amount ) ); }
        public TradeAnswer Trade( BtceApiTradeParams args ) { var v = this.TradeAsync( args ); v.Wait(); return v.Result; }
        public CancelOrderAnswer CancelOrder( int orderId ) { var v = this.CancelOrderAsync( orderId ); v.Wait(); return v.Result; }

        public async Task<UserInfo> GetInfoAsync() { return UserInfo.ReadFromJObject( await QueryObjectAsync( _InfoQueryArgs.Value ) ); }
        public async Task<TradeHistory> GetTradeHistoryAsync( int? from = null, int? count = null, int? fromId = null, int? endId = null, bool? orderAsc = null, DateTime? since = null, DateTime? end = null ) {
            return await GetTradeHistoryAsync( new BtceApiTradeHistoryParams( from, count, fromId, endId, orderAsc, since, end ) );}
        public async Task<TradeHistory> GetTradeHistoryAsync( BtceApiTradeHistoryParams args ) { return TradeHistory.ReadFromJObject( await QueryObjectAsync( args.ToDictionary() ) ); }
        public async Task<OrderList> GetOrderListAsync( int? from = null, int? count = null, int? fromId = null, int? endId = null, bool? orderAsc = null, DateTime? since = null, DateTime? end = null ) {
            return await GetOrderListAsync( new BtceApiOrderListParams( from, count, fromId, endId, orderAsc, since, end ) );}
        public async Task<OrderList> GetOrderListAsync( BtceApiOrderListParams args ) { return OrderList.ReadFromJObject( await QueryObjectAsync( args.ToDictionary() ) ); }
        public async Task<TradeAnswer> TradeAsync( BtcePair pair, TradeType type, decimal rate, decimal amount ) { return await TradeAsync( new BtceApiTradeParams( pair, type, rate, amount ) ); }
        public async Task<TradeAnswer> TradeAsync( BtceApiTradeParams args ) { return TradeAnswer.ReadFromJObject( await QueryObjectAsync( args.ToDictionary() ) ); }
        public async Task<TransHistory> GetTransHistoryAsync( int? from = null, int? count = null, int? fromId = null, int? endId = null, bool? orderAsc = null, DateTime? since = null, DateTime? end = null ) {
            return await GetTransHistoryAsync( new BtceApiTransHistoryParams( from, count, fromId, endId, orderAsc, since, end ) );}
        public async Task<TransHistory> GetTransHistoryAsync( BtceApiTransHistoryParams args ) { return TransHistory.ReadFromJObject( await QueryObjectAsync( args.ToDictionary() ) ); }
        public async Task<CancelOrderAnswer> CancelOrderAsync( int orderId ) { return CancelOrderAnswer.ReadFromJObject( await QueryObjectAsync( _cCancelOrderArgs( orderId ) ) ); }
        

        public static Depth GetDepth(BtcePair pair) { var v = GetDepthAsync(pair); v.Wait(); return v.Result; }
        public static decimal GetFee( BtcePair pair ) { var v = GetFeeAsync( pair ); v.Wait(); return v.Result;}
        public static Ticker GetTicker( BtcePair pair ) { var v = GetTickerAsync( pair ); v.Wait(); return v.Result; }
        public static TradeInfo[] GetTrades( BtcePair pair ) { var v = GetTradesAsync( pair ); v.Wait(); return v.Result; }
        
        public static async Task<Depth> GetDepthAsync( BtcePair pair ) { return Depth.ReadFromJObject( JObject.Parse( await QueryAsync( _prepareUrl( pair, "depth" ) ) ) ); }
        public static async Task<decimal> GetFeeAsync( BtcePair pair ) { return JObject.Parse( await QueryAsync( _prepareUrl( pair, "fee" ) ) ).Value<decimal>( "trade" ); }
        public static async Task<Ticker> GetTickerAsync( BtcePair pair ) { return Ticker.ReadFromJObject( JObject.Parse( await QueryAsync( _prepareUrl( pair, "ticker" ) ) )[ "ticker" ] as JObject ); }
        public static async Task<TradeInfo[]> GetTradesAsync( BtcePair pair ) { return JArray.Parse( await QueryAsync( _prepareUrl( pair, "trades" ) ) ).OfType<JObject>().Select( TradeInfo.ReadFromJObject ).ToArray(); }


        UInt32 GetNonce() { return this._nonce++; }

        private async Task<string> QueryAsync( Dictionary<string, string> args ) {
            var data = _preparePostData( args );
            var request = _prepareQueryRequest( data );
            var reqStream = await request.GetRequestStreamAsync();
            await reqStream.WriteAsync( data, 0, data.Length );
            reqStream.Close();
            return await new StreamReader( ( await request.GetResponseAsync() ).GetResponseStream() ).ReadToEndAsync();
        }
        private async Task<JObject> QueryObjectAsync( Dictionary<string, string> args ) { return _parseQueryObject( await QueryAsync( args ) ); }
        private static async Task<string> QueryAsync( string url ) { return await new StreamReader( ( await Helper.PrepareRequest( url ).GetResponseAsync() ).GetResponseStream() ).ReadToEndAsync(); }

        private JObject _parseQueryObject( string s ) {
            var result = JObject.Parse( s );
            if ( result.Value<int>( "success" ) == 0 )
                throw new Exception( result.Value<string>( "error" ) );
            return result[ "return" ] as JObject;
        }
        private HttpWebRequest _prepareQueryRequest( byte[] data ) {
            var request = Helper.PrepareRequest( this._instanseExchangeHost + "tapi" );
            request.Method = "POST";
            request.Timeout = 15000;
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            request.Headers.Add( "Key", this._key );
            request.Headers.Add( "Sign", Helper.ByteArrayToString( hashMaker.ComputeHash( data ) ).ToLower() );
            return request;
        }

        private static string _prepareUrl( BtcePair pair, string method ) { return string.Format( "{1}api/2/{0}/{2}", BtcePairHelper.ToString( pair ), ExchangeHost, method ); }
        private byte[] _preparePostData( Dictionary<string, string> args ) { args.Add( "nonce", GetNonce().ToString() ); return Encoding.ASCII.GetBytes( Helper.BuildPostData( args ) ); }
        private static Dictionary<string, string> _cCancelOrderArgs( int orderId ) { return new Dictionary<string, string>() { { "method", "CancelOrder" }, { "order_id", orderId.ToString( Helper.InvariantCulture ) } }; }
    }
}
