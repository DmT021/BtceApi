using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BtcE
{
    public class TradeInfoTypeHelper
    {
        public static TradeInfoType FromString(string s)
        {
            switch (s)
            {
                case "ask":
                    return TradeInfoType.Ask;
                case "bid":
                    return TradeInfoType.Bid;
                default:
                    throw new ArgumentException();
            }
        }
    }
}
