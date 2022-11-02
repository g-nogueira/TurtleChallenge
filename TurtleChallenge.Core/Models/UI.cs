using System.Drawing;
using TurtleChallenge.Core.Interfaces;
using TurtleChallenge.Utils;

namespace TurtleChallenge.Core.Models;

/// <summary>
/// Core class responsible for managind the UI.
/// </summary>
public class UI : IUserInterface
{
    private IVector2 _messagesLine = new Vector2(0, 10);

    /// <summary>
    /// Requests for the location of the GameSettings.ini file.
    /// </summary>
    /// <returns></returns>
    public string RequestSettingsFile()
    {
        return Prompt($"Settings Location ({Defaults.GameSettingsLocation}): ")?.NullIfEmpty() ?? Defaults.GameSettingsLocation;

    }
    
    /// <summary>
    /// Requests for the location of the GaveMoves.csv file.
    /// </summary>
    /// <returns></returns>
    public string RequestMovesFile()
    {
        return Prompt($"Settings Location ({Defaults.MovesLocations}): ")?.NullIfEmpty() ?? Defaults.MovesLocations;

    }

    /// <summary>
    /// Requests for an user input.
    /// </summary>
    /// <param name="title"></param>
    /// <param name="breakLineAfter"></param>
    /// <param name="breakLineBefore"></param>
    /// <returns></returns>
    public string? Prompt(string title, bool breakLineAfter = false, bool breakLineBefore = false)
    {
        if (breakLineBefore)
        {
            Console.WriteLine();
        }

        if (breakLineAfter)
        {
            Console.WriteLine(title);
        }
        else
        {
            Console.Write(title);
        }

        return Console.ReadLine()?.NullIfEmpty();
    }

    /// <summary>
    /// Displays a message in the UI while a game is running.
    /// </summary>
    /// <param name="message"></param>
    public void InGameMessage(string message, ConsoleColor color = ConsoleColor.Gray)
    {
        var original = Console.ForegroundColor;

        RemoveLine(_messagesLine.X, _messagesLine.Y);

        Console.SetCursorPosition(_messagesLine.X, _messagesLine.Y);
        Console.ForegroundColor = color;
        Console.Write(message);
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    /// <summary>
    /// Renders a text to the Console.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void Render(string text, int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.WriteLine(text);
        Console.SetCursorPosition(0, 0);
    }

    /// <summary>
    /// Renders a text to the Console.
    /// </summary>
    /// <param name="text"></param>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void Render(string text, int x, int y, ConsoleColor color = ConsoleColor.Gray)
    {
        Console.ForegroundColor = color;
        Render(text, x, y);
        Console.ForegroundColor = ConsoleColor.Gray;
    }

    /// <summary>
    /// Clears the whole Console.
    /// </summary>
    public void Clear()
    {
        Console.Clear();
    }

    /// <summary>
    /// Removes a given line in the Console.
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    private static void RemoveLine(int x, int y)
    {
        Console.SetCursorPosition(x, y);
        Console.Write(new string(' ', Console.WindowWidth));
    }

    public static UI GetInstance()
    {
        return new UI();
    }

}