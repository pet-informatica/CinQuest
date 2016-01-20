﻿using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour 
{

	/*
		Developed by: Higor

		Description: This script is responsible for changing from one scene to another, making a FadeIn/FadeOut effect
		in the process.

		How to use it: Create a prefab with a single BoxCollider2D and attach the script to it. Fill the "destinyScene" variable
        name with the scene you want to go, and name the prefab with exactly the same name. This is important. Then, created a new empty
        gameobject, name it "Spawn" and set it as children of the SceneChanger main prefab. Change the position of the "Spawn
        for setting exaclty where the player will appear when he get's back for this scene. It's generally in front on the
        doors. But be careful not to put it too close for the SceneChanger main prefab BoxCollider2D, because if it happens
        the player will most likely trigger it as soon as he appears in the scene, and we don't want this! 
        *Please see the PlayerSpawn script for more information.
		
	 */

	public string destinyScene = "";
	public bool locked = false;

	IEnumerator ChangeScene()
	{
		//To make the fade in, we simple call the ScreenFader script, present in the GameManager object, to start the fade.
		//*See the ScreenFader script and the GameManger script for more information.
		float fadeTime = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ScreenFader> ().BeginFade (1);
		//Then wait until the fade is over
		yield return new WaitForSeconds (1);
		//And finally change the scene
		PlayerSpawn.SetTarget(Application.loadedLevelName);

		Application.LoadLevel (destinyScene);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(!locked)
			StartCoroutine (ChangeScene());
	}
}
