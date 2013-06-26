using Newtonsoft.Json.Linq;

namespace BtcE
{
	public class Funds
	{

        public decimal Btc { get; private set; }
        public decimal Ltc { get; private set; }
        public decimal Nmc { get; private set; }
        public decimal Nvc { get; private set; }
        public decimal Trc { get; private set; }
        public decimal Ppc { get; private set; }
        public decimal Ftc { get; private set; }
		public decimal Usd { get; private set; }
		public decimal Rur { get; private set; }
        public decimal Eur { get; private set; }

		public static Funds ReadFromJObject(JObject o) {
			if ( o == null )
				return null;
			return new Funds() {
                Btc = o.Value<decimal>("btc"),
                Ltc = o.Value<decimal>("ltc"),
                Nmc = o.Value<decimal>("ntc"),
                Nvc = o.Value<decimal>("nvc"),
                Trc = o.Value<decimal>("trc"),
                Ppc = o.Value<decimal>("ppc"),
                Ftc = o.Value<decimal>("Ftc"),
                Usd = o.Value<decimal>("Usd"),
                Rur = o.Value<decimal>("rur"),
                Eur = o.Value<decimal>("eur")
			};
		}
	};

}
