using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IDriverUnreliableReportRepository
    {
        List<DriverUnreliableReport> GetAll();
        DriverUnreliableReport GetById(int reportId);
        DriverUnreliableReport Save(DriverUnreliableReport report);
        void Delete(DriverUnreliableReport report);
        DriverUnreliableReport Update(DriverUnreliableReport report);
        int NextId();

        bool ReportExists(int reservationId, int touristId, int driverId); 
    }
}

