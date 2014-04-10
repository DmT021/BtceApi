namespace BtcE
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
  using BtcE.Utils;
  using Newtonsoft.Json.Linq;

  /// <summary>
  /// TODO: Update summary.
  /// </summary>
  public class ApiInfo
  {
    // allows to keep information on not-yet-supported pairs
    private Dictionary<string, BtcePairInfo> _pairs;

    private Dictionary<BtcePair, BtcePairInfo> pairs = null; // lazy-eval
    public Dictionary<BtcePair, BtcePairInfo> Pairs
    {
      get
      {
        if (pairs == null)
        {
          pairs = _pairs
            .ToLookup(x => BtcePairHelper.FromString(x.Key), x => x.Value)
            // otherwise when more than one new pair is present DuplicateKeyException is thrown
            .Where(x => x.Key != BtcePair.Unknown)
            .ToDictionary(x => x.Key, x => x.First());
        }
        return pairs;
      }
    }

    public DateTime ServerTime { get; private set; }

    public Dictionary<string, BtcePairInfo> UnsupportedPairs
    {
      get
      {
        return _pairs
          .Where(x => BtcePairHelper.FromString(x.Key) == BtcePair.Unknown)
          .ToDictionary(x => x.Key, x => x.Value);
      }
    }

    public static ApiInfo ReadFromJObject(JObject o)
    {
      if (o == null)
        return null;

      return new ApiInfo
      {
        ServerTime = UnixTime.ConvertToDateTime(o.Value<uint>("server_time")),
        _pairs = ((IDictionary<string, JToken>)o.Value<JToken>("pairs"))
          .ToDictionary(a => a.Key, a => BtcePairInfo.ReadFromJObject(a.Value as JObject))
      };
    }

    public override string ToString()
    {
      var sb = new StringBuilder();
      sb.AppendFormat("ServerTime: {0}\n", ServerTime);
      var result = _pairs.Aggregate(sb, (s, pair) => s.AppendLine(pair.ToString()), s => s.ToString());

      return result;
    }
  }
}