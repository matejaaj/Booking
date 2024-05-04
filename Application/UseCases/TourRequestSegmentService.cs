using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using Syncfusion.UI.Xaml.TreeGrid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class TourRequestSegmentService
    {
        private readonly ITourRequestSegmentRepository _tourRequestSegmentRepository;

        public TourRequestSegmentService(ITourRequestSegmentRepository tourRequestSegmentRepository)
        {
            _tourRequestSegmentRepository = tourRequestSegmentRepository;
        }

        public TourRequestSegmentService()
        {
            _tourRequestSegmentRepository = Injector.CreateInstance<ITourRequestSegmentRepository>();
        }

        public List<TourRequestSegment> GetAll()
        {
            return _tourRequestSegmentRepository.GetAll();
        }

        public TourRequestSegment GetById(int id)
        {
            return _tourRequestSegmentRepository.GetById(id);
        }

        public TourRequestSegment Save(TourRequestSegment tourRequest)
        {
            return _tourRequestSegmentRepository.Save(tourRequest);
        }

        public void Delete(TourRequestSegment tourRequest)
        {
            _tourRequestSegmentRepository.Delete(tourRequest);
        }

        public TourRequestSegment Update(TourRequestSegment tourRequest)
        {
            return _tourRequestSegmentRepository.Update(tourRequest);
        }

        public TourRequestSegment GetByRequestId(int id)
        {
            return _tourRequestSegmentRepository.GetAll().FirstOrDefault(request => request.TourRequestId == id);
        }

        public void MarkAsAccepted(TourRequestSegment tourRequest)
        {
            var request = _tourRequestSegmentRepository.GetById(tourRequest.Id);
            request.IsAccepted = TourRequestStatus.ACCEPTED;
            request.AcceptedDate = tourRequest.AcceptedDate;
            Update(request);
        }

        public int GetEarliestYear()
        {
            int earliest = DateTime.Now.Year;
            foreach(var requst in GetAll())
            {
                if(requst.FromDate.Year < earliest)
                    earliest = requst.FromDate.Year;
            }
            return earliest;
        }

        public Dictionary<string, int> GetTotalRequestsPerYear(List<object> yrs, Language language, Location location)
        {
            List<string> years = new List<string>();
            foreach(var yr in yrs)
            {
                if(!yr.Equals("AllTime"))
                    years.Add(yr.ToString());
            }

            Dictionary<string, int> requestsPerYear = new Dictionary<string, int>();

            foreach(string year in years)
            {
                requestsPerYear.Add(year, 0);
            }

            foreach(var request in GetAll())
            {
                if (language == null && location == null)
                    requestsPerYear[request.FromDate.Year.ToString()]++;

                if (location != null && language != null && request.LanguageId == language.Id && request.LocationId == location.Id)
                    requestsPerYear[request.FromDate.Year.ToString()]++;

                if (location != null && language == null && request.LocationId == location.Id)
                    requestsPerYear[request.FromDate.Year.ToString()]++;

                if (location == null && language != null && request.LanguageId == language.Id)
                    requestsPerYear[request.FromDate.Year.ToString()]++;
            }

            return requestsPerYear;
        }

        public (Dictionary<string, int>, int) GetTotalRequestsPerMonth(int year, Language language, Location location)
        {
            List<string> months = new List<string>();
            for(int i = 1; i < 13; i++)
            {
                months.Add(i.ToString());
            }
            int totalRequests = 0;

            Dictionary<string, int> requestsPerMonth = new Dictionary<string, int>();
            foreach(var month in months)
            {
                requestsPerMonth.Add(month, 0);
            }

            foreach (var request in GetAll())
            {
                if (request.FromDate.Year == year)
                {
                    bool isLanguageMatch = (language == null || request.LanguageId == language.Id);
                    bool isLocationMatch = (location == null || request.LocationId == location.Id);

                    if ((language == null && location == null) ||
                        (location != null && language != null && isLanguageMatch && isLocationMatch) ||
                        (location != null && language == null && isLocationMatch) ||
                        (location == null && language != null && isLanguageMatch))
                    {
                        requestsPerMonth[request.FromDate.Month.ToString()]++;
                        totalRequests++;
                    }
                }
            }

                return (requestsPerMonth, totalRequests);
        }

        public (int, int) FindBest()
        {
            DateTime oneYearAgo = DateTime.Now.AddYears(-1);
            List<TourRequestSegment> requests = GetAll().Where(request => request.FromDate >= oneYearAgo).ToList();

            Dictionary<int, int> languageCounts = new Dictionary<int, int>();
            Dictionary<int, int> locationCounts = new Dictionary<int, int>();

            foreach (var request in requests)
            {
                if (languageCounts.ContainsKey(request.LanguageId))
                    languageCounts[request.LanguageId]++;
                else
                    languageCounts[request.LanguageId] = 1;

                if (locationCounts.ContainsKey(request.LocationId))
                    locationCounts[request.LocationId]++;
                else
                    locationCounts[request.LocationId] = 1;
            }
            int mostFrequentLanguageId = languageCounts.OrderByDescending(pair => pair.Value).First().Key;
            int mostFrequentLocationId = locationCounts.OrderByDescending(pair => pair.Value).First().Key;

            return (mostFrequentLanguageId, mostFrequentLocationId);
        }
    }
}
