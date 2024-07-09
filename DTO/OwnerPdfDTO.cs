using BookingApp.Domain.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.DTO
{
    public class OwnerPdfDTO : INotifyPropertyChanged
    {

        private string name { get; set; }

        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string type { get; set; }

        public string Type
        {
            get { return type; }
            set
            {
                if (type != value)
                {
                    type = value;
                    OnPropertyChanged("Type");
                }
            }
        }

        private double avgScore { get; set; }

        public double AvgScore
        {
            get { return avgScore; }
            set
            {
                if (avgScore != value)
                {
                    avgScore = value;
                    OnPropertyChanged("AvgScore");
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public OwnerPdfDTO(Accommodation accommodation, double avgScore)
        {
            Name = accommodation.Name;
            AvgScore = avgScore;
            Type = accommodation.Type.ToString();
        }

        public override string ToString()
        {
            return $"Name: {Name}, Average Score: {AvgScore:F2}";
        }
    }
}
