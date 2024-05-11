using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using System.Windows;
using System.Windows.Controls;

namespace BookingApp.WPF.View.Guest
{
    public partial class RenovationRecommendationForm : Window
    {
        private readonly RenovationRecommendationService renovationRecommendationService;
        private readonly RateForm rateForm;
        private int reservationId;

        public RenovationRecommendationForm(RateForm rateForm, int reservationId)
        {
            InitializeComponent();
            this.rateForm = rateForm;
            this.reservationId = reservationId;
            renovationRecommendationService = new RenovationRecommendationService();
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string condition = ConditionTextBox.Text;
            string urgencyLevel = (UrgencyComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            RenovationRecommendationLevel renovationLevel;

            switch (urgencyLevel)
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
                    MessageBox.Show("Please select a renovation urgency level.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
            }

            RenovationRecommendation recommendation = new RenovationRecommendation()
            {
                RecommendationInfo = condition,
                RenovationLevel = renovationLevel,
                ReservationId = reservationId
            };

            renovationRecommendationService.Save(recommendation);

            rateForm.SetRenovationRecommendation(true, recommendation.Id);

            Close();
        }
    }
}
