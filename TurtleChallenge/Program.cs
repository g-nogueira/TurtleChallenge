//// See https://aka.ms/new-console-template for more information

using TurtleChallenge.Core.Models;

//GameSettings gameSettings = GameSettings.GetInstance(UI.RequestSettingsFile());
//MatchMoves matchMoves = MatchMoves.GetInstance(UI.RequestMovesFile());

GameSettings gameSettings = GameSettings.GetInstance(@"C:\Projects\TurtleChallenge\GameSettings.ini");
MatchMoves matchMoves = MatchMoves.GetInstance(@"C:\Projects\TurtleChallenge\GameMoves.csv");

Game.GetInstance(gameSettings).StartMatch(matchMoves);