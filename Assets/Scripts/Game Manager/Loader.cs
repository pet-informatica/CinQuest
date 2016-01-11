using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour 
{
	/* Developed by: Higor
	 
		Description: This scripts instantiates the GameManager, in case it isn't alredy instantiated.
		How to use it: Just attach it to the main camera, because it will be present in every scene.
		Then, drag the GameManager prefab to the public variable field;
		*See the GameManager script for more information.
	  
	*/

	public GameObject gameManager;

	void Awake () 
	{
		if (GameManager.instance == null)
			Instantiate (gameManager);
	}
}
