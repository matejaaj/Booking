using System;
using System.Collections.Generic;
using System.Linq;
using BookingApp.Domain.Model;
using BookingApp.Application.UseCases;

namespace BookingApp.Application.UseCases
{
    public class TourRequestExpirationChecker
    {
        private readonly TourRequestService _tourRequestService;
        private readonly TourRequestSegmentService _tourRequestSegmentService;

        public TourRequestExpirationChecker(TourRequestService tourRequestService, TourRequestSegmentService tourRequestSegmentService)
        {
            _tourRequestService = tourRequestService;
            _tourRequestSegmentService = tourRequestSegmentService;
        }

        public void CheckAndExpireRequests()
        {
            var allSegments = _tourRequestSegmentService.GetAll();
            var allRequests = _tourRequestService.GetAll().Where(r => r.IsAccepted == TourRequestStatus.PENDING).ToList();

            foreach (var request in allRequests)
            {
                var segments = allSegments.Where(segment => segment.TourRequestId == request.Id).ToList();
                if (segments.Any())
                {
                    var earliestSegment = segments.OrderBy(segment => segment.FromDate).First();
                    TimeSpan timeUntilExpiration = earliestSegment.FromDate - DateTime.Now;

                    if (earliestSegment.FromDate < DateTime.Now || timeUntilExpiration.TotalHours < 48)
                    {

                        request.IsAccepted = TourRequestStatus.CANCELED;
                        _tourRequestService.Update(request);


                        foreach (var segment in segments)
                        {
                            segment.IsAccepted = TourRequestStatus.CANCELED;
                            _tourRequestSegmentService.Update(segment);
                        }
                    }
                }
            }
        }
    }
}