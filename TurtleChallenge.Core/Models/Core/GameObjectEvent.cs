using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurtleChallenge.Core.Interfaces;

namespace TurtleChallenge.Core.Models;
public class GameObjectEvent : EventArgs
{
    public GameObject GameObject { get; set; }

    public GameObjectEvent(GameObject gameObject)
    {
        GameObject = gameObject;
    }
}
