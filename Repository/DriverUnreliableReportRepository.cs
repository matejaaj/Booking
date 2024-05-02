using BookingApp.Domain.Model;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Domain.RepositoryInterfaces;

namespace BookingApp.Repository
{
    public class DriverUnreliableReportRepository : IDriverUnreliableReportRepository
    {
        private const string FilePath = "../../../Resources/Data/driverUnreliableReports.csv";
        private readonly Serializer<DriverUnreliableReport> _serializer;
        private List<DriverUnreliableReport> _reports;

        public DriverUnreliableReportRepository()
        {
            _serializer = new Serializer<DriverUnreliableReport>();
            _reports = _serializer.FromCSV(FilePath);
        }

        public List<DriverUnreliableReport> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public DriverUnreliableReport GetById(int reportId)
        {
            _reports = _serializer.FromCSV(FilePath);
            return _reports.FirstOrDefault(report => report.Id == reportId);
        }

        public DriverUnreliableReport Save(DriverUnreliableReport report)
        {
            report.Id = NextId();
            _reports.Add(report);
            _serializer.ToCSV(FilePath, _reports);
            return report;
        }

        public void Delete(DriverUnreliableReport report)
        {
            DriverUnreliableReport found = _reports.Find(r => r.Id == report.Id);
            if (found != null)
            {
                _reports.Remove(found);
                _serializer.ToCSV(FilePath, _reports);
            }
        }

        public DriverUnreliableReport Update(DriverUnreliableReport report)
        {
            DriverUnreliableReport current = _reports.Find(r => r.Id == report.Id);
            if (current != null)
            {
                int index = _reports.IndexOf(current);
                _reports[index] = report;
                _serializer.ToCSV(FilePath, _reports);
            }
            return report;
        }

        public int NextId()
        {
            if (_reports.Count < 1)
            {
                return 1;
            }
            return _reports.Max(r => r.Id) + 1;
        }

        public bool ReportExists(int reservationId, int touristId, int driverId)
        {
            return _reports.Any(report => report.DriveReservationId == reservationId
                                          && report.TouristId == touristId
                                          && report.DriverId == driverId);
        }
    }
}
