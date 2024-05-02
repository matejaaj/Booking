using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.RepositoryInterfaces
{
    public interface ITourRequestSegmentRepository
    {
        List<TourRequestSegment> GetAll();
        TourRequestSegment GetById(int reviewId);
        TourRequestSegment Save(TourRequestSegment review);
        void Delete(TourRequestSegment review);
        TourRequestSegment Update(TourRequestSegment review);
        int NextId();
    }
}
