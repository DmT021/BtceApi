using Newtonsoft.Json.Linq;

namespace BtcE
{
	public class CancelOrderAnswer
	{
		public int OrderId { get; private set; }
		public Funds Funds { get; private set; }

		private CancelOrderAnswer() {}
		public static CancelOrderAnswer ReadFromJObject(JObject o) {
			return new CancelOrderAnswer() {
				Funds = Funds.ReadFromJObject(o["funds"] as JObject),
				OrderId = o.Value<int>("order_id")
			};
		}
	}
}
