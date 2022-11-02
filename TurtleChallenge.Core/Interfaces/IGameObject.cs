using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.Core.Interfaces;
using TurtleChallenge.Core.Models;

namespace TurtleChallenge.Core.Interfaces;

/// <summary>
/// Interface that any GameObject should follow.
/// </summary>
public interface IGameObject
{
    /// <summary>
    /// The arbitrary tag of this game object.
    /// </summary>
    public string? Tag { get; set; }
    /// <summary>
    /// The size of the object.
    /// </summary>
    public IVector2 Size { get; set; }
    /// <summary>
    /// The 2D word space position of the GameObject.
    /// </summary>
    public IVector2 Position { get; set; }
    /// <summary>
    /// The 2D offset to consider when moving the GameObject.
    /// </summary>
    public IVector2 MoveStepSize { get; set; }
    /// <summary>
    /// The direction the GameObject is facing.
    /// </summary>
    public Direction Direction { get; set; }

    /// <summary>
    /// The flag that controls wether the GameObject is visible to the UI.
    /// </summary>
    public bool IsVisible { get; set; }

    public event CollisionEntered? CollisionEnter;
    public event PositionChanged? PositionChange;
    public event Directionhanged? RotationChange;

    /// <summary>
    /// Moves the game object one step in the current <see cref="Direction"/>.
    /// </summary>
    public void Move();


    /// <summary>
    /// Sets the rotation of the GameObject to a given <see cref="Direction"/>.
    /// </summary>
    /// <param name="direction"></param>
    public void Rotate(Direction direction);

    /// <summary>
    /// Rotates the GameObject one step clockwise.
    /// </summary>
    public void Rotate();
}

