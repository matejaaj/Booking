using BookingApp.Application;
using BookingApp.Application.UseCases;
using BookingApp.Repository;
using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class DriverOnVacation : ISerializable
    {
        public int DriverId { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public int TypeId { get; set; }
        public int StatusId { get; set; }
        public VacationType Type { get; set; }
        public VacationStatus Status { get; set; }

        public DriverOnVacation()
        {
            DriverId = -1;
        }

        public DriverOnVacation(int driverId, DateOnly startDate, DateOnly endDate, int typeId, int statusId)
        {
            DriverId = driverId;
            StartDate = startDate;
            EndDate = endDate;
            TypeId = typeId;
            StatusId = statusId;
            UpdateModel();
        }

        public void UpdateModel()
        {
            Type = new VacationTypeService().Get(TypeId);
            Status = new VacationStatusService().Get(StatusId);
        }

        public void FromCSV(string[] values)
        {
            DriverId = Convert.ToInt32(values[0]);
            StartDate = DateOnly.Parse(values[1]);
            EndDate = DateOnly.Parse(values[2]);
            TypeId = Convert.ToInt32(values[3]);
            StatusId = Convert.ToInt32(values[4]);
        }

        public string[] ToCSV()
        {
            string[] values =
            {
                DriverId.ToString(), StartDate.ToString(), EndDate.ToString(), TypeId.ToString(), StatusId.ToString()
            };
            return values;
        }

        public override string ToString()
        {
            return $"Pocetak {StartDate.ToString()} Zavrsetak {EndDate.ToString()} --- TIP {Type.Name}";
        }
    }
}
