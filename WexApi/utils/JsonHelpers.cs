//
// https://github.com/multiprogramm/WexAPI
//

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Wex.Utils
{
	static class JsonHelpers
	{
		/// <summary>
		/// Read dictonary with keys WexPair from JSON
		/// </summary>
		/// <typeparam name="T">Dictionary value type</typeparam>
		/// <param name="o">json object</param>
		/// <param name="t_value_reader">Function for reading T from JSON</param>
		public static Dictionary<WexPair, T> ReadPairDict<T>(
			JObject o,
			Func<JContainer, T> t_value_reader )
		{
			var sel = o.OfType<JProperty>().Select(
				x => new KeyValuePair<WexPair, T>(
					WexPairHelper.FromString(x.Name),
					t_value_reader(x.Value as JContainer)
				)
			);

			return sel.ToDictionary(x => x.Key, x => x.Value);
		}
	}
}
