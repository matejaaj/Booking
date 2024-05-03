using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases.Factories
{
    public class TourRequestFactory
    {
        private TourRequestService _tourRequestService;
        private TourRequestSegmentService _tourRequestSegmentService;
        private PrivateTourGuestService _privateTourGuestService;
        public TourRequestFactory(TourRequestService tourRequestService, TourRequestSegmentService tourRequestSegmentService, PrivateTourGuestService tourGuestService)
        {
            _tourRequestService = tourRequestService;
            _tourRequestSegmentService = tourRequestSegmentService;
            _privateTourGuestService = tourGuestService;
        }

    }
}
