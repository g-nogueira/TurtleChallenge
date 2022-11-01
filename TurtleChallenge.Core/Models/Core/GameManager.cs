using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleChallenge.Core.Models;
/// <summary>
/// Core class responsible for managing GameObjects' interaction between one another. This is the core of a running game.
/// </summary>
public class GameManager
{
    /// <summary>
    /// The list of GameObjects this manager is responsible for.
    /// </summary>
    public ICollection<GameObject> GameObjects { get; private set; } = new HashSet<GameObject>();

    public GameManager() {}

    /// <summary>
    /// Adds a GameObject to the GameManager's responsibility. Any object used by the running game should be under a GameManager.
    /// </summary>
    /// <param name="gameObject"></param>
    public void AddObject(GameObject gameObject)
    {
        GameObjects.Add(gameObject);
        gameObject.PositionChange += OnPositionChange;
    }

    /// <summary>
    /// Adds a list of GameObjects to the GameManager's responsibility. Any object used by the running game should be under a GameManager.
    /// </summary>
    /// <param name="gameObject"></param>
    public void AddObject(IEnumerable<GameObject> gameObjects)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            AddObject(gameObject);
        }
    }

    /// <summary>
    /// Resets the state of the GameManager to it's initial state, removing any GameObject from it's responsibility.
    /// This is ideal for starting a new instance or match of a game.
    /// </summary>
    public void Reset()
    {
        GameObjects.Clear();
    }

    /// <summary>
    /// Detects wether a given GameObject is colliding with another and triggers the OnCollisionEnter event of the GameObjects.
    /// </summary>
    /// <param name="e"></param>
    private void OnPositionChange(GameObjectEvent e) {

        // Two GameObjects are colliding when their Position are the same.
        var collidingObjects = GameObjects.Where(obj => obj.Position.Equals(e.GameObject.Position) && obj != e.GameObject);
        
        foreach (var obj in collidingObjects)
        {
            // Triggers the event for both colliding objects.
            e.GameObject.OnCollisionEnter(new CollisionEvent(obj));
            obj.OnCollisionEnter(new CollisionEvent(e.GameObject));
        }
    }

}
