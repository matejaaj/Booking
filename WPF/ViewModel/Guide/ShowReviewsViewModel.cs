using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.WPF.ViewModel.Guide
{

    internal class ShowReviewsViewModel
    {
        public ObservableCollection<TourInstance> TourInstances { get; set; }
        private List<TourInstance> allTourInstances;
        public TourInstance SelectedInstance { get; set; }
        private readonly TourInstanceService _tourInstanceService;
        public ShowReviewsViewModel(TourDTO tourDTO)
        {
            _tourInstanceService = new TourInstanceService();
            allTourInstances = _tourInstanceService.GetAllByTourId(tourDTO.Id);
            TourInstances = new ObservableCollection<TourInstance>();

            foreach(var instance in allTourInstances)
            {
                if (instance.IsCompleted)
                    TourInstances.Add(instance);
            }
        }

        public void ShowReviews()
        {

        }
    }
}
