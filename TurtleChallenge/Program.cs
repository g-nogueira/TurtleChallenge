//// See https://aka.ms/new-console-template for more information

using TurtleChallenge.Core.Models;

GameSettings gameSettings = GameSettings.GetInstance(new UI().RequestSettingsFile());
MatchMoves matchMoves = MatchMoves.GetInstance(new UI().RequestMovesFile());

Game.GetInstance(gameSettings).StartMatch(matchMoves);