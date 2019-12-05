using System;
using System.Collections.Generic;
using System.Text;

namespace TokioCity.Models
{
    public class Address
    {
        [LiteDB.BsonId]
        public int Id { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public int Building { get; set; }
        public int Flat { get; set; }
        public string Comment { get; set; }

        public Address(string city, string street, string house, int building, int flat, string comment):this()
        {
            City = city;
            Street = street;
            House = house;
            Building = building;
            Flat = flat;
            Comment = comment;
        }

        public string FormatAddress()
        {
            return $"{City}, {Street}, {House}, корп. {Building}, кв. {Flat}";
        }

        public Address() { }
    }
}
