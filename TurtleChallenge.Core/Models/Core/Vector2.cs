using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.Core.Interfaces;

namespace TurtleChallenge.Core.Models;
/// <summary>
/// Representation of 2D vectors and points.
/// </summary>
public class Vector2 : IVector2
{
    /// <summary>
    /// X component of the vector.
    /// </summary>
    public int X { get; set; }
    /// <summary>
    /// Y component of the vector.
    /// </summary>
    public int Y { get; set; }

    public Vector2(int x, int y)
    {
        X = x; Y = y;
    }

    public Vector2(string x, string y)
    {
        X = int.Parse(x); Y = int.Parse(y);
    }

    /// <summary>
    /// Creates a Vector2 instance given a string in the X,Y format.
    /// </summary>
    /// <param name="vector2String">A string in the format X,Y. E.g., "2,4"</param>
    /// <exception cref="Exception"></exception>
    public Vector2(string vector2String)
    {
        try
        {
            X = int.Parse(vector2String.Split(',')[0]);
            Y = int.Parse(vector2String.Split(',')[1]);
        }
        catch (FormatException e)
        {

            throw new Exception($"The given coordinate has no valid format. The value received is {vector2String}");
        }
    }

    /// <summary>
    /// Returns true if the given vector is exactly equal to this vector.
    /// </summary>
    /// <param name="vector"></param>
    /// <returns></returns>
    public bool Equals(IVector2 vector)
    {
        return X == vector.X && Y == vector.Y;
    }

    /// <summary>
    /// Returns true if the current Vector is between two points. I.e., pertains to the same line.
    /// </summary>
    /// <remarks>
    /// The code used in the method is highly based on a StackOverflow thread.
    /// See more on <see href="https://stackoverflow.com/questions/328107/how-can-you-determine-a-point-is-between-two-other-points-on-a-line-segment"/>.
    /// </remarks>
    /// <param name="lineStart"></param>
    /// <param name="lineEnd"></param>
    /// <returns></returns>
    public bool IsBetween(IVector2 lineStart, IVector2 lineEnd)
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

    /// <summary>
    /// Clones this Vector2 into a new instance,
    /// </summary>
    /// <returns></returns>
    public IVector2 Clone()
    {
        return new Vector2(X, Y);
    }

    public static Vector2 operator /(Vector2 a, int b) => new(a.X / b, a.Y / b);
    public static Vector2 operator +(Vector2 a, Vector2 b) => new(a.X + b.X, a.Y + b.Y);
    public static Vector2 operator +(Vector2 a, int b) => new(a.X + b, a.Y + b);
    public static Vector2 operator *(Vector2 a, Vector2 b) => new(a.X * b.X, a.Y * b.Y);
    public static Vector2 operator -(Vector2 a, Vector2 b) => new(a.X - b.X, a.Y - b.Y);
}
