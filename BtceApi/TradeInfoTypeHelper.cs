﻿using System;

namespace BtcE
{
    public static class TradeInfoTypeHelper
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