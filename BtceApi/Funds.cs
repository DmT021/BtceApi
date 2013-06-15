using Newtonsoft.Json.Linq;

namespace BtcE
{
	public class Funds
	{
		public decimal Usd { get; private set; }
		public decimal Btc { get; private set; }
		public decimal Sc { get; private set; }
		public decimal Ltc { get; private set; }
		public decimal Ruc { get; private set; }
		public decimal Nmc { get; private set; }
		public decimal Rur { get; private set; }
		public static Funds ReadFromJObject(JObject o) {
			if ( o == null )
				return null;
			return new Funds() {
				Usd = o.Value<decimal>("usd"),
				Btc = o.Value<decimal>("btc"),
				Sc = o.Value<decimal>("sc"),
				Ltc = o.Value<decimal>("ltc"),
				Ruc = o.Value<decimal>("ruc"),
				Nmc = o.Value<decimal>("nmc"),
				Rur = o.Value<decimal>("rur")
			};
		}
	};

}
