using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using BookingApp.WPF.View.Guide;

namespace BookingApp.WPF.ViewModel.Guide
{
    public class GuideOverviewViewModel
    {
        public void ShowTourForm()
        {
            TourForm tourForm = new TourForm();
            tourForm.ShowDialog();
        }

        public void ShowTodayTours()
        {
            TodayToursOverview todayToursOverview = new TodayToursOverview();
            todayToursOverview.ShowDialog();
        }

        public void ShowAllTours()
        {
            AllToursOverview allToursOverview = new AllToursOverview();
            allToursOverview.ShowDialog();
        }
    }
}
