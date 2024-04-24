using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IGroupDriveReservationRepository
    {
        List<GroupDriveReservation> GetAll();
        GroupDriveReservation Save(GroupDriveReservation groupDriveReservation);
        void Delete(GroupDriveReservation groupDriveReservation);
        GroupDriveReservation Update(GroupDriveReservation groupDriveReservation);
    }
}
