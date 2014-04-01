using System.Linq;
using Newtonsoft.Json.Linq;

namespace BtcE
{
    public class Depth
    {
        public OrderInfo[] Asks { get; private set; }
        public OrderInfo[] Bids { get; private set; }

        public static Depth ReadFromJObject(JObject o)
        {
            return new Depth
                {
                    Asks = o["asks"].OfType<JArray>().Select(order => OrderInfo.ReadFromJObject(order)).ToArray(),
                    Bids = o["bids"].OfType<JArray>().Select(order => OrderInfo.ReadFromJObject(order)).ToArray()
                };
        }

        public override string ToString()
        {
            return string.Format("{0} Bids:{2} & {1} Asks: {3}", Asks.Length, Bids.Length, Asks.Sum(x => x.Amount),
                                 Bids.Sum(x => x.Amount));
        }
    }
}