using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class ComplexTourRequestDTO : INotifyPropertyChanged
    {
        public int ComplexTourRequestId { get; set; }
        private List<TourRequestDTO> _tourSegments;

        public List<TourRequestDTO> TourSegments
        {
            get { return _tourSegments; }
            set
            {
                if (_tourSegments != value)
                {
                    _tourSegments = value;
                    OnPropertyChanged("TourSegments");
                }
            }
        }

        private string _status;

        public string Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged("Status");
                }
            }
        }

        private TourRequestStatus _isAccepted;

        public TourRequestStatus IsAccepted
        {
            get { return _isAccepted; }
            set
            {
                if (_isAccepted != value)
                {
                    _isAccepted = value;
                    OnPropertyChanged("IsAccepted");
                }
            }
        }

        public string DisplayLocations
        {
            get
            {
                return TourSegments.Take(2).Select(ts => ts.Location).Aggregate((i, j) => i + "\n" + j);
            }
        }

        public string DisplayGuestNames
        {
            get
            {
                if (TourSegments.Any())
                {
                    return TourSegments.First().DisplayGuests;
                }
                return string.Empty;
            }
        }

        public string DateRange
        {
            get
            {
                if (TourSegments.Any())
                {
                    var firstSegment = TourSegments.First();
                    return $"{firstSegment.FromDate:dd.MM.yyyy} - {firstSegment.ToDate:dd.MM.yyyy}";
                }
                return "Nema dostupnih datuma";
            }
        }

        public string GetStatusDescription(TourRequestStatus status)
        {
            switch (status)
            {
                case TourRequestStatus.PENDING:
                    return "Status: Na cekanju";
                case TourRequestStatus.ACCEPTED:
                    return "Status: Prihvaćen";
                case TourRequestStatus.CANCELED:
                    return "Status: Odbijen";
                default:
                    return "Status: Nepoznat";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ComplexTourRequestDTO()
        {

        }
    }
}
