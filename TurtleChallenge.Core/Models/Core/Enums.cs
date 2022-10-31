using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleChallenge.Core.Models;
public enum Direction
{
    North,
    South,
    West,
    East
}

public enum MoveType
{
    None,
    /// <summary>Rotate</summary>
    R,
    /// <summary>Move</summary>
    M
}

public enum ObjectTypes
{
    Default,
    Turtle,
    Mine,
    Exit,
    Board
}
