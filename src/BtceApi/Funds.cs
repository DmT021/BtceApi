using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using System.Linq;
namespace BtcE {
	public class Funds {
		private Dictionary<string, decimal> AllValues;
		public decimal GetFund( BtceCurrency cur ) { return ( GetFund( cur.ToString() ) ); }
		public decimal GetFund( string cur ) { return ( AllValues[ cur.ToLowerInvariant() ] ); }
		public string[] GetFundValues() { return AllValues.Keys.ToArray(); }
		public decimal Usd { get { return AllValues[ "usd" ]; } }
		public decimal Btc { get { return AllValues[ "btc" ]; } }
		public decimal Ltc { get { return AllValues[ "ltc" ]; } }
		public decimal Nmc { get { return AllValues[ "nmc" ]; } }
		public decimal Rur { get { return AllValues[ "rur" ]; } }
		public decimal Ftc { get { return AllValues[ "ftc" ]; } }
		public decimal Nvc { get { return AllValues[ "nvc" ]; } }
		public decimal Eur { get { return AllValues[ "eur" ]; } }
		public decimal Trc { get { return AllValues[ "trc" ]; } }
		public decimal Ppc { get { return AllValues[ "ppc" ]; } }
		public static Funds ReadFromJObject( JObject o ) {
			return o == null ? null : new Funds() { AllValues = ( ( IDictionary<string, JToken> ) o ).ToDictionary( a => a.Key, a => ( decimal ) a.Value ) };
		}
	}
}
