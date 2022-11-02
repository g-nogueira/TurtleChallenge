using TurtleChallenge.Core.Models;
using TurtleChallenge.Utils;

namespace TurtleChallenge.Core.Interfaces;
public interface IUserInterface
{
    /// <summary>
    /// Requests for the location of the GameSettings.ini file.
    /// </summary>
    /// <returns></returns>
    public string RequestSettingsFile();

    /// <summary>
    /// Requests for the location of the GaveMoves.csv file.
    /// </summary>
    /// <returns></returns>
    public string RequestMovesFile();
    
    /// <summary>
    /// Requests for an user input.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="breakLineAfter"></param>
    /// <param name="breakLineBefore"></param>
    /// <returns></returns>
    public string? Prompt(string title, bool breakLineAfter = false, bool breakLineBefore = false);
    
    /// <summary>
    /// Displays a message in the UI while a game is running.
    /// </summary>
    /// <param name="message"></param>
    public void InGameMessage(string message, ConsoleColor color);

    /// <summary>
    /// Clears the whole Console.
    /// </summary>
    public void Clear();

    public void Render(string text, int x, int y);
    public void Render(string text, int x, int y, ConsoleColor color);
    public static abstract UI GetInstance();
}
