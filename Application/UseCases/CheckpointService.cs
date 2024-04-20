using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Application.UseCases
{
    public class CheckpointService
    {
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly TourInstanceService _tourInstanceService;
        public CheckpointService()
        {
            _checkpointRepository = Injector.CreateInstance<ICheckpointRepository>();
            _tourInstanceService = new TourInstanceService();
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

        public Checkpoint GetById(int id)
        {
            return _checkpointRepository.GetById(id);
        }

        public (ObservableCollection<Checkpoint> NotVisited, ObservableCollection<Checkpoint> Visited) InitializeCheckpoints(int tourId, int tourInstanceId)
        {
            var notVisitedCheckpoints = new ObservableCollection<Checkpoint>();
            var visitedCheckpoints = new ObservableCollection<Checkpoint>();

            foreach (var checkpoint in GetAll())
            {
                if (checkpoint.TourId == tourId)
                    notVisitedCheckpoints.Add(checkpoint);
            }

            if (notVisitedCheckpoints.Any())
            {
                var firstCheckpoint = notVisitedCheckpoints.First();
                visitedCheckpoints.Add(firstCheckpoint);
                notVisitedCheckpoints.Remove(firstCheckpoint);

                var activeTour = _tourInstanceService.GetAll().Find(tour => tour.Id == tourInstanceId);
                if (activeTour != null)
                {
                    activeTour.CurrentCheckpoint = firstCheckpoint.Name;
                    _tourInstanceService.Update(activeTour);
                }
            }

            return (notVisitedCheckpoints, visitedCheckpoints);
        }
    }

}
