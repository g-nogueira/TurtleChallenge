//// See https://aka.ms/new-console-template for more information

using TurtleChallenge.Core.Models;

//GameSettings gameSettings = GameSettings.GetInstance(UI.RequestSettingsFile());
//MatchMoves matchMoves = MatchMoves.GetInstance(UI.RequestMovesFile());

GameSettings gameSettings = GameSettings.GetInstance(@"C:\Projects\TurtleChallenge\GameSettings.ini");
MatchMoves matchMoves = MatchMoves.GetInstance(@"C:\Projects\TurtleChallenge\GameMoves.csv");

Game game = Game.GetInstance();
game.StartMatch(gameSettings, matchMoves);

//MoveSettings moveSettings = UI.RequestMovesFile();

//using TurtleChallenge.Models;

//Console.WriteLine("Welcome, Turtle Player!\nTo begin, we need a few things first.");

//string defaultGameSettingsLocation = "./GameSettings.ini";
//string defaultMoveLocation = "./GaveMoves.csv";

//Console.Write($"Settings Location: ({defaultGameSettingsLocation})");
//string settingsLocation = Console.ReadLine()?.NullIfEmpty() ?? defaultGameSettingsLocation;

//Console.Write($"Moves Location: ({defaultMoveLocation})");
//string movesLocation = Console.ReadLine()?.NullIfEmpty() ?? defaultMoveLocation;


//var gameSettings = new GameSettings(settingsLocation);


//public static class StringExtensions
//{
//    public static string? NullIfEmpty(this string s)
//    {
//        return string.IsNullOrEmpty(s) ? null : s;
//    }
//    public static string? NullIfWhiteSpace(this string s)
//    {
//        return string.IsNullOrWhiteSpace(s) ? null : s;
//    }
//}