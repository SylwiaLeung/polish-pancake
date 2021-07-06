using System;
using Hangman.CsvManagers;
using Hangman.Engine;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Hangman game");
            var gameEngine = new GameEngine();
            var resultsManager = new ResultsFileManager();
            while (true)
            {
                var score = gameEngine.RunGame();
                if (score != null)
                {
                    resultsManager.ManageHighScores(score);
                    resultsManager.PrintResults();
                }

                Console.WriteLine("Game over!");
                Console.WriteLine("Would you like to quit?");
                Console.WriteLine("Press [y] to quit or press Enter to continue the fun!");
                var response = Console.ReadLine();
                if (response.ToLower() == "y")
                {
                    break;
                }
            }
            Console.WriteLine("Thank you for playing Hangman!");
            Console.WriteLine("Press Enter to exit");
            Console.ReadLine();
        }

    }
}
