namespace TurtleChallenge.Core.Interfaces;
/// <summary>
/// Interface for representation of 2D vectors and points.
/// </summary>
public interface IVector2
{
    /// <summary>
    /// X component of the vector.
    /// </summary>
    public int X { get; set; }
    /// <summary>
    /// Y component of the vector.
    /// </summary>
    public int Y { get; set; }

    /// <summary>
    /// Returns true if the given vector is exactly equal to this vector.
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public bool Equals(IVector2 coordinate);

    /// <summary>
    /// Returns true if the current Vector is between two points. I.e., pertains to the same line.
    /// </summary>
    /// <param name="lineStart"></param>
    /// <param name="lineEnd"></param>
    /// <returns></returns>
    public bool IsBetween(IVector2 lineStart, IVector2 lineEnd);

    /// <summary>
    /// Clones this Vector2 into a new instance,
    /// </summary>
    /// <returns></returns>
    public IVector2 Clone();

}
