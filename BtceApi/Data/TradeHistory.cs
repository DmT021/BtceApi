using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace BtcE.Data
{
    public class TradeHistory
    {
        public Dictionary<int, Trade> List { get; private set; }

        public static TradeHistory ReadFromJObject(JObject o)
        {
          if (o == null)
            return null;

            return new TradeHistory
                {
                    List =
                        ((IDictionary<string, JToken>) o).ToDictionary(item => int.Parse(item.Key),
                                                                       item =>
                                                                       Trade.ReadFromJObject(item.Value as JObject))
                };
        }

        public override string ToString()
        {
            return string.Format("Trades: {0}", List.Count);
        }
    }
}