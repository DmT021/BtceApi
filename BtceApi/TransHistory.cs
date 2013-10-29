using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace BtcE
{
    public class Transaction
    {
        public int Type { get; private set; }
        public decimal Amount { get; private set; }
        public BtceCurrency Currency { get; private set; }
        public string Description { get; private set; }
        public int Status { get; private set; }
        public UInt32 Timestamp { get; private set; }

        public static Transaction ReadFromJObject(JObject o)
        {
            if (o == null)
                return null;
			return new Transaction()
            {
                Type = o.Value<int>("type"),
                Amount = o.Value<decimal>("amount"),
                Currency = BtceCurrencyHelper.FromString(o.Value<string>("currency")),
                Timestamp = o.Value<UInt32>("timestamp"),
                Status = o.Value<int>("status"),
                Description = o.Value<string>("desc")
            };
        }
    }

    public class TransHistory {
        public Dictionary<int, Transaction> List { get; private set; }
        public static TransHistory ReadFromJObject(JObject o)
        {
			TransHistory trans_hist = new TransHistory();
            trans_hist.List = new Dictionary<int, Transaction>();

            foreach (KeyValuePair<string, JToken> element in o)
            {
                trans_hist.List.Add(int.Parse(element.Key), Transaction.ReadFromJObject(element.Value as JObject));
            }

			return trans_hist;
        }
    }
}
