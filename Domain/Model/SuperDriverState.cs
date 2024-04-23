using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class SuperDriverState : ISerializable
    {
        public int DriverID { get; set; }
        public int StateOfPoints { get; set; }
        public int IsSuper { get; set; }
        public int NumberOfDrives { get; set; }
        public DateOnly? DateOfGettingStatus { get; set; }
        public DateOnly? DateOfEndingOfStatus { get; set; }

        public SuperDriverState() { }
        public SuperDriverState(int driverID, int stateOfPoints, int isSuper, int numberOfDrives, DateOnly? dateOfGettingStatus, DateOnly? dateOfEndingOfStatus)
        {
            DriverID = driverID;
            StateOfPoints = stateOfPoints;
            IsSuper = isSuper;
            NumberOfDrives = numberOfDrives;
            DateOfGettingStatus = dateOfGettingStatus;
            DateOfEndingOfStatus = dateOfEndingOfStatus;
        }


        public void FromCSV(string[] values)
        {
            DriverID = Convert.ToInt32(values[0]);
            StateOfPoints = Convert.ToInt32(values[1]);
            IsSuper = Convert.ToInt32(values[2]);
            NumberOfDrives = Convert.ToInt32(values[3]);
            if (values[4] == "null" && values[5] == "null")
            {
                DateOfGettingStatus = null;
                DateOfGettingStatus = null;
            }
            else
            {
                DateOfGettingStatus = DateOnly.Parse(values[4]);
                DateOfEndingOfStatus = DateOnly.Parse(values[5]);
            }
        }

        public string[] ToCSV()
        {
            string[] strings = { DriverID.ToString(), StateOfPoints.ToString(), IsSuper.ToString(), NumberOfDrives.ToString(),
                (!DateOfGettingStatus.HasValue) ? "null" : DateOfGettingStatus.ToString(),
                (!DateOfEndingOfStatus.HasValue) ? "null" : DateOfEndingOfStatus.ToString() };
            return strings;
        }
    }
}
