//// See https://aka.ms/new-console-template for more information

using TurtleChallenge.Core.Interfaces;
using TurtleChallenge.Core.Models;

public class MatchMoves : LoadableFromFile<MatchMoves>, IMatchMoves
{
    public IEnumerable<MoveType> Moves { get; set; } = new List<MoveType>();

    public MatchMoves(string fileLocation) : base(fileLocation){}

    /// <summary>
    /// Returns an Instance of <see cref="GameSettings"/> given a settings file.
    /// </summary>
    /// <param name="fileLocation"></param>
    /// <returns></returns>
    public static MatchMoves GetInstance(string fileLocation)
    {
        var instance = new MatchMoves(fileLocation ?? Defaults.MovesLocations);

        instance.Moves = instance.ReadCSVFile(fileLocation).Select(move => Enum.TryParse<MoveType>(move, true, out var moveType) ? moveType : MoveType.None);

        return instance;
    }
}