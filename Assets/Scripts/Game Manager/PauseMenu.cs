using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
	/*
    *   Developed by: Torres (phtg)
    *   Description: It's the pause menu. When user presses 'ESC', player is frozen and the menu appears. 
    * 				Player is able to select the options with the mouse. 
    *   How to use it: This class carries two important prefabs: Canvas and EventSystem. Both are really important
    * 					for the canvas to work. This script is attached to GameManager. When player presses 'ESC' the
    * 					game object of the Canvas is loaded together with the EventSystem.
    * 					All the changes in the menu itself must be made in Unity selecting the Canvas prefab (Pause Canvas).
    * 					DO NOT do anything hardcoded! Functions here are the actions that will be called by each button on the
    * 					menu. In Unity, select the button and go to OnClick() and select the function you want.
    */

//	public GUIStyle style;
//	public GUIStyle style2;

	public GameObject canvasPrefab;
	public GameObject eventSystemPrefab;

	private GameObject canvas;
	private GameObject eventSystem;

	private bool paused = false;

	void Start () {
		
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (paused) {
				GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().OnPause (false);
				Destroy (canvas);
				Destroy (eventSystem);
				paused = false;
			} else {
				GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().OnPause (true);
				paused = true;
				canvas = Instantiate (canvasPrefab);
				eventSystem = Instantiate (eventSystemPrefab);

			}
		}
	}

	public void quitGame() {
		Application.Quit ();
	}
}
