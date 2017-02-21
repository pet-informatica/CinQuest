using UnityEngine;
using System.Collections;

/// <summary>
/// Can be used to create a path, using the previous waypoint to build a chain. Scripts like
/// Move or NPCMover will receive the last waypoint of the path, and walk to it using the chain
/// of previous waypoints in reverse order.
/// </summary>
public class Waypoint : MonoBehaviour {

	public bool avaiable = true;
	public GameObject previous;

}
