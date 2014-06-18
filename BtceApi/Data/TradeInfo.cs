using System;
using BtcE.Utils;
using Newtonsoft.Json.Linq;

namespace BtcE.Data
{
  public class TradeInfo
  {
    public decimal Amount { get; private set; }
    public decimal Price { get; private set; }
    public uint Tid { get; private set; }
    public TradeInfoType Type { get; private set; }
    public uint Timestamp { get; private set; }

    [Obsolete("Not used anymore", false)]
    public BtcePair CurrencyPair { get; private set; }

    [System.Xml.Serialization.XmlIgnore]
    [System.Runtime.Serialization.IgnoreDataMember]
    public DateTime Date { get { return UnixTime.ConvertToDateTime(Timestamp); } }

    [Obsolete("Not used anymore", false)]
    public BtceCurrency Item { get; private set; }

    [Obsolete("Not used anymore", false)]
    public BtceCurrency PriceCurrency { get; private set; }

    [Obsolete("Not used anymore", false)]
    public static TradeInfo ReadFromJObject(JObject o)
    {
      if (o == null)
        return null;

      return new TradeInfo
          {
            Amount = o.Value<decimal>("amount"),
            Price = o.Value<decimal>("price"),
            Timestamp = o.Value<uint>("date"),
            Item = BtceCurrencyHelper.FromString(o.Value<string>("item")),
            PriceCurrency = BtceCurrencyHelper.FromString(o.Value<string>("price_currency")),
            Tid = o.Value<uint>("tid"),
            Type = TradeInfoTypeHelper.FromString(o.Value<string>("trade_type")),
            CurrencyPair =
                BtcePairHelper.FromString(o.Value<string>("item") + "_" + o.Value<string>("price_currency"))
          };
    }

    public static TradeInfo ReadFromJObjectV3(JObject o)
    {
      if (o == null)
        return null;

      return new TradeInfo
          {
            Amount = o.Value<decimal>("amount"),
            Price = o.Value<decimal>("price"),
            Timestamp = o.Value<uint>("timestamp"),
            Tid = o.Value<uint>("tid"),
            Type = TradeInfoTypeHelper.FromString(o.Value<string>("type"))
          };
    }

    public override string ToString()
    {
      return string.Format(
          "{0} [{1}] {2}x{3} id:{4}",
          Timestamp,
          Type,
          Price,
          Amount,
          Tid);
    }
  }
}