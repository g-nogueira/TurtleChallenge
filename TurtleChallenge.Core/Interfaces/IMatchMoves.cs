using TurtleChallenge.Core.Models;

namespace TurtleChallenge.Core.Interfaces;
public interface IMatchMoves
{
    public IEnumerable<MoveType> Moves { get; set; }
}
