﻿using TurtleChallenge.Core.Interfaces;
using TurtleChallenge.Utils;

namespace TurtleChallenge.Core.Models;
public class UI : IUserInterface
{
    private ICoordinate _messagesLine = new Coordinate(0, 6);

    public string RequestSettingsFile()
    {
        return Prompt($"Settings Location ({Defaults.GameSettingsLocation}): ", breakLine: false)?.NullIfEmpty() ?? Defaults.GameSettingsLocation;

    }

    public string RequestMovesFile()
    {
        return Prompt($"Settings Location ({Defaults.MovesLocations}): ", breakLine: false)?.NullIfEmpty() ?? Defaults.MovesLocations;

    }

    public string? Prompt(string title, bool breakLine = true)
    {
        if (breakLine)
        {
            Console.WriteLine(title);
        }
        else
        {
            Console.Write(title);
        }

        return Console.ReadLine()?.NullIfEmpty();
    }

    public void Display(string message)
    {
        Console.WriteLine(message);
    }

    public void GameMessage(string message)
    {
        RemoveLine(_messagesLine.X, _messagesLine.Y);

        Console.SetCursorPosition(_messagesLine.X, _messagesLine.Y);

        Console.Write(message);
    }

    public void DisplayBoard(Board board, IEnumerable<GameObject> gameObjects)
    {
        ResetBoard(board);

        Console.WriteLine($"┌────┬────┬────┬────┬────┐");

        for (int y = 0; y < board.Size.Y; y++)
        {
            Console.Write($"│");
            for (int x = 0; x < board.Size.X; x++)
            {
                var objectToDraw = gameObjects.FirstOrDefault(o => o.Position.Equals(new Coordinate(x, y)) && o.IsVisible);

                if (objectToDraw != null && objectToDraw.Position.Equals(new Coordinate(x, y)))
                {
                    Console.Write($" {objectToDraw.UI()}  │");

                }
                else
                {
                    Console.Write($"    │");
                }
            }
            Console.WriteLine();
        }

        Console.WriteLine($"└────┴────┴────┴────┴────┘");

        _messagesLine.Y = board.Size.Y + 2;
    }

    public void ResetBoard(Board board)
    {
        // Removes first line
        RemoveLine(0, 0);

        var lineCount = 1;
        while (lineCount <= board.Size.Y)
        {
            RemoveLine(0, board.Position.Y + lineCount);

            lineCount++;
        }

        RemoveLine(0, lineCount);

        Console.SetCursorPosition(0, 0);
    }

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