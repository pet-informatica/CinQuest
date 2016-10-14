using UnityEngine;
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

	private GameObject pauseCanvas;
	private GameObject controlCanvas;
	private GameObject feedbackCanvas;

	void Start() {
		pauseCanvas = GameObject.Find("Pause Canvas");
		if (pauseCanvas != null)
			pauseCanvas.SetActive (false);

		controlCanvas = GameObject.Find("Control Canvas");
		if(controlCanvas != null)
			controlCanvas.SetActive (false);
		
		feedbackCanvas = GameObject.Find("Feedback Canvas");
		if (feedbackCanvas != null)
			feedbackCanvas.SetActive (false);
	}

	void Update () {
		if (pauseCanvas != null && Input.GetButtonDown("Pause")) {
			if (pauseCanvas.activeSelf) {
				if (controlCanvas.activeSelf) {
					controlCanvas.SetActive (false);
				} else {
					GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().OnPause (false);
					pauseCanvas.SetActive (false);
				}
			} else {
				GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().OnPause (true);
				pauseCanvas.SetActive (true);
			}
		}
	}

	public void openControlOption() {
		GameObject.Find ("PauseManager").GetComponent<PauseMenu> ().openControl();
	}

	private void openControl() {
		controlCanvas.SetActive (true);
		GameObject.Find ("upButton").GetComponent<Text> ().text = "W";
		GameObject.Find ("downButton").GetComponent<Text> ().text = "S";
		GameObject.Find ("leftButton").GetComponent<Text> ().text = "A";
		GameObject.Find ("rightButton").GetComponent<Text> ().text = "D";
		GameObject.Find ("runButton").GetComponent<Text> ().text = "LEFT SHIFT";
		GameObject.Find ("inventoryButton").GetComponent<Text> ().text = "Q";
		GameObject.Find ("pauseButton").GetComponent<Text> ().text = "ESC";
	}

	public void openFeedbackOption() {
		GameObject.Find ("PauseManager").GetComponent<PauseMenu> ().openFeedback();
	}

	private void openFeedback(){
		feedbackCanvas.SetActive (true);
	}

	public void quitGameOption() {
		GameObject.Find ("PauseManager").GetComponent<PauseMenu> ().quitGame();
	}

	private void quitGame() {
		gameObject.AddComponent<SceneChanger>();
		SceneChanger sceneChanger = GetComponent<SceneChanger>();
		sceneChanger.destinyScene = "GameOpening";
		sceneChanger.Change();
	}
		
}
