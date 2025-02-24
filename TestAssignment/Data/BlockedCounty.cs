using System;
using TestAssignment.Models;

namespace TestAssignment.Data
{
    public static class BlockedCounty
    {
        public static List<string> BlocledCountries = new List<string>();

        public static List<BlockedAttempt> BlockedAttempts = new List<BlockedAttempt>();

        public static List<TemporalBlock> TemporalBlocks = new List<TemporalBlock>();
        public static void Add(string item)
        {
            BlocledCountries.Add(item);
        }
        public static void Remove(string item)
        {
            BlocledCountries.Remove(item);
        }
        public static void Clear()
        {
            BlocledCountries.Clear();
        }
        public static bool IsBlocked(string item)
        {
            if(BlocledCountries.Contains(item)) return true;
            return false;
        }
        public static void AddToBlockedAttempts(string ip, string countryCode, string userAgent)
        {
            BlockedAttempt blockedAttempt = new BlockedAttempt()
            {
                CountryCode = countryCode,
                UserAgent = userAgent,
                IP = ip,
                Timestamp = DateTime.UtcNow
            };
            BlockedAttempts.Add(blockedAttempt);
        }

        public static void AddToTemporalBlocks(string countryCode, int DurationMinutes)
        {
            DurationMinutes %= 1440;
            TemporalBlocks.Add(new TemporalBlock()
            {
                CountryCode = countryCode,
                dateTime = DateTime.UtcNow.AddMinutes(DurationMinutes)
            }
            );
        }
        public static void RemomveFromTemporalBlocks()
        {
            var now  = DateTime.UtcNow;
            foreach (var item in TemporalBlocks)
            {
                if(item.dateTime <= now)
                {
                    TemporalBlocks.Remove(item);
                }
            }
           
        }
    }
}
