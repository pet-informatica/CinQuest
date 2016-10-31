using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Helper class that can move an NPC with the Move script attached.
/// Developed by: hcmb
/// </summary>
public class NPCMover : Move 
{
	/// <summary>
	/// Start walking the Move script to a target last waypoint, using it's previousw waypoints in order to build a path.
	/// </summary>
	/// <param name="lastWaypoint">Last waypoint to reach. The previous variables of the waypoints must be set to make a path.</param>
	public void GoForTargetWaypoint(GameObject lastWaypoint)
	{
		List<Vector2> path = new List<Vector2>();
		GameObject current = lastWaypoint;

		while(current != null)
		{
			path.Add(new Vector2(current.transform.position.x, current.transform.position.y));
			current = current.GetComponent<Waypoint>().previous;
		}

		for (int i = path.Count - 1; i >= 0; i--)
			addPoint(path[i]);
		
		anim.enabled = true;
		StartMoving ();
	}
}
