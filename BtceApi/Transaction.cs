using Newtonsoft.Json.Linq;

namespace BtcE
{
    public class Transaction
    {
        public decimal Amount { get; private set; }
        public BtceCurrency Currency { get; private set; }
        public string Description { get; private set; }
        public int Status { get; private set; }
        public uint Timestamp { get; private set; }
        public int Type { get; private set; }

        public static Transaction ReadFromJObject(JObject o)
        {
            if (o == null)
                return null;
            return new Transaction
                {
                    Type = o.Value<int>("type"),
                    Amount = o.Value<decimal>("amount"),
                    Currency = BtceCurrencyHelper.FromString(o.Value<string>("currency")),
                    Timestamp = o.Value<uint>("timestamp"),
                    Status = o.Value<int>("status"),
                    Description = o.Value<string>("desc")
                };
        }
    }
}