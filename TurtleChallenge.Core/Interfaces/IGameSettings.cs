using TurtleChallenge.Core.Models;

namespace TurtleChallenge.Core.Interfaces;
public interface IGameSettings
{
    public string FileLocation { get; }
    public ICoordinate BoardSize{ get; set; }
    public ICoordinate StartPosition { get; set; }
    public Direction StartDirection { get; set; }
    public ICollection<ICoordinate> Mines { get; set; }
    public ICoordinate ExitPosition { get; set; }
}
