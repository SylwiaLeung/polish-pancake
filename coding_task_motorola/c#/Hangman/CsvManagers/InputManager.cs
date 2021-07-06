using CsvHelper;
using CsvHelper.Configuration;
using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Hangman.CsvManagers
{
    public static class InputManager
    {

        public static string FileName = "countries_and_capitals.txt";

        public static List<CountryCityPair> ReadCountryCapitalPairs()
        {
            var csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
                Delimiter = "|",
                TrimOptions = TrimOptions.Trim
            };
            using (var reader = new StreamReader(FileName))
            using (var csv = new CsvReader(reader, csvConfig))
            {
                var records = csv.GetRecords<CountryCityPair>().ToList();
                return records;
            }
        }

    }
}
