using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class TourRequestStat
    {
        public int CountAccepted { get; }
        public int CountRejected { get; }

        public int Year { get;  }

        public TourRequestStat()
        {

        }

        public TourRequestStat(int countAccepted, int countRejected, int year)
        {
            CountAccepted = countAccepted;
            CountRejected = countRejected;
            Year = year;
        }
    }
}
