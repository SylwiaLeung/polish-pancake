using Hangman.CsvManagers;
using Hangman.Models;
using System.Collections.Generic;

namespace Hangman
{
    public static class Constants
    {
        public static List<CountryCityPair> EuropeanCapitals = InputManager.ReadCountryCapitalPairs();
    }
}
