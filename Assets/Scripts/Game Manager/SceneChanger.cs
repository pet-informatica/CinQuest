using UnityEngine;
using System.Collections;

public class SceneChanger : MonoBehaviour 
{

	/*
		Developed by: Higor

		Description: This script is responsible for changing from one scene to another, making a FadeIn/FadeOut effect
		in the process.

		How to use it: Create a prefab with a single BoxCollider2D and attach the script to it. Fill the destinyScene variable
		and that's it. When the player trigger it, it will change the scene. The savePosition variable is important. If you,
		for example, enter in a room, and then leave it, you will probably want your player to be next to the door when it leaves
		the room. For this, we shall change it's position for in front of the door. So, if you want that to happen, savePosition must be true.
		
	 */

	public string destinyScene = "";
	public bool savePosition = false;
	public bool locked = false;

	IEnumerator ChangeScene()
	{
		//To make the fade in, we simple call the ScreenFader script, present in the GameManager object, to start the fade.
		//*See the ScreenFader script and the GameManger script for more information.
		float fadeTime = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<ScreenFader> ().BeginFade (1);
		//Then wait until the fade is over
		yield return new WaitForSeconds (fadeTime);
		//And finally change the scene
		if(savePosition)
			PlayerSpawn.SetTarget(this.transform.position, Application.loadedLevelName);

		Application.LoadLevel (destinyScene);
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(!locked)
			StartCoroutine (ChangeScene());
	}
}
