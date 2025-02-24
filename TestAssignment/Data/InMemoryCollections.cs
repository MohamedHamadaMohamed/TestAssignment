using System;
using TestAssignment.Models;

namespace TestAssignment.Data
{
    public static class InMemoryCollections
    {
        public static List<BlockedCountry> BlocledCountries = new List<BlockedCountry>();

        public static List<BlockedAttempt> BlockedAttempts = new List<BlockedAttempt>();

        public static List<TemporalBlock> TemporalBlocks = new List<TemporalBlock>();
    
        public static void RemomveFromTemporalBlocks()
        {
            var now = DateTime.UtcNow;
            foreach (var item in TemporalBlocks)
            {
                if (item.dateTime <= now)
                {
                    TemporalBlocks.Remove(item);
                }
            }

        }


    }
}
