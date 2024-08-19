using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO.Factories
{
    public class TourDTOFactory
    {
        private readonly LocationService _locationService;
        private readonly LanguageService _languageService;
        private readonly ImageService _imageService;
        private readonly CheckpointService _checkpointService;
        private readonly TourInstanceService _tourInstanceService;

        public TourDTOFactory(LocationService locationService, LanguageService languageService, ImageService imageService, CheckpointService checkpointService, TourInstanceService tourInstanceService)
        {
            _locationService = locationService;
            _languageService = languageService;
            _imageService = imageService;
            _checkpointService = checkpointService;
            _tourInstanceService = tourInstanceService;
        }

        public List<TourDTO> CreateTourDTOs(List<Tour> tours)
        {
            List<TourDTO> dtos = new List<TourDTO>();

            foreach (var tour in tours)
            {
                var dto = new TourDTO
                {
                    Id = tour.Id,
                    Name = tour.Name,
                    Description = tour.Description,
                    LocationId = tour.LocationId,
                    LanguageId = tour.LanguageId,
                    MaximumCapacity = tour.MaximumCapacity,
                    DurationHours = tour.DurationHours
                };


                dto.Language = _languageService.GetById(tour.LanguageId).Name;
                Location location = _locationService.GetLocationById(tour.LocationId);
                dto.Location = location.City + ", " + location.Country;

                dto.Checkpoints = _checkpointService.GetAllByTourId(tour.Id);
                dto.Images = _imageService.GetImagesByEntityAndType(tour.Id, ImageResourceType.TOUR).Select(image => image.Path).ToList();
                dto.Dates = _tourInstanceService.GetAllByTourId(tour.Id)
                    .Where(instance => instance.StartTime > DateTime.Now)
                    .Select(instance => instance.StartTime)
                    .ToList();

                dtos.Add(dto);
            }

            return dtos;
        }
    }
}
