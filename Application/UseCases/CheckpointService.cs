using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class CheckpointService
    {
        private readonly ICheckpointRepository _checkpointRepository;

        public CheckpointService()
        {
            _checkpointRepository = Injector.CreateInstance<ICheckpointRepository>();
        }

        public List<Checkpoint> GetAll()
        {
            return _checkpointRepository.GetAll();
        }

        public List<Checkpoint> GetAllByTourId(int tourId)
        {
            var allCheckpoints = GetAll();
            var checkpointsForTour = allCheckpoints.Where(checkpoint => checkpoint.TourId == tourId).ToList();


            return checkpointsForTour;
        }

        public Checkpoint Save(Checkpoint checkpoint)
        {
            return _checkpointRepository.Save(checkpoint);
        }

        public void Delete(Checkpoint checkpoint)
        {
            _checkpointRepository.Delete(checkpoint);
        }

        public Checkpoint Update(Checkpoint checkpoint)
        {
            return _checkpointRepository.Update(checkpoint);
        }
    }

}
