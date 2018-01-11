//
// https://github.com/multiprogramm/WexAPI
//

using Newtonsoft.Json.Linq;

namespace Wex
{
	public class TradeAnswer
	{
		public decimal Received { get; private set; }
		public decimal Remains { get; private set; }
		public int OrderId { get; private set; }
		public Funds Funds { get; private set; }

		public static TradeAnswer ReadFromJObject(JObject o)
		{
			if ( o == null )
				return null;

			return new TradeAnswer()
			{
				Received = o.Value<decimal>("received"),
				Remains = o.Value<decimal>("remains"),
				OrderId = o.Value<int>("order_id"),
				Funds = Funds.ReadFromJObject(o["funds"] as JObject)
			};
		}
	}
}
