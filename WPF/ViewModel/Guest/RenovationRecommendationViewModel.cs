using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.Domain.RepositoryInterfaces;
using BookingApp.WPF.View.Guest;

namespace BookingApp.WPF.ViewModel.Guest
{
    public class RenovationRecommendationViewModel : INotifyPropertyChanged
    {
        private readonly RenovationRecommendationService renovationRecommendationService;
        private readonly RateForm rateForm;
        private int reservationId;
        public event PropertyChangedEventHandler PropertyChanged;
        public RenovationRecommendationViewModel(RateForm rateForm, int reservationId)
        {
            renovationRecommendationService = new RenovationRecommendationService(Injector.CreateInstance<IRenovationRecommendationRepository>());
            this.rateForm = rateForm;
            this.reservationId = reservationId;
            LoadUrgencyLevels();
            SelectedUrgencyLevel = UrgencyLevels[0];
        }

        private ObservableCollection<string> urgencyLevels;
        public ObservableCollection<string> UrgencyLevels
        {
            get { return urgencyLevels; }
            set
            {
                urgencyLevels = value;
                OnPropertyChanged(nameof(UrgencyLevels));
            }
        }

        private string selectedUrgencyLevel;
        public string SelectedUrgencyLevel
        {
            get { return selectedUrgencyLevel; }
            set
            {
                selectedUrgencyLevel = value;
                OnPropertyChanged(nameof(SelectedUrgencyLevel));
            }
        }

        private string condition;
        public string Condition
        {
            get { return condition; }
            set
            {
                condition = value;
                OnPropertyChanged(nameof(Condition));
            }
        }

        private void LoadUrgencyLevels()
        {
            // Load urgency levels into the ObservableCollection
            UrgencyLevels = new ObservableCollection<string>
            {
                "Level 1 - It would be nice to renovate some small things, but everything works fine without it",
                "Level 2 - Minor complaints about the accommodation that, if removed, would make it perfect",
                "Level 3 - Several things that bothered should be renovated",
                "Level 4 - There are many bad things and renovation is really necessary",
                "Level 5 - The accommodation is in very bad condition and it is not worth renting it at all if it is not renovated"
            };
        }

        public void Submit()
        {
            if (string.IsNullOrEmpty(SelectedUrgencyLevel))
            {
                MessageBox.Show("Please select a renovation urgency level.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            RenovationRecommendationLevel renovationLevel;
            switch (SelectedUrgencyLevel)
            {
                case "Level 1 - It would be nice to renovate some small things, but everything works fine without it":
                    renovationLevel = RenovationRecommendationLevel.LEVEL1;
                    break;
                case "Level 2 - Minor complaints about the accommodation that, if removed, would make it perfect":
                    renovationLevel = RenovationRecommendationLevel.LEVEL2;
                    break;
                case "Level 3 - Several things that bothered should be renovated":
                    renovationLevel = RenovationRecommendationLevel.LEVEL3;
                    break;
                case "Level 4 - There are many bad things and renovation is really necessary":
                    renovationLevel = RenovationRecommendationLevel.LEVEL4;
                    break;
                case "Level 5 - The accommodation is in very bad condition and it is not worth renting it at all if it is not renovated":
                    renovationLevel = RenovationRecommendationLevel.LEVEL5;
                    break;
                default:
                    return;
            }

            RenovationRecommendation recommendation = new RenovationRecommendation()
            {
                RecommendationInfo = Condition,
                RenovationLevel = renovationLevel,
                ReservationId = reservationId
            };

            renovationRecommendationService.Save(recommendation);

            rateForm.SetRenovationRecommendation(true, recommendation.Id);
        }
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
