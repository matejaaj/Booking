using BookingApp.Domain.Model;
using BookingApp.Domain.Model.BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BookingApp.Application.UseCases
{
    public class TourInstanceService
    {
        private readonly ITourInstanceRepository _tourInstanceRepository;
        private readonly TourReservationService _tourReservationService;
        private readonly VoucherService _voucherService;
        private readonly TourGuestService _tourGuestService;

        public TourInstanceService()
        {
            _tourInstanceRepository = Injector.CreateInstance<ITourInstanceRepository>();
            _tourReservationService = new TourReservationService();
            _voucherService = new VoucherService();
        }

        public TourInstanceService(ITourInstanceRepository tourInstance, TourReservationService tourReservation, VoucherService voucher, TourGuestService tourGuest)
        {
            _tourInstanceRepository = tourInstance;
            _tourReservationService = tourReservation;
            _voucherService = voucher;
            _tourGuestService = tourGuest;
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

        public void CancelTour(TourInstance SelectedInstance)
        {
            IssueVouchersToTourParticipants(SelectedInstance);
            Delete(SelectedInstance);
        }

        private void IssueVouchersToTourParticipants(TourInstance SelectedInstance)
        {
            List<TourReservation> allToursReservations = _tourReservationService.GetAll();
            foreach (var tourReservation in allToursReservations)
            {
                if (tourReservation.TourInstanceId == SelectedInstance.Id)
                {
                    IssueVoucher(tourReservation.UserId);
                }
            }
        }
        private void IssueVoucher(int userId)
        {
            Voucher voucher = new Voucher(userId, DateTime.Now.AddYears(1));
            _voucherService.Save(voucher);
        }

        public void FinishTour(int id)
        {
            var finishedTour = GetById(id);
            finishedTour.IsCompleted = true;
            Update(finishedTour);
        }

        public void UpdateCheckpoint(int id, string checkpoint)
        {
            var tour = GetById(id);
            tour.CurrentCheckpoint = checkpoint;
            Update(tour);
        }

        public (int TouristsUnder18, int TouristsBetween18And50, int TouristsOver50) GetStatistics(int tourId)
        {
            var guests = GetGuestsForTour(tourId);

            int touristsUnder18 = CountGuestsByAge(guests, age => age < 18);
            int touristsBetween18And50 = CountGuestsByAge(guests, age => age >= 18 && age < 50);
            int touristsOver50 = CountGuestsByAge(guests, age => age >= 50);

            return (touristsUnder18, touristsBetween18And50, touristsOver50);
        }

        private List<TourGuest> GetGuestsForTour(int tourId)
        {
            var completedTourInstances = GetAll()
                .Where(instance => instance.TourId == tourId && instance.IsCompleted)
                .Select(instance => instance.Id);

            var guestsForTour = _tourGuestService.GetAll()
                .Where(guest => completedTourInstances.Contains(guest.TourReservationId))
                .ToList();

            return guestsForTour;
        }

        private int CountGuestsByAge(IEnumerable<TourGuest> guests, Func<int, bool> agePredicate)
        {
            return guests.Count(guest => agePredicate(guest.Age));
        }

        public TourInstance GetCurrentTourInstance(int tourId)
        {
            var todayDate = DateTime.Now.Date;
            var tourInstance = GetAll()
                .FirstOrDefault(instance => instance.TourId == tourId &&
                                            instance.StartTime.Date == todayDate &&
                                            !instance.IsCompleted);
            return tourInstance;
        }

        public int GetEarliestYear()
        {
            int earliest = DateTime.Now.Year;

            foreach(var instance in GetAll())
            {
                if(instance.StartTime.Year < earliest)
                    earliest = instance.StartTime.Year;
            }
            return earliest;
        }
    }

}
