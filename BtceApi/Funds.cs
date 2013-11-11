using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

//using Newtonsoft.Json.Linq;
using System.Linq;
namespace BtcE {
	public class Funds {
		private Dictionary<string, decimal> AllValues;

		public decimal GetFund( BtceCurrency cur ) {
			return ( GetFund( cur.ToString() ) );
		}

		public decimal GetFund( string cur ) {
			return ( AllValues[ cur.ToLowerInvariant() ] );
		}
		public string[] GetFundValues() {
			return AllValues.Keys.ToArray();
		}

		public decimal Usd {
			get { return AllValues[ "usd" ]; }
		}
		public decimal Btc {
			get { return AllValues[ "btc" ]; }
		}
		public decimal Ltc {
			get { return AllValues[ "ltc" ]; }
		}
		public decimal Nmc {
			get { return AllValues[ "nmc" ]; }
		}
		public decimal Rur {
			get { return AllValues[ "rur" ]; }
		}
		public decimal Ftc {
			get { return AllValues[ "ftc" ]; }
		}
		public decimal Nvc {
			get { return AllValues[ "nvc" ]; }
		}
		public decimal Eur {
			get { return AllValues[ "eur" ]; }
		}
		public decimal Trc {
			get { return AllValues[ "trc" ]; }
		}
		public decimal Ppc {
			get { return AllValues[ "ppc" ]; }
		}
		public static Funds ReadFromJObject( JObject o ) {
			if ( o == null )
				return null;
			return new Funds() {
				AllValues = ( ( IDictionary<string, JToken> ) o ).ToDictionary( a => a.Key, a => ( decimal ) a.Value )

				//Usd = o.Value<decimal>("usd"),
				//Btc = o.Value<decimal>("btc"),
				//Sc = o.Value<decimal>("sc"),
				//Ltc = o.Value<decimal>("ltc"),
				//Ruc = o.Value<decimal>("ruc"),
				//Nmc = o.Value<decimal>("nmc"),
				//Rur = o.Value<decimal>("rur")
			};
		}

		public override string ToString()
		{
			return string.Format("btc:{0} ltc:{1} ppc:{2} nmc:{3} trc:{4}", Btc, Ltc, Ppc, Nmc, Trc);
		}
	}

}
