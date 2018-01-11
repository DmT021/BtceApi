//
// https://github.com/multiprogramm/WexAPI
//

using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Wex
{
	public class Funds
	{
		public Dictionary<WexCurrency, decimal> Amounts { get; private set; }

		public decimal GetAmount( WexCurrency curr )
		{
			if (Amounts == null)
				return 0m;

			decimal val;
			if (Amounts.TryGetValue(curr, out val))
				return val;

			return 0m;
		}

		public static Funds ReadFromJObject(JObject o)
		{
			if (o == null)
				return null;

			Dictionary<WexCurrency, decimal> funds_amounts = new Dictionary<WexCurrency, decimal>();
			var funds = o.Children();
			foreach (JProperty cur in funds)
			{
				string name = cur.Name;
				decimal value = o.Value<decimal>(name);
				funds_amounts.Add( WexCurrencyHelper.FromString(name), value );
			}

			return new Funds() { Amounts = funds_amounts };
		}
	};

}
