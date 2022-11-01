using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleChallenge.Core.Models;

/// <summary>
/// Represents the possible Directions a GameObject can face. A GameObject will only move forward to the direction it's pointing.
/// </summary>
public enum Direction
{
    North,
    South,
    West,
    East
}

/// <summary>
/// Represents the possible movements a GameObject can make when changing its <see cref="Position"/> or <see cref="Direction"/>.
/// </summary>
public enum MoveType
{
    /// <summary>Nothing</summary>
    None,
    /// <summary>Rotate</summary>
    R,
    /// <summary>Move</summary>
    M
}

/// <summary>
/// Represents the current existing GameObjects.
/// </summary>
public enum ObjectTypes
{
    Default,
    /// <summary>The Turtle is usually used as the player.</summary>
    Turtle,
    /// <summary>The Mine is usually used as an lost end game.</summary>
    Mine,
    /// <summary>The Exit is usually used as an win end game.</summary>
    Exit,
    /// <summary>The Board is used as a background image for the game.</summary>
    Board
}
