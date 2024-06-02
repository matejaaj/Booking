using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingApp.Serializer;

namespace BookingApp.Domain.Model
{
    public class LocationState : ISerializable
    {
        public int LocationId { get; set; }
        public State LocState { get; set; }
        public LocationState() { }
        public void FromCSV(string[] values)
        {
            LocationId = Convert.ToInt32(values[0]);
            LocState = (State) Enum.Parse(typeof(State), values[1]);
        }

        public string[] ToCSV()
        {
            string[] s = {LocationId.ToString(), LocState.ToString()};
            return s;
        }
    }

    public enum State
    {
        BAD, SUPER
    }
}
