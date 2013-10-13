using Newtonsoft.Json.Linq;
namespace BtcE {
	public class Rights {
		public bool Info { get; private set; }
		public bool Trade { get; private set; }
		public static Rights ReadFromJObject( JObject o ) {
			return o == null ? null : new Rights() {
				Info = o.Value<int>( "info" ) == 1,
				Trade = o.Value<int>( "trade" ) == 1
			};
		}
	}
}
