using UnityEngine;
using System.Collections;

/// <summary>
/// Specializes the NPCMover class allowing the user to set a path for the player to
/// automatically move through as soon as the script starts.
/// </summary>
public class NPCMoverAuto : NPCMover 
{
	public GameObject targetWaypoint;

	void Start()
	{
		GoForTargetWaypoint (targetWaypoint);
	}
}
