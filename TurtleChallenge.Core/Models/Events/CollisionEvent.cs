using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleChallenge.Core.Models;
public class CollisionEvent : GameObjectEvent
{
    /// <summary>
    /// The GameObject you are colliding with.
    /// </summary>
    public GameObject Target { get; set; }

    public CollisionEvent(GameObject gameObject) : base(gameObject)
    {
        Target = gameObject;
    }
}