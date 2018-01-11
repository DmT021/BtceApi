//
// https://github.com/multiprogramm/WexAPI
//

using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System;
using Wex.Utils;

namespace Wex
{
	public class PairInfo
	{
		public int DecimalPlaces { get; private set; }
		public decimal MinPrice { get; private set; }
		public decimal MaxPrice { get; private set; }
		public decimal MinAmount { get; private set; }
		public bool Hidden { get; private set; }
		public decimal Fee { get; private set; }

		public static PairInfo ReadFromJObject(JObject o)
		{
			if( o == null )
				return null;

			return new PairInfo()
			{
				DecimalPlaces = o.Value<int>("decimal_places"),
				MinPrice = o.Value<decimal>("min_price"),
				MaxPrice = o.Value<decimal>("max_price"),
				MinAmount = o.Value<decimal>("min_amount"),
				Hidden = ( o.Value<int>("hidden") != 0 ),
				Fee = o.Value<decimal>("fee")
			};
		}
	}

	public class InfoAnswer
	{
		public DateTime ServerTime { get; private set; }
		public Dictionary<WexPair, PairInfo> Pairs { get; private set; }

		private InfoAnswer() { }
		public static InfoAnswer ReadFromJObject(JObject o)
		{
			var pair_info_reader = new Func<JContainer, PairInfo>(
				x => PairInfo.ReadFromJObject(x as JObject)
			);

			return new InfoAnswer()
			{
				ServerTime = UnixTime.ConvertToDateTime(o.Value<UInt32>("server_time")),
				Pairs = JsonHelpers.ReadPairDict<PairInfo>(o.Value<JObject>("pairs"), pair_info_reader)
			};
		}
	}
}