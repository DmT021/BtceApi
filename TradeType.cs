using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BtcE
{
    /*public class TradeType
    {
        public static TradeType Sell = new TradeType();
        public static TradeType Buy = new TradeType();

        public static TradeType Parse(string s)
        {
            switch (s)
            {
                case "sell":
                    return Sell;
                case "buy":
                    return Buy;
                default:
                    throw new ArgumentException();
            }
        }

        private TradeType() { }

        public override string ToString()
        {
            if (this == Sell)
                return "sell";
            if (this == Buy)
                return "buy";

            throw new NotSupportedException();
        }
    }*/

    public enum TradeType
    {
        Sell,
        Buy
    }

    public class TradeTypeHelper
    {
        public static TradeType FromString(string s)
        {
            switch (s)
            {
                case "sell":
                    return TradeType.Sell;
                case "buy":
                    return TradeType.Buy;
                default:
                    throw new ArgumentException();
            }
        }

        public static string ToString(TradeType v)
        {
            if (v == TradeType.Sell)
                return "sell";
            if (v == TradeType.Buy)
                return "buy";

            throw new NotSupportedException();
        }
    }
}
