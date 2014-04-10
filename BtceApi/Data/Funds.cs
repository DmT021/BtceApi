using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace BtcE.Data
{
  public class Funds
  {
    private Dictionary<string, decimal> _allValues;

    public decimal this[BtceCurrency btcCurrency]
    {
      get {return GetFund(btcCurrency); }
    }

    public decimal Btc
    {
      get { return _allValues["btc"]; }
    }

    public decimal Eur
    {
      get { return _allValues["eur"]; }
    }

    public decimal Ftc
    {
      get { return _allValues["ftc"]; }
    }

    public decimal Ltc
    {
      get { return _allValues["ltc"]; }
    }

    public decimal Nmc
    {
      get { return _allValues["nmc"]; }
    }

    public decimal Nvc
    {
      get { return _allValues["nvc"]; }
    }

    public decimal Ppc
    {
      get { return _allValues["ppc"]; }
    }

    public decimal Rur
    {
      get { return _allValues["rur"]; }
    }

    public decimal Trc
    {
      get { return _allValues["trc"]; }
    }

    public decimal Usd
    {
      get { return _allValues["usd"]; }
    }

    public decimal Xpm
    {
      get { return _allValues["xpm"]; }
    }

    public static Funds ReadFromJObject(JObject o)
    {
      if (o == null)
        return null;
      return new Funds
      {
        _allValues = ((IDictionary<string, JToken>)o).ToDictionary(a => a.Key, a => (decimal)a.Value),
      };
    }

    public decimal GetFund(BtceCurrency cur)
    {
      return (GetFund(cur.ToString()));
    }

    public decimal GetFund(string cur)
    {
      return (_allValues[cur.ToLowerInvariant()]);
    }

    public string[] GetFundValues()
    {
      return _allValues.Keys.ToArray();
    }

    public override string ToString()
    {
      return new { Btc, Eur, Rur, Usd, Ltc, Ppc, Nmc, Nvc, Trc, Xpm, Ftc }.ToString();
    }
  }
}
