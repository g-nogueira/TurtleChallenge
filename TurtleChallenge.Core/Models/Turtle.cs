using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.Core.Interfaces;

namespace TurtleChallenge.Core.Models;
public class Turtle : GameObject
{
    public Turtle(ICoordinate position)
    {
        Position = position;
        Tag = ObjectTypes.Turtle.ToString();
    }

    public Turtle(ICoordinate position, Direction direction)
    {
        Position = position;
        Direction = direction;
        Tag = ObjectTypes.Turtle.ToString();
    }

    public override string UI()
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