using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

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

	public GameObject pauseCanvas;
	public GameObject controlCanvas;
	public GameObject feedbackCanvas;
	private Stack<GameObject> allCanvas;

	static PauseMenu instance;

	void Start() {
		instance = this;

		allCanvas = new Stack<GameObject>();
		allCanvas.Push(pauseCanvas);
	}

	void Update () {
		if (Input.GetButtonDown ("Pause")) {
			if (pauseCanvas.activeSelf) {
				allCanvas.Pop ().SetActive (false);
			} else {
				pauseCanvas.SetActive (true);
				allCanvas.Push (pauseCanvas);
			}
		}
	}

	public static PauseMenu Instance
	{
		get { return instance; }
	}

	public void openControl() {
		controlCanvas.SetActive (true);
		allCanvas.Push (controlCanvas);
		GameObject.Find ("upButton").GetComponent<Text> ().text = "W";
		GameObject.Find ("downButton").GetComponent<Text> ().text = "S";
		GameObject.Find ("leftButton").GetComponent<Text> ().text = "A";
		GameObject.Find ("rightButton").GetComponent<Text> ().text = "D";
		GameObject.Find ("runButton").GetComponent<Text> ().text = "LEFT SHIFT";
		GameObject.Find ("inventoryButton").GetComponent<Text> ().text = "Q";
		GameObject.Find ("pauseButton").GetComponent<Text> ().text = "P";
	}

	public void openFeedback(){
		feedbackCanvas.SetActive (true);
		allCanvas.Push (feedbackCanvas);
	}

	public void quitGame() {
		gameObject.AddComponent<SceneChanger>();
		SceneChanger sceneChanger = GetComponent<SceneChanger>();
		sceneChanger.destinyScene = "GameOpening";
		sceneChanger.Change();
		pauseCanvas.SetActive (false);
	}

	public void CloseFeedback() {
		this.allCanvas.Pop ().SetActive (false);
	}

	void OnDisable() {
		if(	GameObject.FindGameObjectWithTag ("Player") != null)
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().OnPause (false);
	}

	void OnEnable() {
		if(	GameObject.FindGameObjectWithTag ("Player") != null)
			GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ().OnPause (false);
	}
}
