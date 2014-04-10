using System;

namespace BtcE.Utils
{
    public static class UnixTime
    {
        private static DateTime unixEpoch;

        static UnixTime()
        {
            unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        }

        public static uint Now
        {
            get { return GetFromDateTime(DateTime.UtcNow); }
        }

        public static DateTime ConvertToDateTime(uint unixtime)
        {
            return unixEpoch.AddSeconds(unixtime);
        }

        public static uint GetFromDateTime(DateTime d)
        {
            return (uint) (d - unixEpoch).TotalSeconds;
        }
    }
}