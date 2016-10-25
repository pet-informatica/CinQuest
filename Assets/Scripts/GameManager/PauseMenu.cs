using UnityEngine;
using UnityEngine.UI;
using AssemblyCSharp;

public class PauseMenu : MonoBehaviour {
	/*
    *   Developed by: Torres (phtg)
    *   Description: It's the pause menu. When user presses 'P', player is frozen and the menu appears. 
    * 				Player is able to select the options with the mouse. 
    *   How to use it: This class carries the Pause Canvas and all its children. This script is attached to GameManager. 
    * 					When player presses 'P' the game object of the Canvas is loaded.
    * 					All the changes in the menu itself must be made in Unity selecting the Canvas prefab (Pause Canvas).
    * 					DO NOT do anything hardcoded! Functions here are the actions that will be called by each button on the
    * 					menu. In Unity, select the button and go to OnClick() and select the function you want.
    */

	private GameObject pauseCanvas;
	private GameObject controlCanvas;
	private GameObject feedbackCanvas;
	private GameObject[] allCanvas;
	private MenuStatus menuStatus;


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

		// pauseCanvas must be in the last position!
		allCanvas = new GameObject[] {feedbackCanvas, controlCanvas, pauseCanvas};

		menuStatus = GameManager.Instance.menuStatus;
	}

	void Update () {

		if (Input.GetButtonDown ("Pause") && !menuStatus.openProblem("Pause")) {
			if (allCanvas [allCanvas.Length - 1].activeSelf) {
				foreach (GameObject canvas in allCanvas) {
					if (canvas.activeSelf) {
						canvas.SetActive (false);
						menuStatus.close ("Pause");
						break;
					}
				}
			} else {
				GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().OnPause (true);
				pauseCanvas.SetActive (true);
				menuStatus.open ("Pause");
			}
		}
	}

	public void openControl() {
		controlCanvas.SetActive (true);
		GameObject.Find ("upButton").GetComponent<Text> ().text = "W";
		GameObject.Find ("downButton").GetComponent<Text> ().text = "S";
		GameObject.Find ("leftButton").GetComponent<Text> ().text = "A";
		GameObject.Find ("rightButton").GetComponent<Text> ().text = "D";
		GameObject.Find ("runButton").GetComponent<Text> ().text = "LEFT SHIFT";
		GameObject.Find ("inventoryButton").GetComponent<Text> ().text = "Q";
		GameObject.Find ("pauseButton").GetComponent<Text> ().text = "P";
	}

	public void openFeedback(){
		menuStatus.open ("Feedback");
		feedbackCanvas.SetActive (true);
	}

	public void quitGame() {
		gameObject.AddComponent<SceneChanger>();
		SceneChanger sceneChanger = GetComponent<SceneChanger>();
		sceneChanger.destinyScene = "GameOpening";
		sceneChanger.Change();
		pauseCanvas.SetActive (false);
	}
		
}
