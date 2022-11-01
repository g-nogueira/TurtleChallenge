namespace TurtleChallenge.Core.Interfaces;
public interface ICoordinate
{
    public int X { get; set; }
    public int Y { get; set; }

    public bool Equals(ICoordinate coordinate);

    /// <summary>
    /// Evaluates wether the current coordinates is between two points. I.e., pertains to the same line.
    /// </summary>
    /// <param name="lineStart"></param>
    /// <param name="lineEnd"></param>
    /// <returns></returns>
    public bool IsBetween(ICoordinate lineStart, ICoordinate lineEnd);


    public ICoordinate Clone();
}
