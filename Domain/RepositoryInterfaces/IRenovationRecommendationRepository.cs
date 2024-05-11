using BookingApp.Domain.Model;
using System.Collections.Generic;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface IRenovationRecommendationRepository
    {
        List<RenovationRecommendation> GetAll();
        RenovationRecommendation Save(RenovationRecommendation renovationRecommendation);
        void Delete(RenovationRecommendation renovationRecommendation);
        RenovationRecommendation Update(RenovationRecommendation renovationRecommendation);
        RenovationRecommendation GetById(int id);
    }
}
