using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

/// <summary>
/// It's the pause menu. When user presses 'P', player is frozen and the menu appears. 
/// Player is able to select the options with the mouse. 
/// 
/// How to use it: This class carries the Pause Canvas and all its children. This script is attached to GameManager. 
/// When player presses 'P' the game object of the Canvas is loaded.
/// All the changes in the menu itself must be made in Unity selecting the Canvas prefab (Pause Canvas).
/// DO NOT do anything hardcoded! Functions here are the actions that will be called by each button on the
/// menu. In Unity, select the button and go to OnClick() and select the function you want.
/// 
/// Developed by: Torres (phtg)
/// </summary>
public class PauseMenu : MonoBehaviour {
	public GameObject pauseCanvas;
	public GameObject controlCanvas;
	public GameObject feedbackCanvas;
	private Stack<GameObject> allCanvas;
	private MenuStatus menuStatus;

	static PauseMenu instance;

	void Start() {
		instance = this;
		menuStatus = GameManager.Instance.menuStatus;
		allCanvas = new Stack<GameObject>();
		allCanvas.Push(pauseCanvas);
	}

	void Update () {
		if (Input.GetButtonDown ("Pause") && !menuStatus.openProblem("Pause")) {
			if (pauseCanvas.activeSelf) {
				allCanvas.Pop ().SetActive (false);
				if(allCanvas.Count == 1)
					menuStatus.close ("Pause");
			} else {
				pauseCanvas.SetActive (true);
				menuStatus.open ("Pause");
				allCanvas.Push (pauseCanvas);
			}
		}
	}

	public static PauseMenu Instance
	{
		get { return instance; }
	}

	/// <summary>
	/// Opens the controls menu
	/// </summary>
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

	/// <summary>
	/// Opens the feedback menu
	/// </summary>
	public void openFeedback(){
		menuStatus.open ("Feedback");
		feedbackCanvas.SetActive (true);
		allCanvas.Push (feedbackCanvas);
	}

	/// <summary>
	/// Go to GameOpnening Scene
	/// </summary>
	public void quitGame() {
		gameObject.AddComponent<SceneChanger>();
		SceneChanger sceneChanger = GetComponent<SceneChanger>();
		sceneChanger.destinyScene = "GameOpening";
		sceneChanger.Change();
		pauseCanvas.SetActive (false);
	}

	/// <summary>
	/// Closes the feedback menu
	/// </summary>
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
