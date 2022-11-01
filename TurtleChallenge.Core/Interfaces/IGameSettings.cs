using TurtleChallenge.Core.Models;

namespace TurtleChallenge.Core.Interfaces;
public interface IGameSettings
{
    public string FileLocation { get; }
    public ICoordinate BoardSize{ get; }
    public ICoordinate StartPosition { get; }
    public Direction StartDirection { get; }
    public ICollection<ICoordinate> Mines { get; }
    public ICoordinate ExitPosition { get; }
}
