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
    }
}
