using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows;

namespace BookingApp.Application.UseCases
{
    internal class SuperGuestService
    {
        private readonly ISuperGuestRepository _superGuestRepository;
        private AccommodationReservationService _accommodationReservationService;
        public SuperGuestService(ISuperGuestRepository superGuestRepository, AccommodationReservationService _accommodationReservationService)
        {
            _superGuestRepository = superGuestRepository;
            this._accommodationReservationService = _accommodationReservationService;
        }

        public List<SuperGuest> GetAll()
        {
            return _superGuestRepository.GetAll();
        }

        public SuperGuest Save(SuperGuest superGuest)
        {
            return _superGuestRepository.Save(superGuest);
        }

        public void Delete(SuperGuest superGuest)
        {
            _superGuestRepository.Delete(superGuest);
        }

        public SuperGuest Update(SuperGuest superGuest)
        {
            return _superGuestRepository.Update(superGuest);
        }

        public SuperGuest GetById(int id)
        {
            return _superGuestRepository.GetById(id);
        }
        public SuperGuest GetByGuestId(int guestId)
        {
            return _superGuestRepository.GetAll().FirstOrDefault(sg => sg.GuestId == guestId);
        }

        public void FindSuperGuests(User loggedInGuest)
        {
            SuperGuest superGuest = GetByGuestId(loggedInGuest.Id);

            if (superGuest != null)
            {
                UpdateExistingSuperGuest(superGuest, loggedInGuest);
            }
            else
            {
                CreateNewSuperGuest(loggedInGuest);
            }
        }

        private void UpdateExistingSuperGuest(SuperGuest superGuest, User Guest)
        {
            DateTime startDate = superGuest.StartDate.Date;
            DateTime endDate = superGuest.EndDate.Date;
            int totalReservations = GetTotalReservationsSinceStartDate(Guest, startDate);

            if (totalReservations >= 10)
            {
                superGuest.IsSuperGuestNextYear = true;
                Update(superGuest);
            }

            if (superGuest.EndDate.Date <= DateTime.Now.Date && superGuest.IsSuperGuestNextYear)
            {
                RenewSuperGuestStatus(superGuest);
            }
            else if (superGuest.EndDate.Date <= DateTime.Now.Date && !superGuest.IsSuperGuestNextYear)
            {
                Delete(superGuest);
            }
        }

        private void RenewSuperGuestStatus(SuperGuest superGuest)
        {
            var startDate = DateTime.Now.Date;
            var endDate = startDate.AddYears(1);
            superGuest.StartDate = startDate;
            superGuest.EndDate = endDate;
            superGuest.BonusPoints = 5;
            superGuest.IsSuperGuestNextYear = false;
            Update(superGuest);
        }


        private void CreateNewSuperGuest(User loggedInGuest)
        {
            int totalReservations = GetTotalReservationsForLastYear(loggedInGuest);

            if (totalReservations >= 10)
            {
                DateTime startDate = DateTime.Now.Date;
                DateTime endDate = startDate.AddYears(1);

                SuperGuest newSuperGuest = new SuperGuest
                {
                    GuestId = loggedInGuest.Id,
                    ReservationsNumber = totalReservations,
                    StartDate = startDate,
                    EndDate = endDate,
                    BonusPoints = 5,
                    IsSuperGuestNextYear = false
                };

                Save(newSuperGuest);
            }
        }
        private int GetTotalReservationsSinceStartDate(User loggedInGuest, DateTime startDate)
        {
            List<AccommodationReservation> reservations = _accommodationReservationService.GetByUser(loggedInGuest);
            return reservations.Count(reservation => reservation.StartDate >= startDate);
        }

        private int GetTotalReservationsForLastYear(User loggedInGuest)
        {
            DateTime oneYearAgo = DateTime.Now.AddYears(-1);
            List<AccommodationReservation> reservations = _accommodationReservationService.GetByUser(loggedInGuest);
            return reservations.Count(reservation => reservation.StartDate >= oneYearAgo);
        }

        private (DateTime, DateTime) ConvertStringToDates(string selectedDateRange)
        {
            string[] dateRangeParts = selectedDateRange.Split(" - ");
            DateTime selectedStartDate = DateTime.ParseExact(dateRangeParts[0], "dd.MM.yyyy", CultureInfo.InvariantCulture);
            DateTime selectedEndDate = DateTime.ParseExact(dateRangeParts[1], "dd.MM.yyyy", CultureInfo.InvariantCulture);
            return (selectedStartDate, selectedEndDate);
        }

        public void ConfirmReservation(int guestNumber, int accommodationId, int guestId, string selectedDateRange, int days, int maxCapacity)
        {
            if (guestNumber > maxCapacity || guestNumber < 1)
            {
                MessageBox.Show($"Number of people cannot exceed {maxCapacity}. Please enter a valid number.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("Reservation successfully confirmed!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            DateTime startDate, endDate;
            (startDate, endDate) = ConvertStringToDates(selectedDateRange);

            AccommodationReservation reservation = new AccommodationReservation(startDate, endDate, days, guestNumber, accommodationId, guestId, false, false, false);
            _accommodationReservationService.Save(reservation);

            UpdateSuperGuest(guestId);
        }

        public void UpdateSuperGuest(int guestId)
        {
            SuperGuest superGuest = GetByGuestId(guestId);
            if (superGuest != null)
            {
                superGuest.ReservationsNumber++;
                if (superGuest.BonusPoints > 0)
                {
                    superGuest.BonusPoints--;
                }
                _superGuestRepository.Update(superGuest);
            }
        }
    }
}
