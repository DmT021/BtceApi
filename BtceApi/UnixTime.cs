using System;

namespace BtcE.Utils
{
    public static class UnixTime
    {
        private static DateTime unixEpoch = new DateTime(1970, 1, 1);

        public static UInt32 Now
        {
            get { return GetFromDateTime(DateTime.UtcNow); }
        }

        public static UInt32 GetFromDateTime(DateTime d)
        {
            TimeSpan dif = d - unixEpoch;
            return (UInt32) dif.TotalSeconds;
        }

        public static DateTime ConvertToDateTime(UInt32 unixtime)
        {
            return unixEpoch.AddSeconds(unixtime);
        }
    }
}