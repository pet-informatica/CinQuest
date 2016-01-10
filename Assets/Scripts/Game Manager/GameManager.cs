using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour 
{
	/*
		Developed by: Higor

		Description: The GameManager is a prefab responsible for storing information about
		all the quests and overall game progress. To do it, we call DontDestroyOnLoad() for
		keeping it alive across the scenes. However, there must never be more than one instance
		of the GameManager on the game, that's why we make it be a singleton. A singleton is an object
		that is instantiated once the game starts, and then never again.

		How to use it: Attach it to the GameManger prefab. But you can't just drag the GameManager prefab on the scenes, because of it's
		singleton behavior. You have to instantiate him on the first scenes, but using a script, the so called Loader.
		*See the Loader script for more information
		
	 */

	public static GameManager instance = null;

	void Awake () 
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
		DontDestroyOnLoad (gameObject);
	}

	void Update () 
	{
	
	}
}
