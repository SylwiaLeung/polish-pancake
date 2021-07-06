using CsvHelper;
using CsvHelper.Configuration;
using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using System.Linq;

namespace Hangman.CsvManagers
{
    public class ResultsFileManager
    {

        private List<HighScoreRecord> HighScores { get; set; }
        private const string FileName = "HighScores.csv";
        public ResultsFileManager()
        {
            if (!File.Exists(FileName))
            {
                File.Create(FileName);
            }
            else
            {
                HighScores = ReadRecords();
            }
        }

        public void PrintResults()
        {
            for (int i = 0; i < HighScores.Count; i++)
            {
                Console.WriteLine($"{i + 1}." + HighScores[i].ToString());
            }
        }

        public bool ManageHighScores(HighScoreRecord record)
        {
            if (HighScores.Count == 10)
            {
                if (HighScores[9].GuessingTime > record.GuessingTime)
                {
                    HighScores.Add(record);
                    HighScores = HighScores.OrderBy(record => record.GuessingTime).ToList();
                    HighScores.RemoveAt(10);
                    WriteRecords(HighScores);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                HighScores.Add(record);
                HighScores = HighScores.OrderBy(record => record.GuessingTime).ToList();
                WriteRecords(HighScores);
                return true;
            }
        }

        private List<HighScoreRecord> ReadRecords()
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                Delimiter = "|"
            };

            using (var reader = new StreamReader(FileName))
            using (var csv = new CsvReader(reader, csvConfig))
            {
                return csv.GetRecords<HighScoreRecord>().ToList();
            }
        }

        public void WriteRecords(List<HighScoreRecord> records)
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                Delimiter = "|"
            };

            using (var writer = new StreamWriter(FileName))
            using (var csv = new CsvWriter(writer, csvConfig))
            {
                csv.WriteRecords(records);
            }
        }

    }
}
