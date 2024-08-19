﻿using BookingApp.Application.UseCases;
using BookingApp.Domain.Model;
using BookingApp.WPF.ViewModel.Tourist;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BookingApp.WPF.View.Tourist
{
    /// <summary>
    /// Interaction logic for TouristTabsWindow.xaml
    /// </summary>
    public partial class TouristTabsWindow : Window
    {
        public TouristTabsViewModel ViewModel { get; private set; }

        public TouristTabsWindow(User user)
        {
            InitializeComponent();
            ViewModel = new TouristTabsViewModel(user);

            ViewModel.RequestClose += (s, e) => this.Close();
            ViewModel.ShowAllToursRequested += (s, e) => SwitchToTab(AllToursTabItem);
            ViewModel.ShowMyToursRequested += (s, e) => SwitchToTab(MyToursTabItem);
            ViewModel.ShowTourRequestsRequested += (s, e) => SwitchToTab(TourRequestsTabItem);
            ViewModel.ShowMyDrivesRequested += (s, e) => SwitchToTab(MyDrivesTabItem);

            DataContext = ViewModel;
        }

        private void SwitchToTab(TabItem tabItem)
        {
            // First, switch the main TabControl to the appropriate parent tab
            if (tabItem.Parent is TabControl parentTabControl)
            {
                if (parentTabControl.Parent is TabItem parentTabItem)
                {
                    MainTabControl.SelectedItem = parentTabItem;
                }
                // Then switch the nested TabControl to the appropriate child tab
                parentTabControl.SelectedItem = tabItem;
            }
        }

        private void OnToursTabSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl tabControl && tabControl.SelectedItem == MyToursTabItem)
            {
                ViewModel.ToursMainViewModel.MyToursViewModel.CreateViewModels();
            }
        }

    }
}
