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

    public class TransHistory
    {
        public Dictionary<int, Transaction> List { get; private set; }
        public static TransHistory ReadFromJObject(JObject o)
        {
            var r = new TransHistory();
            r.List = new Dictionary<int, Transaction>();
            foreach (var item in o)
            {
                var transId = int.Parse(item.Key);
                var trans = Transaction.ReadFromJObject(item.Value as JObject);
                r.List.Add(transId, trans);
            }

            return r;
        }
    }
}
