using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.Core.Interfaces;

namespace TurtleChallenge.Core.Models;
public class Turtle : GameObject
{
    public Turtle(IVector2 position)
    {
        Position = position;
        Tag = ObjectTypes.Turtle.ToString();
        Color = ConsoleColor.White;
    }

    public Turtle(IVector2 position, Direction direction)
    {
        Position = position;
        Direction = direction;
        Tag = ObjectTypes.Turtle.ToString();
        Color = ConsoleColor.White;
    }

    public override string ToString()
    {
        switch (Direction)
        {
            case Direction.North:
                return "▲";
            case Direction.South:
                return "▼";
            case Direction.West:
                return "◄";
            case Direction.East:
                return "►";
            default:
                break;
        }
        return "";
    }
}