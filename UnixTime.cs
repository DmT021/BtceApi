using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BtcE.Utils
{
    public static class UnixTime
    {
        static DateTime unixEpoch = new DateTime(1970, 1, 1);

        public static UInt32 Now
        {
            get
            {
                return GetFromDateTime(DateTime.UtcNow);
            }
        }

        public static UInt32 GetFromDateTime(DateTime d)
        {
            var dif = d - unixEpoch;
            return (UInt32)dif.TotalSeconds;
        }

        public static DateTime ConvertToDateTime(UInt32 unixtime)
        {
            return unixEpoch.AddSeconds(unixtime);
        }
    }
}
