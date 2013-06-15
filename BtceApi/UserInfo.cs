using Newtonsoft.Json.Linq;

namespace BtcE
{
    public class UserInfo
    {
        public Funds Funds { get; private set; }
        public Rights Rights { get; private set; }
        public int TransactionCount { get; private set; }
        public int OpenOrders { get; private set; }
        public int ServerTime { get; private set; }

        private UserInfo(){}
        public static UserInfo ReadFromJObject(JObject o)
        {
            return new UserInfo()
            {
                Funds = Funds.ReadFromJObject(o["funds"] as JObject),
                Rights = Rights.ReadFromJObject(o["rights"] as JObject),
                TransactionCount = o.Value<int>("transaction_count"),
                OpenOrders = o.Value<int>("open_orders"),
                ServerTime = o.Value<int>("server_time")
            };
        }
    }
}
