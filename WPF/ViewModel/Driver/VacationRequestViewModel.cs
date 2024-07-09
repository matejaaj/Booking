using BookingApp.Application.Events;
using BookingApp.Domain.Model;
using BookingApp.LogicServices.Driver;
using BookingApp.WPF.View.Driver;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Diagnostics;

namespace BookingApp.WPF.ViewModel.Driver
{
    public class VacationRequestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DateTime _startDate = DateTime.Now;
        public DateTime StartDate
        {
            get { return _startDate; }
            set {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }

        private DateTime _endDate = DateTime.Now;
        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }

        private int _statusId;
        public int StatusId
        {
            get { return _statusId; }
            set
            {
                _statusId = value;
                OnPropertyChanged(nameof(StatusId));
            }
        }

        

        private ObservableCollection<VacationType> _vacTypes = new ObservableCollection<VacationType>();
        public ObservableCollection<VacationType> VacTypes
        {
            get { return this._vacTypes; }
            set
            {
                _vacTypes = value;
                OnPropertyChanged(nameof(VacTypes));
            }
        }

        private VacationType _selectedType;
        public VacationType SelectedType
        {
            get { return _selectedType; }
            set
            {
                _selectedType = value;
                OnPropertyChanged(nameof(SelectedType));
            }
        }

        private int DriverId;
        private readonly VacationService service;
        public VacationRequestViewModel(int driverId, VacationService vacService) 
        {
            DriverId = driverId;
            service = vacService;
            VacTypes = new ObservableCollection<VacationType>(service.GetTypes());
            SelectedType = VacTypes.First();

        }

        public async Task Button_Confirm(object sender, EventArgs e, System.Windows.Controls.Page owner)
        {
            DateOnly timeNow = DateOnly.FromDateTime(DateTime.Now);
            timeNow.AddDays(2);
            bool allowed = false;
            if(DateOnly.FromDateTime(StartDate) > timeNow)
            {
                allowed = await service.sendRequest(DateOnly.FromDateTime(StartDate), DateOnly.FromDateTime(EndDate), SelectedType.Id, false);
                if (!allowed)
                {
                    MainWindow.EventAggregator.Publish(new ShowMessageEvent("Your vacation is dennied!", "Notification"));
                    service.AllowVacation(DateOnly.FromDateTime(StartDate), DateOnly.FromDateTime(EndDate), SelectedType.Id, 2);
                }
                else
                {
                    MainWindow.EventAggregator.Publish(new ShowMessageEvent("Your vacation is approwed!", "Notification"));
                }

            }
            else
            {
                allowed = await service.sendRequest(DateOnly.FromDateTime(StartDate), DateOnly.FromDateTime(EndDate), SelectedType.Id, true);
                if(allowed)
                {
                    MainWindow.EventAggregator.Publish(new ShowMessageEvent("Your vacation is approwed!", "Notification"));
                }
                else
                {
                    MainWindow.EventAggregator.Publish(new ShowMessageEvent("Your vacation is dennied!", "Notification"));
                    service.AllowVacation(DateOnly.FromDateTime(StartDate), DateOnly.FromDateTime(EndDate), SelectedType.Id, 2);
                }
                owner.NavigationService.GoBack();
            }
        }

        public List<DriverOnVacation> getVacationsForDriver()
        {
            return service.getVacationsForDriver(DriverId);
        }

        public void GeneratePDF(List<DriverOnVacation> vacations, int start, int end)
        {
            Document doc = new Document(PageSize.A4);
            string filePath = "../../../PDF/report.pdf";
            PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(filePath, FileMode.Create));
            doc.Open();
            PdfContentByte contentByte = writer.DirectContent;
            doc.AddTitle("Kita je ovo");
            var list = vacations.Where(v => v.StartDate.Year > start && v.EndDate.Year < end).ToList();
            foreach ( var v in list)
            {
                doc.Add(new Paragraph(v.ToString()));
            }
            writer.Flush();
            doc.Close();
            writer.Close();
            Process.Start("C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe", $"file:///\"C:\\Programiranje\\SIMS\\sims-ra-2024-group-5-team-b\\PDF\\report.pdf\"");
        }
    }
}
