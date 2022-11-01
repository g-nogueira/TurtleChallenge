using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.Core.Interfaces;

namespace TurtleChallenge.Core.Models;

/// <summary>
/// Base class for any event raised by a GameObject.
/// </summary>
public class GameObjectEvent : EventArgs
{
    /// <summary>
    /// The GameObject who raised the event.
    /// </summary>
    public GameObject GameObject { get; set; }

    public GameObjectEvent(GameObject gameObject)
    {
        GameObject = gameObject;
    }
}
