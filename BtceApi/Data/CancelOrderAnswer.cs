using Newtonsoft.Json.Linq;

namespace BtcE.Data
{
  public class CancelOrderAnswer
  {
    private CancelOrderAnswer()
    {
    }

    public Funds Funds { get; private set; }
    public int OrderId { get; private set; }

    public static CancelOrderAnswer ReadFromJObject(JObject o)
    {
      if (o == null)
        return null;

      return new CancelOrderAnswer
      {
        Funds = Funds.ReadFromJObject(o["funds"] as JObject),
        OrderId = o.Value<int>("order_id")
      };
    }
  }
}