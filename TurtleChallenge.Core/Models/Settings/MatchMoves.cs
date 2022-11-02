//// See https://aka.ms/new-console-template for more information

using TurtleChallenge.Core.Interfaces;
using TurtleChallenge.Utils;

namespace TurtleChallenge.Core.Models;

/// <summary>
/// Class representing a MatchMoves.csv file.
/// </summary>
public class MatchMoves : IMatchMoves
{
    public IEnumerable<MoveType> Moves { get; set; }

    public MatchMoves(string fileLocation = Defaults.MovesLocations)
    {
        var fileReader = new FileReader(fileLocation);

        Moves = fileReader
            .CSV()
            .Select(move => Enum.TryParse<MoveType>(move, true, out var moveType) ? moveType : MoveType.None);

    }

    /// <summary>
    /// Returns an Instance of <see cref="GameSettings"/> given a settings file.
    /// </summary>
    /// <param name="fileLocation"></param>
    /// <returns></returns>
    public static MatchMoves GetInstance(string fileLocation)
    {
        return new MatchMoves(fileLocation);
    }
}