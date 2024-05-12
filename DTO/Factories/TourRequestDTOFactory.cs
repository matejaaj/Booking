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

        public List<TourRequestDTO> CreateDTOs(List<TourRequest> requests)
        {
            List<TourRequestDTO> dtos = new List<TourRequestDTO>();

            foreach (var request in requests)
            {
                var segment = _tourSegmentService.GetByRequestId(request.Id);

                TourRequestDTO dto = new TourRequestDTO(segment);

                var guests = _tourGuestService.GetAllByTourRequestSegmentId(segment.Id);
                dto.Status = dto.GetStatusDescription(request.IsAccepted);
                dto.Guests = guests;
                dtos.Add(dto);

            }
            return dtos;
        }
    }
}
