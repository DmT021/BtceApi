// -----------------------------------------------------------------------
// <copyright file="BtceCurrencyInfo.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace BtcE
{
  using Newtonsoft.Json.Linq;

  /// <summary>
  /// TODO: Update summary.
  /// </summary>
  public class BtcePairInfo
  {
    public int DecimalPlaces { get; private set; }

    public decimal Fee { get; private set; }

    public bool IsHidden { get; private set; }

    public decimal MaxPrice { get; private set; }

    public decimal MinAmount { get; private set; }

    public decimal MinPrice { get; private set; }

    public static BtcePairInfo ReadFromJObject(JObject o)
    {
      if (o == null)
        return null;
      return new BtcePairInfo
      {
        DecimalPlaces = o.Value<int>("decimal_places"),
        MinPrice = o.Value<decimal>("min_price"),
        MaxPrice = o.Value<decimal>("max_price"),
        MinAmount = o.Value<decimal>("min_amount"),
        IsHidden = o.Value<int>("hidden") == 1,
        Fee = o.Value<decimal>("fee")
      };
    }

    public override string ToString()
    {
      return new { DecimalPlaces, Fee, MaxPrice, MinPrice, MinAmount, IsHidden }.ToString();
    }
  }
}
