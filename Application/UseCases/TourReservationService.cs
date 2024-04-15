using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class TourReservationService
    {
        private readonly ITourReservationRepository _tourReservationRepository;

        public TourReservationService()
        {
            _tourReservationRepository = Injector.CreateInstance<ITourReservationRepository>();
        }

        public List<TourReservation> GetAll()
        {
            return _tourReservationRepository.GetAll();
        }

        public List<TourReservation> GetAllByTourInstanceId(int tourInstanceId)
        {
            return _tourReservationRepository.GetAllByTourInstanceId(tourInstanceId);
        }

        public List<TourReservation> GetAllByUserId(int userId)
        {
            return _tourReservationRepository.GetAllByUserId(userId);
        }

        public TourReservation Save(TourReservation tourReservation)
        {
            return _tourReservationRepository.Save(tourReservation);
        }

        public void Delete(TourReservation tourReservation)
        {
            _tourReservationRepository.Delete(tourReservation);
        }

        public TourReservation Update(TourReservation tourReservation)
        {
            return _tourReservationRepository.Update(tourReservation);
        }
    }

}
