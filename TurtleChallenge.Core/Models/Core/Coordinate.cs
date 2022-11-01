using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.Core.Interfaces;

namespace TurtleChallenge.Core.Models;
public class Coordinate : ICoordinate
{
    public int X { get; set; }
    public int Y { get; set; }

    public Coordinate(int x, int y)
    {
        X = x; Y = y;
    }

    /// <summary>
    /// Creates a Coordinate instance given a string in the X,Y format.
    /// </summary>
    /// <param name="coordinate">A string in the format X,Y. E.g., "2,4"</param>
    /// <exception cref="Exception"></exception>
    public Coordinate(string coordinate)
    {
        try
        {
            X = int.Parse(coordinate.Split(',')[0]);
            Y = int.Parse(coordinate.Split(',')[1]);
        }
        catch (FormatException e)
        {

            throw new Exception($"The given coordinate has no valid format. The value received is {coordinate}");
        }
    }

    public bool Equals(ICoordinate coordinate)
    {
        return this.X == coordinate.X && this.Y == coordinate.Y;
    }

    /// <summary>
    /// Evaluates wether the current coordinates is between two points. I.e., pertains to the same line.
    /// </summary>
    /// <remarks>
    /// The code used in the method is highly on a StackOverflow thread.
    /// See more on <see href="https://stackoverflow.com/questions/328107/how-can-you-determine-a-point-is-between-two-other-points-on-a-line-segment"/>.
    /// </remarks>
    /// <param name="lineStart"></param>
    /// <param name="lineEnd"></param>
    /// <returns></returns>
    public bool IsBetween(ICoordinate lineStart, ICoordinate lineEnd)
    {
        var a = lineStart;
        var b = this;
        var c = lineEnd;

        // https://en.wikipedia.org/wiki/Cross_product
        var crossProduct = (c.Y - a.Y) * (b.X - a.X) - (c.X - a.X) * (b.Y - a.Y);

        if (crossProduct != 0) return false;

        // See https://en.wikipedia.org/wiki/Dot_product
        var dotProduct = (c.X - a.X) * (b.X - a.X) + (c.Y - a.Y) * (b.Y - a.Y);
        if (dotProduct < 0) return false;

        var squaredLengthBA = Math.Pow(b.X - a.X , 2) + Math.Pow(b.Y - a.Y, 2);
        if (dotProduct > squaredLengthBA) return false;

        return true;
    }

    public ICoordinate Clone()
    {
        return new Coordinate(this.X, this.Y);
    }
}
