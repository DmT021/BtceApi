//
// https://github.com/multiprogramm/WexAPI
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace Wex
{
	public class DepthOrderInfo
	{
		public decimal Price { get; private set; }
		public decimal Amount { get; private set; }
		public static DepthOrderInfo ReadFromJObject(JArray o)
		{
			if ( o == null )
				return null;

			return new DepthOrderInfo()
			{
				Price = o.Value<decimal>(0),
				Amount = o.Value<decimal>(1),
			};
		}
	}

	public class DepthAnswer
	{
		public List<DepthOrderInfo> Asks { get; private set; }
		public List<DepthOrderInfo> Bids { get; private set; }

		public static DepthAnswer ReadFromJObject(JObject o)
		{
			return new DepthAnswer()
			{
				Asks = o["asks"].OfType<JArray>().Select(order => DepthOrderInfo.ReadFromJObject(order as JArray)).ToList(),
				Bids = o["bids"].OfType<JArray>().Select(order => DepthOrderInfo.ReadFromJObject(order as JArray)).ToList()
			};
		}
	}
}
