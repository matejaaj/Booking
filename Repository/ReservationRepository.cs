using BookingApp.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class ReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/reservation.csv";
        private readonly Serializer<Reservation> _serializer;
        private List<Reservation> _reservation;

        public ReservationRepository()
        {
            _serializer = new Serializer<Reservation>();
            _reservation = _serializer.FromCSV(FilePath);
        }

        public List<Reservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Reservation Save(Reservation reservation)
        {
            reservation.ReservationId = NextId();
            _reservation = _serializer.FromCSV(FilePath);
            _reservation.Add(reservation);
            _serializer.ToCSV(FilePath, _reservation);
            return reservation;
        }

        public int NextId()
        {
            _reservation = _serializer.FromCSV(FilePath);
            if (_reservation.Count < 1)
            {
                return 1;
            }
            return _reservation.Max(a => a.ReservationId) + 1;
        }

        public void Delete(Reservation reservation)
        {
            _reservation = _serializer.FromCSV(FilePath);
            Reservation founded = _reservation.Find(a => a.ReservationId == reservation.ReservationId);
            _reservation.Remove(founded);
            _serializer.ToCSV(FilePath, _reservation);
        }

        public Reservation Update(Reservation reservation)
        {
            _reservation = _serializer.FromCSV(FilePath);
            Reservation current = _reservation.Find(a => a.ReservationId == reservation.ReservationId);
            int index = _reservation.IndexOf(current);
            _reservation.Remove(current);
            _reservation.Insert(index, reservation);
            _serializer.ToCSV(FilePath, _reservation);
            return reservation;
        }

        public List<Reservation> GetByUser(User user)
        {
            _reservation = _serializer.FromCSV(FilePath);
            return _reservation.FindAll(a => a.Guest.Id == user.Id);
        }
    }

}
