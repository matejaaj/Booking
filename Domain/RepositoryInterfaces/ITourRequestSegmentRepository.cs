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
        TourRequestSegment GetById(int requestId);
        TourRequestSegment Save(TourRequestSegment request);
        void Delete(TourRequestSegment request);
        TourRequestSegment Update(TourRequestSegment request);
        int NextId();
    }
}
