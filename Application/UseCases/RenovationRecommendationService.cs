using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BookingApp.Application.UseCases
{
    public class RenovationRecommendationService
    {
        private readonly IRenovationRecommendationRepository _renovationRepository;

        public RenovationRecommendationService(IRenovationRecommendationRepository renovationRecommendationRepository)
        {
            _renovationRepository = renovationRecommendationRepository;
        }

        public RenovationRecommendationService(IRenovationRecommendationRepository renovationRepository)
        {
            _renovationRepository = renovationRepository;
        }


        public List<RenovationRecommendation> GetAll()
        {
            return _renovationRepository.GetAll();
        }

        public RenovationRecommendation Save(RenovationRecommendation renovationRecommendation)
        {
            return _renovationRepository.Save(renovationRecommendation);
        }

        public void Delete(RenovationRecommendation renovationRecommendation)
        {
            _renovationRepository.Delete(renovationRecommendation);
        }

        public RenovationRecommendation Update(RenovationRecommendation renovationRecommendation)
        {
            return _renovationRepository.Update(renovationRecommendation);
        }

        public RenovationRecommendation GetById(int id)
        {
            return _renovationRepository.GetById(id);
        }

        public List<RenovationRecommendation> GetAllWithReservationId(int id)
        {
            var recommendations = _renovationRepository.GetAll();
            return recommendations.FindAll(r => r.ReservationId == id).ToList();
        }
    }
}
