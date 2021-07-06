using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Models
{
    public class CountryCityPair
    {
        public string Country { get; }
        public string City { get; }

        public CountryCityPair(string country, string city)
        {
            Country = country;
            City = city;
        }
        public sealed class CountryCityPairMap : ClassMap<CountryCityPair>
        {
            public CountryCityPairMap()
            {
                Map(m => m.Country).Index(0);
                Map(m => m.City).Index(1);
            }
        }
    }
}
