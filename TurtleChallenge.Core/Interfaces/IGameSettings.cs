using TurtleChallenge.Core.Models;

namespace TurtleChallenge.Core.Interfaces;
public interface IGameSettings
{
    public string FileLocation { get; }
    public IVector2 BoardSize{ get; }
    public IVector2 StartCoodinates { get; }
    public Direction StartDirection { get; }
    public ICollection<IVector2> Mines { get; }
    public IVector2 ExitCoordinates { get; }
}
