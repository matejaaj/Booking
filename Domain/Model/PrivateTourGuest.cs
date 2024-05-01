﻿using BookingApp.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Domain.Model
{
    public class PrivateTourGuest : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public int TouristId { get; set; }
        public PrivateTourGuest() { }

        public PrivateTourGuest(int id, string name, int age, int touristId)
        {
            Id = id;
            Name = name;
            Age = age;
            TouristId = touristId;
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Age = int.Parse(values[2]);
            TouristId = int.Parse(values[3]);
        }
        public string[] ToCSV()
        {
            return new string[] {
                Id.ToString(),
                Name,
                Age.ToString(),
                TouristId.ToString()
            };
        }
    }
}
