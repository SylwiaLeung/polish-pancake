using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hangman.Models
{
    public class HighScoreRecord
    {

        public DateTime Date { get; }
        public string Name { get; }
        public string City { get; }
        public int Tries { get; }
        public double GuessingTime { get; }

        public HighScoreRecord(DateTime date, string name, string city, int tries, double guessingTime)
        {
            Date = date;
            Name = name;
            City = city;
            Tries = tries;
            GuessingTime = guessingTime;
        }


        public override string ToString()
        {
            return $"{Name} - Time: {GuessingTime} - number of tries: {Tries} - date: {Date} - guessed word: {City}";
        }

        public sealed class HighScoreRecordMap : ClassMap<HighScoreRecord>
        {
            public HighScoreRecordMap()
            {
                Map(m => m.Date).Index(0);
                Map(m => m.Name).Index(1);
                Map(m => m.City).Index(2);
                Map(m => m.Tries).Index(3);
                Map(m => m.GuessingTime).Index(4);
            }
        }
    }
}
