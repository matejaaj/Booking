using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class GroupDriveReservationService
    {
        private readonly IGroupDriveReservationRepository _groupDriveReservationRepository;

        public GroupDriveReservationService()
        {
            _groupDriveReservationRepository = Injector.CreateInstance<IGroupDriveReservationRepository>();
        }

        public GroupDriveReservationService(IGroupDriveReservationRepository groupDriveReservationRepository)
        {
            _groupDriveReservationRepository = groupDriveReservationRepository;
        }

        public List<GroupDriveReservation> GetAll()
        {
            return _groupDriveReservationRepository.GetAll();
        }

        public GroupDriveReservation Save(GroupDriveReservation groupDriveReservation)
        {
            return _groupDriveReservationRepository.Save(groupDriveReservation);
        }

        public void Delete(GroupDriveReservation groupDriveReservation)
        {
            _groupDriveReservationRepository.Delete(groupDriveReservation);
        }

        public GroupDriveReservation Update(GroupDriveReservation groupDriveReservation)
        {
            return _groupDriveReservationRepository.Update(groupDriveReservation);
        }
    }
}
