using BtcE.Utils;
using System;
using System.Collections.Generic;
namespace BtcE {
	public abstract class BtceApiParams {
		public readonly string method;
		protected BtceApiParams( string method ) {
			this.method = method;
		}
		protected virtual void AddToDictionary( Dictionary<string, string> args ) { if ( method != null ) args.Add( "method", method ); }
		public virtual Dictionary<string, string> ToDictionary() {
			var args = new Dictionary<string, string>();
			this.AddToDictionary( args );
			return args;
		}
	}
	public abstract class BtceApiHistoryParams : BtceApiParams {
		public int? from = null, count = null, fromId = null, endId = null;
		public bool? orderAsc = null;
		public DateTime? since = null, end = null;
		public BtceApiHistoryParams( string p ) : base( p ) { }
		public BtceApiHistoryParams( string method, int? from = null, int? count = null, int? fromId = null, int? endId = null,
									bool? orderAsc = null, DateTime? since = null, DateTime? end = null ) : base( method ) {
			this.from = from; this.count = count; this.fromId = fromId; this.endId = endId;
			this.orderAsc = orderAsc; this.since = since; this.end = end;
		}
		protected new virtual void AddToDictionary( Dictionary<string, string> args ) {
			base.AddToDictionary( args );
			if ( from != null ) args.Add( "from", from.Value.ToString( Helper.InvariantCulture ) );
			if ( count != null ) args.Add( "count", count.Value.ToString( Helper.InvariantCulture ) );
			if ( fromId != null ) args.Add( "from_id", fromId.Value.ToString( Helper.InvariantCulture ) );
			if ( endId != null ) args.Add( "end_id", endId.Value.ToString( Helper.InvariantCulture ) );
			if ( orderAsc != null ) args.Add( "order", orderAsc.Value ? "ASC" : "DESC" );
			if ( since != null ) args.Add( "since", UnixTime.GetFromDateTime( since.Value ).ToString( Helper.InvariantCulture ) );
			if ( end != null ) args.Add( "end", UnixTime.GetFromDateTime( end.Value ).ToString( Helper.InvariantCulture ) );
		}
		public new virtual Dictionary<string, string> ToDictionary() {
			var args = new Dictionary<string, string>();
			this.AddToDictionary( args );
			return args;
		}
	}
	public class BtceApiTransHistoryParams : BtceApiHistoryParams {
		public BtceApiTransHistoryParams() : base( "TransHistory" ) { }
		public BtceApiTransHistoryParams(
			int? from = null, int? count = null, int? fromId = null,
			int? endId = null, bool? orderAsc = null, DateTime? since = null, DateTime? end = null ) :
			base( "TransHistory", from, count, fromId, endId, orderAsc, since, end ) { }
	}
	public class BtceApiTradeHistoryParams : BtceApiHistoryParams {
		public BtceApiTradeHistoryParams(
			int? from = null, int? count = null, int? fromId = null,
			int? endId = null, bool? orderAsc = null, DateTime? since = null, DateTime? end = null ) :
			base( "TradeHistory", from, count, fromId, endId, orderAsc, since, end ) { }
	}
	public class BtceApiOrderListParams : BtceApiHistoryParams {
		public bool? Active = null;
		public BtcePair? Pair;
		public BtceApiOrderListParams() : base( "OrderList" ) { }
		public BtceApiOrderListParams(
			int? from = null, int? count = null, int? fromId = null,
			int? endId = null, bool? orderAsc = null, DateTime? since = null,
			DateTime? end = null, BtcePair? pair = null, bool? active = null
			) :
			base( "OrderList", from, count, fromId, endId, orderAsc, since, end ) {
			this.Pair = pair;
			this.Active = active;
		}
		protected new virtual void AddToDictionary( Dictionary<string, string> args ) {
			base.AddToDictionary( args );
			if ( Pair != null ) args.Add( "pair", BtcePairHelper.ToString( Pair.Value ) );
			if ( Active != null ) args.Add( "active", Active.Value ? "1" : "0" );
		}
		public new virtual Dictionary<string, string> ToDictionary() {
			var args = new Dictionary<string, string>();
			this.AddToDictionary( args );
			return args;
		}
	}
	public class BtceApiTradeParams : BtceApiParams {
		public TradeType type;
		public BtcePair pair;
		public decimal rate, amount;
		public BtceApiTradeParams() : base( "Trade" ) { }
		public BtceApiTradeParams( BtcePair pair, TradeType type, decimal rate, decimal amount ) : this() {
			this.pair = pair; this.type = type; this.rate = rate; this.amount = amount;
		}
		protected new virtual void AddToDictionary( Dictionary<string, string> args ) {
			base.AddToDictionary( args );
			args.Add( "type", TradeTypeHelper.ToString( type ) );
			args.Add( "rate", Helper.DecimalToString( rate ) );
			args.Add( "amount", Helper.DecimalToString( amount ) );
		}
		public new virtual Dictionary<string, string> ToDictionary() {
			var args = new Dictionary<string, string>();
			this.AddToDictionary( args );
			return args;
		}
	}
}