using BookingApp.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.Model;

namespace BookingApp.DTO.Factories
{
    public class TourRequestDTOFactory
    {
        private readonly LocationService _locationService;
        private readonly LanguageService _languageService;
        private readonly PrivateTourGuestService _tourGuestService;
        private readonly TourRequestSegmentService _tourSegmentService;

        public TourRequestDTOFactory(LocationService location, LanguageService language, PrivateTourGuestService guest, TourRequestSegmentService tourSegmentService)
        {
            _locationService = location;
            _languageService = language;
            _tourGuestService = guest;
            _tourSegmentService = tourSegmentService;
        }

        public List<TourRequestDTO> CreateSimpleTourDTOs(List<TourRequest> requests)
        {
            return requests.Select(request =>
            {
                var segment = _tourSegmentService.GetByRequestId(request.Id);
                return CreateTourRequestDTO(segment);
            }).ToList();
        }

        public List<TourRequestDTO> GetRequestDTOs(List<TourRequestSegment> requests)
        {
            List<TourRequestDTO> dtos = new List<TourRequestDTO>();
            foreach(TourRequestSegment segment in requests)
            {
                dtos.Add(CreateTourRequestDTO(segment));
            }
            return dtos;
        }

        public List<ComplexTourRequestDTO> CreateComplexTourDTOs(List<TourRequest> requests)
        {
            List<ComplexTourRequestDTO> complexDtos = new List<ComplexTourRequestDTO>();
            foreach (var request in requests)
            {
                var segments = _tourSegmentService.GetAllByRequestId(request.Id);
                var tourRequestDtos = segments.Select(segment => CreateTourRequestDTO(segment)).ToList();

                var complexDto = new ComplexTourRequestDTO
                {
                    TourSegments = tourRequestDtos,
                    Status = GetStatusDescription(request.IsAccepted)
                };

                complexDtos.Add(complexDto);
            }
            return complexDtos;
        }
        private TourRequestDTO CreateTourRequestDTO(TourRequestSegment segment)
        {
            return new TourRequestDTO
            {
                Id = segment.Id,
                Description = segment.Description,
                LocationId = segment.LocationId,
                LanguageId = segment.LanguageId,
                Capacity = segment.Capacity,
                FromDate = segment.FromDate,
                ToDate = segment.ToDate,
                AcceptedDate = segment.IsAccepted == TourRequestStatus.ACCEPTED ? segment.AcceptedDate : DateTime.MinValue, 
                TourRequestId = segment.TourRequestId,
                Location = _locationService.GetLocationById(segment.LocationId).City + " " + _locationService.GetLocationById(segment.LocationId).Country,
                Language = _languageService.GetById(segment.LanguageId).Name,
                Guests = _tourGuestService.GetAllByTourRequestSegmentId(segment.Id),
                Status = GetStatusDescription(segment.IsAccepted)
            };
        }


        private string GetStatusDescription(TourRequestStatus status)
        {
            switch (status)
            {
                case TourRequestStatus.PENDING:
                    return TranslationSource.Instance["PendingStatus"];
                case TourRequestStatus.ACCEPTED:
                    return TranslationSource.Instance["AcceptedStatus"];
                case TourRequestStatus.CANCELED:
                    return TranslationSource.Instance["CanceledStatus"];
                default:
                    return TranslationSource.Instance["UnknownStatus"];
            }
        }
    }
}
