using UnityEngine;
using System.Collections;

public class PlayerSpawn : MonoBehaviour 
{
	/*

		Developed by: Higor

		Description: This script is responsible for keeping track of the player positions when it is changing between scenes.
		For example: you are in the Cin, and the you enter a teacher room. The game scene will change from Cin to Room.
		When you leave the room, you obviously wanna get back in front of the Room door in the Cin. But this is not
		going to happen, because the player will be teleported for the position he is originally set up in the scene. So, when the player
		enters the room, we must save the room door position and when it leaves, manually move the player for that position. That's what
		this script does.

		How to use it: It must be attached to the GameManager prefab.
	
	 */

	static Vector3 target;
	static string level;

	/// <summary>
	/// Sets the current position the player must appear when he get's back for this scene
	/// </summary>
	/// <param name="currentPosition">The position the player is before leaving the scene</param>
	/// <param name="currentLevel">The scene he is leaving</param>
	public static void SetTarget(Vector3 currentPosition, string currentLevel)
	{
		target = currentPosition;
		level = currentLevel;
	}

	void OnLevelWasLoaded()
	{
		if (Application.loadedLevelName == level && target != Vector3.zero) 
		{
			Transform player = GameObject.FindGameObjectWithTag ("Player").transform;
			player.position = new Vector3 (target.x, target.y, player.position.z);
			Camera.main.transform.position = new Vector3 (target.x, target.y, Camera.main.transform.position.z);
			target = Vector3.zero;
			level = null;
		}
	}
}
