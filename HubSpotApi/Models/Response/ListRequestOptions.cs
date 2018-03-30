using System;
using System.Collections.Generic;

namespace HubSpotApi
{
    /// <summary>
    /// Options used when querying for lists of items.
    /// </summary>
    public class ListRequestOptions
    {
        private int _limit = 20;

        public int Limit
        {
            get => _limit;
            set
            {
                if (value < 1 || value > 100)
                {
                    throw new ArgumentException(
                        $"Number of items to return must be a positive ingeteger greater than 0, and less than 100 - you provided {value}");
                }
                _limit = value;
            }
        }

        public long? timeOffset { get; set; } = null;

        public List<string> PropertiesToInclude { get; set; } = new List<string>();
    }
}


