//
// https://github.com/multiprogramm/WexAPI
//

using Wex.Utils;
using Newtonsoft.Json.Linq;
using System;

namespace Wex
{
	public class Deal
	{
		public DealType Type { get; private set; }
		public decimal Price { get; private set; }
		public decimal Amount { get; private set; }
		public UInt32 Tid { get; private set; }
		public DateTime Timestamp { get; private set; }

		public static Deal ReadFromJObject(JObject o)
		{
			if (o == null)
				return null;

			return new Deal()
			{
				Type = DealTypeHelper.FromString(o.Value<string>("type")),
				Price = o.Value<decimal>("price"),
				Amount = o.Value<decimal>("amount"),
				Tid = o.Value<UInt32>("tid"),
				Timestamp = UnixTime.ConvertToDateTime(o.Value<UInt32>("timestamp"))
			};
		}
	}
}
