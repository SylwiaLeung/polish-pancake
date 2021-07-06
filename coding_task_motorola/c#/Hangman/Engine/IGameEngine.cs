using Hangman.Models;

namespace Hangman.Engine
{
    public interface IGameEngine
    {
        HighScoreRecord RunGame();
    }
}