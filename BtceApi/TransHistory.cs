using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace BtcE
{
  public class TransHistory
  {
    public Dictionary<int, Transaction> List { get; private set; }

    public static TransHistory ReadFromJObject(JObject o)
    {
      if (o == null)
        return null;

      return new TransHistory
          {
            List =
                ((IDictionary<string, JToken>)o).ToDictionary(a => int.Parse(a.Key),
                                                               a =>
                                                               Transaction.ReadFromJObject(a.Value as JObject))
          };
    }
  }
}