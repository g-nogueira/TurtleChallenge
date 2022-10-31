using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.Core.Interfaces;

namespace TurtleChallenge.Core.Models;

public delegate void PositionChanged(GameObjectEvent e);
public delegate void RotationChanged(GameObjectEvent e);
public delegate void CollisionEntered(CollisionEvent e);
public class GameObject
{
    /// <summary>
    /// A string that can be arbitrarily used.
    /// </summary>
    public string? Tag { get; set; }
    public ICoordinate Size { get; set; } = new Coordinate(1, 1);
    public ICoordinate Position { get; set; } = new Coordinate(0, 0);
    public Direction Direction { get; set; } = Direction.North;
    public bool IsVisible { get; set; } = true;

    public event CollisionEntered? CollisionEnter;
    public event PositionChanged? PositionChange;
    public event RotationChanged? RotationChange;

    /// <summary>
    /// Moves the game object one step to the current <see cref="Direction"/>.
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
    /// Sets the rotation of the object to a given direction.
    /// </summary>
    /// <param name="direction"></param>
    public void Rotate(Direction direction)
    {
        Direction = direction;
    }

    /// <summary>
    /// Rotates the game object one step clockwise.
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

        OnRotationChanged(new GameObjectEvent(this));
    }

    /// <summary>
    /// Event raised when a collision is detected.
    /// </summary>
    /// <param name="e"></param>
    public virtual void OnCollisionEnter(CollisionEvent e)
    {
        CollisionEnter?.Invoke(e);
    }

    public virtual void OnPositionChange(GameObjectEvent e)
    {
        PositionChange?.Invoke(e);
    }

    public virtual void OnRotationChanged(GameObjectEvent e)
    {
        RotationChange?.Invoke(e);
    }

    public virtual string UI()
    {
        return "-";
    }

}

