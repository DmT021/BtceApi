//
// https://github.com/multiprogramm/WexAPI
//

using Newtonsoft.Json.Linq;
using System;
using Wex.Utils;

namespace Wex
{
	public class UserInfo
	{
		public Funds Funds { get; private set; }
		public Rights Rights { get; private set; }
		public int TransactionCount { get; private set; } //DEPRECATED
		public int OpenOrders { get; private set; }
		public DateTime ServerTime { get; private set; }

		private UserInfo(){}
		public static UserInfo ReadFromJObject(JObject o)
		{
			return new UserInfo()
			{
				Funds = Funds.ReadFromJObject(o["funds"] as JObject),
				Rights = Rights.ReadFromJObject(o["rights"] as JObject),
				TransactionCount = o.Value<int>("transaction_count"),
				OpenOrders = o.Value<int>("open_orders"),
				ServerTime = UnixTime.ConvertToDateTime( o.Value<UInt32>("server_time") )
			};
		}
	}
}
