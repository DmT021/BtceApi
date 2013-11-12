using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BtcE
{
    public class OrderInfo
    {
        public decimal Price { get; private set; }
        public decimal Amount { get; private set; }
        public static OrderInfo ReadFromJObject(JArray o)
        {
            if (o == null)
                return null;
            return new OrderInfo()
            {
                Price = o.Value<decimal>(0),
                Amount = o.Value<decimal>(1),
            };
        }
    }
}
