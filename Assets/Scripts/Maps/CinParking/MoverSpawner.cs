using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// This classe is reponsible for spawning objects with the Move script attached to it.
/// It spawns a mover, and them make it moves for a targetWaypoint, point by point according to the
///	path that is made by it's previous waypoints. *See Waypoint class for more info.
///
///	How to use it: Put it into a gameobject and fill the objects list with what you want to spawn.
///	Then, you just have to create a path connecting waypoints previous variables and drag the last waypoint, the target, to
///	the targetWaypoint variable. That's where the mover's will go once spawned, following the path you made.
/// 
/// Developed by: Higor
/// </summary>
public class MoverSpawner : MonoBehaviour
{
    public List<GameObject> objects;
    public int spawnEverySeconds = 10;
    public GameObject targetWaypoint;

    [HideInInspector]
    public int spawnedAmount;

    protected float spawnTime;
    protected float spawn;

	protected virtual void Start ()
    {
        spawn = spawnEverySeconds + Random.Range(-spawnEverySeconds / 2f, spawnEverySeconds / 2f);
	}

    protected virtual void Update ()
    {
        spawnTime += Time.deltaTime;

        if (spawnTime > spawn)
            Spawn();
	}

	/// <summary>
	/// Set a path by backtracking waypoints and adds it to a move script
	/// </summary>
	/// <param name="move">Move.</param>
    protected void GoForTargetWaypoint(Move move)
    {
        List<Vector2> path = new List<Vector2>();
        GameObject current = targetWaypoint;

        while(current != null)
        {
            path.Add(new Vector2(current.transform.position.x, current.transform.position.y));
            current = current.GetComponent<Waypoint>().previous;
        }

        for (int i = path.Count - 1; i >= 0; i--)
            move.addPoint(path[i]);
    }

	/// <summary>
	/// Spawns a random mover object and moves it to it's target waypoint
	/// </summary>
    protected virtual void Spawn()
    {
        int r = Random.Range(0, objects.Count);
        GameObject newObject = Instantiate(objects[r], transform.position, Quaternion.identity) as GameObject;
        newObject.GetComponent<Mover>().spawner = this;
        newObject.transform.parent = this.transform;
        Move move = newObject.GetComponent<Move>();
        GoForTargetWaypoint(move);
        spawn = spawnEverySeconds + Random.Range(-spawnEverySeconds / 2f, spawnEverySeconds / 2f);
        spawnTime = 0;
    }
}
