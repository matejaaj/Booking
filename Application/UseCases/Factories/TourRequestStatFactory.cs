using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model;

namespace BookingApp.Application.UseCases.Factories
{
    public class TourRequestStatFactory
    {
        private TourRequestService _tourRequestService;
        private TourRequestSegmentService _tourSegmentService;

        public TourRequestStatFactory(TourRequestService request, TourRequestSegmentService segment)
        {
            _tourRequestService = request;
            _tourSegmentService = segment;
        }

        public TourRequestStat CreateUserStatForYear(int year, int userId)
        {
            var allRequestIds = _tourRequestService.GetSimpleRequestsForUser(userId).Select(request => request.Id);
            int acceptedCount = 0;
            int rejectedCount = 0;
            foreach (var id in allRequestIds)
            {
                var segment = _tourSegmentService.GetByRequestId(id);
                if (segment.AcceptedDate.Year == year)
                {
                    if (segment.IsAccepted == TourRequestStatus.ACCEPTED) acceptedCount++;
                    if (segment.IsAccepted == TourRequestStatus.CANCELED) rejectedCount++;
                }
            }

            return new TourRequestStat(acceptedCount, rejectedCount, year);
        }

        public TourRequestStat CreateUserStatForAllYears(int userId)
        {
            var allRequestIds = _tourRequestService.GetSimpleRequestsForUser(userId).Select(request => request.Id);
            int acceptedCount = 0;
            int rejectedCount = 0;
            foreach (var id in allRequestIds)
            {
                var segment = _tourSegmentService.GetByRequestId(id);
                if (segment.IsAccepted == TourRequestStatus.ACCEPTED) acceptedCount++;
                if (segment.IsAccepted == TourRequestStatus.CANCELED) rejectedCount++;
            }

            return new TourRequestStat(acceptedCount, rejectedCount, 0); 
        }

        public int GetEarliestYear(int userId)
        {
            var allRequestIds = _tourRequestService.GetSimpleRequestsForUser(userId).Select(request => request.Id);
            var allSegments = allRequestIds.Select(id => _tourSegmentService.GetByRequestId(id)).Where(segment => segment != null).ToList();

            if (!allSegments.Any())
            {
                return DateTime.Now.Year;
            }


            var earliestYear = allSegments.Min(segment => segment.AcceptedDate.Year);

            return earliestYear;
        }

    }
}
