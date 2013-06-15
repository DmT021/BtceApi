using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
namespace BtcE
{
	public class OrderInfo
	{
		public decimal Price { get; private set; }
		public decimal Amount { get; private set; }
		public static OrderInfo ReadFromJObject(JArray o) {
			if ( o == null )
				return null;
			return new OrderInfo() {
				Price = o.Value<decimal>(0),
				Amount = o.Value<decimal>(1),
			};
		}
	}
	public class Depth
	{
		public List<OrderInfo> Asks { get; private set; }
		public List<OrderInfo> Bids { get; private set; }
		public static Depth ReadFromJObject(JObject o) {
			return new Depth() {
				Asks = o["asks"].OfType<JArray>().Select(order => OrderInfo.ReadFromJObject(order as JArray)).ToList(),
				Bids = o["bids"].OfType<JArray>().Select(order => OrderInfo.ReadFromJObject(order as JArray)).ToList()
			};
		}
	}
}
