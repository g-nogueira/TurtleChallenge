using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.Core.Interfaces;

namespace TurtleChallenge.Core.Models;

public delegate void PositionChanged(GameObjectEvent e);
public delegate void Directionhanged(GameObjectEvent e);
public delegate void CollisionEntered(CollisionEvent e);

/// <summary>
/// Base class for all game entities.
/// </summary>
public class GameObject
{
    /// <summary>
    /// The arbitrary tag of this game object.
    /// </summary>
    public string? Tag { get; set; }
    /// <summary>
    /// The size of the object.
    /// </summary>
    public IVector2 Size { get; set; } = new Vector2(1, 1);
    /// <summary>
    /// The 2D word space position of the GameObject.
    /// </suDmmary>
    public IVector2 Position { get; set; } = new Vector2(0, 0);
    /// <summary>
    /// The direction the GameObject is facing.
    /// </summary>
    public Direction Direction { get; set; } = Direction.North;

    /// <summary>
    /// The flag that controls wether the GameObject is visible to the UI.
    /// </summary>
    public bool IsVisible { get; set; } = true;

    public event CollisionEntered? CollisionEnter;
    public event PositionChanged? PositionChange;
    public event Directionhanged? RotationChange;

    /// <summary>
    /// Moves the game object one step in the current <see cref="Direction"/>.
    /// </summary>
    public void Move()
    {
        switch (Direction)
        {
            case Direction.North:
                Position.Y -= 1;
                break;
            case Direction.South:
                Position.Y += 1;
                break;
            case Direction.West:
                Position.X -= 1;
                break;
            case Direction.East:
                Position.X += 1;
                break;
            default:
                break;
        }

        OnPositionChange(new GameObjectEvent(this));
    }

    /// <summary>
    /// Sets the rotation of the GameObject to a given <see cref="Direction"/>.
    /// </summary>
    /// <param name="direction"></param>
    public void Rotate(Direction direction)
    {
        Direction = direction;
    }

    /// <summary>
    /// Rotates the GameObject one step clockwise.
    /// </summary>
    public void Rotate()
    {
        switch (Direction)
        {
            case Direction.North:
                Direction = Direction.East;
                break;
            case Direction.South:
                Direction = Direction.West;
                break;
            case Direction.West:
                Direction = Direction.North;
                break;
            case Direction.East:
                Direction = Direction.South;
                break;
            default:
                return;
        }

        OnDirectionChanged(new GameObjectEvent(this));
    }

    /// <summary>
    /// OnCollisionEnter is called when this GameObject has the same <see cref="Vector2">Position</see> as another GameObject.
    /// </summary>
    /// <param name="e"></param>
    public virtual void OnCollisionEnter(CollisionEvent e)
    {
        CollisionEnter?.Invoke(e);
    }

    /// <summary>
    /// OnPositionChange is called when this GameObject has its <see cref="Vector2">Position</see> changed.
    /// </summary>
    /// <param name="e"></param>
    public virtual void OnPositionChange(GameObjectEvent e)
    {
        PositionChange?.Invoke(e);
    }

    /// <summary>
    /// OnRotationChanged is called when this GameObject has its <see cref="Direction">Direction</see> changed.
    /// </summary>
    /// <param name="e"></param>
    public virtual void OnDirectionChanged(GameObjectEvent e)
    {
        RotationChange?.Invoke(e);
    }

    public virtual string UI()
    {
        return "-";
    }

}

