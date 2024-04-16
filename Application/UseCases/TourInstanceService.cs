using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class TourInstanceService
    {
        private readonly ITourInstanceRepository _tourInstanceRepository;

        public TourInstanceService()
        {
            _tourInstanceRepository = Injector.CreateInstance<ITourInstanceRepository>();
        }

        public List<TourInstance> GetAll()
        {
            return _tourInstanceRepository.GetAll();
        }

        public List<TourInstance> GetAllByTourId(int tourId)
        {
            return _tourInstanceRepository.GetAllByTourId(tourId);
        }

        public TourInstance GetById(int tourInstanceId)
        {
            return _tourInstanceRepository.GetById(tourInstanceId);
        }

        public TourInstance GetByDateAndId(int tourId, DateTime date)
        {
            return _tourInstanceRepository.GetByDateAndId(tourId, date);
        }

        public TourInstance Save(TourInstance tourInstance)
        {
            return _tourInstanceRepository.Save(tourInstance);
        }

        public void Delete(TourInstance tourInstance)
        {
            _tourInstanceRepository.Delete(tourInstance);
        }

        public TourInstance Update(TourInstance tourInstance)
        {
            return _tourInstanceRepository.Update(tourInstance);
        }


        public bool CheckAvailability(int tourInstanceId, int numberOfGuests)
        {
            var tourInstance = _tourInstanceRepository.GetById(tourInstanceId);
            return tourInstance != null && numberOfGuests <= tourInstance.RemainingSlots;
        }

        public void ReserveSlots(int tourInstanceId, int numberOfGuests)
        {
            var tourInstance = _tourInstanceRepository.GetById(tourInstanceId);
            if (tourInstance != null && numberOfGuests <= tourInstance.RemainingSlots)
            {
                tourInstance.RemainingSlots -= numberOfGuests;
                _tourInstanceRepository.Update(tourInstance);
            }
        }
    }

}
