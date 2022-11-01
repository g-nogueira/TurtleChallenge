using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TurtleChallenge.Core.Models;
public class GameManager
{
    public ICollection<GameObject> GameObjects { get; private set; } = new HashSet<GameObject>();

    public GameManager() {}

    public void AddObject(GameObject gameObject)
    {
        GameObjects.Add(gameObject);
        gameObject.PositionChange += OnPositionChange;
    }

    public void AddObject(IEnumerable<GameObject> gameObjects)
    {
        foreach (GameObject gameObject in gameObjects)
        {
            AddObject(gameObject);
        }
    }

    public void Reset()
    {
        GameObjects.Clear();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    private void OnPositionChange(GameObjectEvent e) {
        var collidingObjects = GameObjects.Where(obj => obj.Position.Equals(e.GameObject.Position) && obj != e.GameObject);
        
        foreach (var obj in collidingObjects)
        {
            e.GameObject.OnCollisionEnter(new CollisionEvent(obj));
        }
    }

}
