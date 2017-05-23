﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// This script is responsible for keeping track of the player positions when it is changing between scenes.
///	For example: you are in the Cin, and the you enter a teacher room. The game scene will change from Cin to Room.
///	When you leave the room, you obviously wanna get back in front of the Room door in the Cin. But this is not
///	going to happen, because the player will be teleported for the position he is originally set up in the scene. So,
///	when changing scenes, you save the name of the scene you are leaving. Then, when you enter the new scene, it searchs
///	for any "SceneChanger" object that is named like your leavingScene, get it's children object named "Spawn", wich must
//	be positioned on the position you want the player to spawn and teleport the player to it.
///
///	How to use it: It must be attached to the GameManager prefab.
/// 
/// Developed by: Higor
/// </summary>
public class PlayerSpawn : MonoBehaviour 
{
	static string leavingScene;

	/// <summary>
	/// Sets the current level the player is beforing moving for another scene
	/// </summary>
	/// <param name="currentLevel">The scene he is leaving</param>
	public static void SetTarget(string currentLevel)
	{
        leavingScene = currentLevel;
	}

	void OnEnable()
	{
		SceneManager.sceneLoaded += OnLevelFinishedLoading;
	}

	void OnDisable()
	{
		SceneManager.sceneLoaded -= OnLevelFinishedLoading;
	}
		
	void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode)
	{
		Transform target = null;
		if(GameObject.Find(leavingScene) != null) 
			target = GameObject.Find(leavingScene).GetComponent<Transform>();

		if(target != null)
		{
			Transform spawn = target.GetChild(0).GetComponent<Transform>();
			Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
			Vector3 playerPosition = new Vector3(spawn.position.x, spawn.position.y, player.position.z);
			Vector3 cameraPosition = new Vector3(spawn.position.x, spawn.position.y, Camera.main.transform.position.z);
			player.position = playerPosition;
			Camera.main.transform.position = cameraPosition;
		}
	}
}
