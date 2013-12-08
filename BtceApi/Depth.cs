namespace BtcE
{
  using System.Linq;
  using Newtonsoft.Json.Linq;

  public class Depth
  {
    public OrderInfo[] Asks { get; private set; }
    public OrderInfo[] Bids { get; private set; }

    public static Depth ReadFromJObject(JObject o)
    {
      return new Depth()
      {
        Asks = o["asks"].OfType<JArray>().Select(order => OrderInfo.ReadFromJObject(order as JArray)).ToArray(),
        Bids = o["bids"].OfType<JArray>().Select(order => OrderInfo.ReadFromJObject(order as JArray)).ToArray()
      };
    }

    public override string ToString()
    {
      return string.Format("{0} Bids:{2} & {1} Asks: {3}", this.Asks.Length, this.Bids.Length, this.Asks.Sum(x => x.Amount), this.Bids.Sum(x => x.Amount));
    }
  }
}