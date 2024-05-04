using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class TourRequestStat
    {
        public int AcceptedCount { get; }
        public int RejectedCount { get; }

        public int Year { get;  }

        public TourRequestStat()
        {

        }

        public TourRequestStat(int acceptedCount, int rejectedCount, int year)
        {
            AcceptedCount = acceptedCount;
            RejectedCount = rejectedCount;
            Year = year;
        }
    }
}
