using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class TourRequestService
    {
        private readonly ITourRequestRepository _tourRequestRepository;

        public TourRequestService(ITourRequestRepository tourRequestRepository)
        {
            _tourRequestRepository = tourRequestRepository;
        }


        public List<TourRequest> GetAll()
        {
            return _tourRequestRepository.GetAll();
        }

        public TourRequest GetById(int id)
        {
            return _tourRequestRepository.GetById(id);
        }

        public TourRequest Save(TourRequest tourRequest)
        {
            return _tourRequestRepository.Save(tourRequest);
        }

        public void Delete(TourRequest tourRequest)
        {
            _tourRequestRepository.Delete(tourRequest);
        }

        public TourRequest Update(TourRequest tourRequest)
        {
            return _tourRequestRepository.Update(tourRequest);
        }

        public List<TourRequest> GetSimpleRequests()
        {
            return _tourRequestRepository.GetAll().Where(request => request.IsComplex == false).ToList();
        }




        public List<TourRequest> GetComplexRequests()
        {
            return _tourRequestRepository.GetAll().Where(request => request.IsComplex == true).ToList();
        }


        public List<TourRequest> GetComplexRequestsForUser(int userId)
        {
            return GetComplexRequests().Where(request => request.TouristId == userId).ToList();
        }

        public List<TourRequest> GetSimpleRequestsForUser(int userId)
        {

            return GetSimpleRequests().Where(request => request.TouristId == userId).ToList();
        }


    }
}
