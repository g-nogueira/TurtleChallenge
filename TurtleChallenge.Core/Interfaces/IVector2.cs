namespace TurtleChallenge.Core.Interfaces;
public interface IVector2
{
    public int X { get; set; }
    public int Y { get; set; }

    public bool Equals(IVector2 coordinate);

    public bool IsBetween(IVector2 lineStart, IVector2 lineEnd);

    public IVector2 Clone();
}
