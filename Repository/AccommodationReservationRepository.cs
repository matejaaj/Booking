﻿using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class AccommodationReservationRepository : IAccommodationReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/accommodationReservations.csv";
        private readonly Serializer<AccommodationReservation> _serializer;
        private List<AccommodationReservation> _accommodationReservation;

        public AccommodationReservationRepository()
        {
            _serializer = new Serializer<AccommodationReservation>();
            _accommodationReservation = _serializer.FromCSV(FilePath);
        }

        public List<AccommodationReservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public AccommodationReservation Save(AccommodationReservation accommodationReservation)
        {
            accommodationReservation.Id = NextId();
            _accommodationReservation = _serializer.FromCSV(FilePath);
            _accommodationReservation.Add(accommodationReservation);
            _serializer.ToCSV(FilePath, _accommodationReservation);
            return accommodationReservation;
        }

        public int NextId()
        {
            _accommodationReservation = _serializer.FromCSV(FilePath);
            if (_accommodationReservation.Count < 1)
            {
                return 1;
            }
            return _accommodationReservation.Max(a => a.Id) + 1;
        }

        public void Delete(AccommodationReservation accommodationReservation)
        {
            _accommodationReservation = _serializer.FromCSV(FilePath);
            AccommodationReservation founded = _accommodationReservation.Find(a => a.Id == accommodationReservation.Id);
            _accommodationReservation.Remove(founded);
            _serializer.ToCSV(FilePath, _accommodationReservation);
        }

        public AccommodationReservation Update(AccommodationReservation accommodationReservation)
        {
            _accommodationReservation = _serializer.FromCSV(FilePath);
            AccommodationReservation current = _accommodationReservation.Find(a => a.Id == accommodationReservation.Id);
            int index = _accommodationReservation.IndexOf(current);
            _accommodationReservation.Remove(current);
            _accommodationReservation.Insert(index, accommodationReservation);
            _serializer.ToCSV(FilePath, _accommodationReservation);
            return accommodationReservation;
        }

        public List<AccommodationReservation> GetByUser(User user)
        {
            _accommodationReservation = _serializer.FromCSV(FilePath);
            return _accommodationReservation.FindAll(a => a.GuestId == user.Id);
        }

        public List<AccommodationReservation> GetByAccommodationIds(List<int> accommodationIds)
        {
            return _accommodationReservation.FindAll(a => accommodationIds.Contains(a.AccommodationId));
        }

        public List<AccommodationReservation> GetByAccommodationId(int accommodationId)
        {
            return _accommodationReservation.Where(a => a.AccommodationId == accommodationId).ToList();
        }

        public AccommodationReservation GetByReservationId(int id)
        {
            _accommodationReservation = _serializer.FromCSV(FilePath);
            return _accommodationReservation.Find(a => a.Id == id);
        }
    }

}
