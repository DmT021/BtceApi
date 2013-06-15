using Newtonsoft.Json.Linq;

namespace BtcE
{
	public class Rights
	{
		public bool Info { get; private set; }
		public bool Trade { get; private set; }
		public static Rights ReadFromJObject(JObject o) {
			if ( o == null )
				return null;
			return new Rights() {
				Info = o.Value<int>("info") == 1,
				Trade = o.Value<int>("trade") == 1
			};
		}
	}
}
