using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Repository
{
    public class GroupDriveReservationRepository : IGroupDriveReservationRepository
    {
        private const string FilePath = "../../../Resources/Data/groupdrivereservation.csv";
        private readonly Serializer<GroupDriveReservation> _serializer;
        private List<GroupDriveReservation> _groupDriveReservations;

        public GroupDriveReservationRepository()
        {
            _serializer = new Serializer<GroupDriveReservation>();
            _groupDriveReservations = _serializer.FromCSV(FilePath);
        }

        public List<GroupDriveReservation> GetAll()
        {
            return _groupDriveReservations;
        }

        public GroupDriveReservation Save(GroupDriveReservation groupDriveReservation)
        {
            groupDriveReservation.Id = NextId();
            _groupDriveReservations.Add(groupDriveReservation);
            _serializer.ToCSV(FilePath, _groupDriveReservations);
            return groupDriveReservation;
        }

        public void Delete(GroupDriveReservation groupDriveReservation)
        {
            _groupDriveReservations.Remove(_groupDriveReservations.Find(r => r.Id == groupDriveReservation.Id));
            _serializer.ToCSV(FilePath, _groupDriveReservations);
        }

        public GroupDriveReservation Update(GroupDriveReservation groupDriveReservation)
        {
            int index = _groupDriveReservations.FindIndex(r => r.Id == groupDriveReservation.Id);
            _groupDriveReservations[index] = groupDriveReservation;
            _serializer.ToCSV(FilePath, _groupDriveReservations);
            return groupDriveReservation;
        }

        private int NextId()
        {
            return _groupDriveReservations.Any() ? _groupDriveReservations.Max(r => r.Id) + 1 : 1;
        }
    }
}
