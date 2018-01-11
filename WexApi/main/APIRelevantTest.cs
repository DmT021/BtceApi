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
	public static class APIRelevantTest
	{
		/// <summary>
		/// Check that code is relevant. Error if code does not contain new pairs or currencies.
		/// </summary>
		public static void Check()
		{
			var info_json = WexPublicApi.InfoRequestWithoutParse();
			var pairs_json = info_json.Value<JObject>("pairs");

			var pairs = pairs_json.OfType<JProperty>().Select(
				x => x.Name
			);

			SortedSet<string> unknown_pairs = new SortedSet<string>();
			SortedSet<string> unknown_currencies = new SortedSet<string>();
			char[] SLPIT_CHARS = new char[] { '_' };

			foreach (string s_pair in pairs)
			{
				WexPair wex_pair = WexPairHelper.FromString(s_pair);
				if (wex_pair == WexPair.Unknown)
					unknown_pairs.Add(s_pair.ToLowerInvariant());

				string[] two_currs_arr = s_pair.Split(SLPIT_CHARS);
				if (two_currs_arr.Count() != 2)
					throw new Exception("Strange pair " + s_pair);

				foreach (string s_curr in two_currs_arr)
				{
					WexCurrency wex_curr = WexCurrencyHelper.FromString(s_curr);
					if (wex_curr == WexCurrency.Unknown)
						unknown_currencies.Add(s_curr.ToLowerInvariant());
				}
			}

			if (unknown_currencies.Count() == 0
				&& unknown_pairs.Count() == 0)
				return; // ok

			StringBuilder err_descr_builder = new StringBuilder();

			// Pairs
			{
				bool is_first = true;
				foreach (string s_pair in unknown_pairs)
				{
					if (is_first)
					{
						err_descr_builder.AppendLine("In enum WexPair need to add items:");
						is_first = false;
					}
					else
						err_descr_builder.AppendLine(",");

					err_descr_builder.Append("    ").Append(s_pair);
				}

				if (!is_first)
					err_descr_builder.AppendLine().AppendLine();
			}

			// Currencies
			{
				bool is_first = true;
				foreach (string s_curr in unknown_currencies)
				{
					if (is_first)
					{
						err_descr_builder.AppendLine("In enum WexCurrency need to add items:");
						is_first = false;
					}
					else
						err_descr_builder.AppendLine(",");

					err_descr_builder.Append("    ").Append(s_curr);
				}
			}

			throw new Exception(err_descr_builder.ToString());
		}
	}
}
