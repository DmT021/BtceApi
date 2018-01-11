//
// https://github.com/multiprogramm/WexAPI
//

using Newtonsoft.Json.Linq;
using System;
using Wex.Utils;

namespace Wex
{
	public class TickerAnswer
	{
		public decimal High { get; private set; }
		public decimal Low { get; private set; }
		public decimal Average { get; private set; }
		public decimal Volume { get; private set; }
		public decimal VolumeCurrent { get; private set; }
		public decimal Last { get; private set; }
		public decimal Buy { get; private set; }
		public decimal Sell { get; private set; }
		public DateTime Updated { get; private set; }

		public static TickerAnswer ReadFromJObject(JObject o)
		{
			if ( o == null )
				return null;

			return new TickerAnswer()
			{
				High = o.Value<decimal>("high"),
				Low = o.Value<decimal>("low"),
				Average = o.Value<decimal>("avg"),
				Volume = o.Value<decimal>("vol"),
				VolumeCurrent = o.Value<decimal>("vol_cur"),
				Last = o.Value<decimal>("last"),
				Buy = o.Value<decimal>("buy"),
				Sell = o.Value<decimal>("sell"),
				Updated = UnixTime.ConvertToDateTime( o.Value<UInt32>("updated") ),
			};
		}
	}
}
