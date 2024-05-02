using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class DriverUnreliableReportService
    {
        private IDriverUnreliableReportRepository _reportRepository;

        public DriverUnreliableReportService(IDriverUnreliableReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public List<DriverUnreliableReport> GetAll()
        {
            return _reportRepository.GetAll();
        }

        public DriverUnreliableReport Save(DriverUnreliableReport report)
        {
            return _reportRepository.Save(report);
        }

        public void Delete(DriverUnreliableReport report)
        {
            _reportRepository.Delete(report);
        }

        public DriverUnreliableReport Update(DriverUnreliableReport report)
        {
            return _reportRepository.Update(report);
        }

        public int NextId()
        {
            return _reportRepository.NextId();
        }

        public DriverUnreliableReport GetById(int id)
        {
            return _reportRepository.GetById(id);
        }

        public bool ReportExists(int reservationId, int touristId, int driverId)
        {
            return _reportRepository.ReportExists(reservationId, touristId, driverId);
        }
    }
}
