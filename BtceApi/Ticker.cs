using System;
using Newtonsoft.Json.Linq;

namespace BtcE
{
	public class Ticker
	{
		public decimal Average { get; private set; }
		public decimal Buy { get; private set; }
		public decimal High { get; private set; }
		public decimal Last { get; private set; }
		public decimal Low { get; private set; }
		public decimal Sell { get; private set; }
		public decimal Volume { get; private set; }
		public decimal VolumeCurrent { get; private set; }
		public UInt32 ServerTime { get; private set; }

		public static Ticker ReadFromJObject(JObject o)
		{
			if (o == null)
				return null;
			return new Ticker()
			{
				Average = o.Value<decimal>("avg"),
				Buy = o.Value<decimal>("buy"),
				High = o.Value<decimal>("high"),
				Last = o.Value<decimal>("last"),
				Low = o.Value<decimal>("low"),
				Sell = o.Value<decimal>("sell"),
				Volume = o.Value<decimal>("vol"),
				VolumeCurrent = o.Value<decimal>("vol_cur"),
				ServerTime = o.Value<UInt32>("server_time"),
			};
		}

		public override string ToString()
		{
			return string.Format("T:{0} P:{1} L:{2} H:{3} Avg:{4} B:{5} S:{6} V:{7} VC:{8}",
				ServerTime,
				Last,
				Low,
				High,
				Average,
				Buy,
				Sell,
				Volume,
				VolumeCurrent);
		}
	}
}