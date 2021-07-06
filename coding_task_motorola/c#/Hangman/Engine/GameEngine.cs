using Hangman.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Hangman.Engine
{
    public class GameEngine : IGameEngine
    {
        private CountryCityPair CountryAndCity { get; set; }
        private List<string> stages = AsciiImages.Stages.ToList();

        public HighScoreRecord RunGame()
        {
            while (true)
            {
                var lives = stages.Count() - 1;
                var numberOfGuesses = 0;
                CountryAndCity = ChooseRandomCity();
                var guessedLetters = new HashSet<char>();
                var notInWordLetters = new HashSet<char>();
       
                var time = new Stopwatch();
                time.Start();

                while (lives > 0)
                {
                    Console.WriteLine(stages[lives]);

                    Console.WriteLine("Try to guess the capital!");
                    
                    Console.WriteLine($"Lives = {new string('*', lives)}");
                    if (lives == 1)
                    {
                        Console.WriteLine("***************************************************");
                        Console.WriteLine($"HINT: capital of {CountryAndCity.Country}");
                        Console.WriteLine("***************************************************");
                    }


                    var validGuessWasPicked = false;
                    bool success = false;
                    bool isWord = false;
                    string guess = "";

                    do
                    {
                        Console.WriteLine(ConcealCityName(guessedLetters));
                        if (notInWordLetters.Count != 0)
                        {
                            Console.WriteLine("Letters not in word: " + string.Join(',', notInWordLetters));
                        }
                         Console.Write("Guess a letter or word: ");

                        guess = Console.ReadLine().Trim();
                        if (guess.Length == 1)
                        {
                            validGuessWasPicked = !notInWordLetters.Contains(guess[0]);

                        } else { break;  }

                        if (!validGuessWasPicked)
                            Console.WriteLine("You've already picked this letter. Try again!");
                    }
                    while (!validGuessWasPicked);

                    numberOfGuesses += 1;
                    success = HandleGuess(guess);
                    isWord = guess.Length > 1;

                    if (success)
                    {
                        if (isWord)
                        {
                            Console.WriteLine("You guessed it! Congratulations!");
                            Console.WriteLine(AsciiImages.Doge);
                            Console.WriteLine($"It took you {numberOfGuesses} guesses, and {time.Elapsed.TotalSeconds} seconds");
                            Console.WriteLine($"The word was: {CountryAndCity.City}");
                            Console.WriteLine("Please enter your name for the hall of fame:");
                            var name = Console.ReadLine();
                            return new HighScoreRecord(DateTime.Now, name, CountryAndCity.City, numberOfGuesses, (int)time.Elapsed.TotalSeconds);
                        }
                        else
                        {
                            Console.WriteLine("You guessed a letter! Congratulations!");
                            if (guessedLetters.Contains(guess[0]))
                                Console.WriteLine("You've already guessed this letter, try again!");

                            guessedLetters.Add(guess[0]);
                            if (CheckIfAllLettersWereGuessed(guessedLetters))
                            {
                                Console.WriteLine("You guessed the whole word! Congratulations!");
                                Console.WriteLine(AsciiImages.Doge);
                                time.Stop();
                                Console.WriteLine($"It took you {numberOfGuesses} guesses, and {time.Elapsed.TotalSeconds} seconds");
                                Console.WriteLine($"The word was: {CountryAndCity.City}");
                                Console.WriteLine("Please enter your name for the hall of fame:");
                                var name = Console.ReadLine();
                                return new HighScoreRecord(DateTime.Now, name, CountryAndCity.City, numberOfGuesses, (int)time.Elapsed.TotalSeconds);
                            }
                            ConcealCityName(guessedLetters);
                        }
                    }
                    else
                    {
                        if(guess.Length < 1)
                        {
                            Console.WriteLine("This is not a valid input, try again.");
                            continue;
                        }
                        if (!isWord)
                        {
                            Console.WriteLine("Missed! You lost a life");
                            notInWordLetters.Add(guess[0]);
                            lives -= 1;
                        }
                        else
                        {
                            Console.WriteLine("Missed! You lost two lives");

                            lives -= 2;
                        }
                    }
                }
                if (lives <= 0)
                {
                    Console.WriteLine(stages[0]);
                    Console.WriteLine("You lost! :(");
                    Console.WriteLine($"The word was: {CountryAndCity.City}");

                    return null;
                }
            }
        }
        private CountryCityPair ChooseRandomCity()
        {
            var cities = Constants.EuropeanCapitals;
            var city = cities[new Random().Next(0, cities.Count)];
            return city;
        }

        private string ConcealCityName(HashSet<char> guessedLetters)
        {
            var replacedCityName = new char[CountryAndCity.City.Length];
            var cityNameInLowerCase = CountryAndCity.City.ToLower();
            for (int i = 0; i < CountryAndCity.City.Length; i++)
            {
                if (guessedLetters.Contains(cityNameInLowerCase[i]))
                {
                    replacedCityName[i] = CountryAndCity.City[i];
                }
                else
                {
                    replacedCityName[i] = '_';
                }
            }

            return new string(replacedCityName);
        }

        private bool CheckIfAllLettersWereGuessed(HashSet<char> guessedLetters)
        {
            var cityLetters = new HashSet<char>(CountryAndCity.City.ToLower());
            return cityLetters.SetEquals(guessedLetters);
        }

        private bool HandleGuess(string guess)
        {
            if (guess.Length == 0)
            {
                Console.WriteLine("There's no such letter");
                return false;
            }
            if (guess.Length > 1)
            {
                return CountryAndCity.City.ToLower().Equals(guess.ToLower());
            }
            else
            {
                return CountryAndCity.City.ToLower().Contains(guess.ToLower());
            }
        }
    }
}
