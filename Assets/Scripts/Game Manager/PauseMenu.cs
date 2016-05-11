using UnityEngine;
using UnityEditor;
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

	public GameObject pauseCanvasPrefab;
	public GameObject controlCanvasPrefab;
	public GameObject eventSystemPrefab;

	private GameObject pauseCanvas;
	private GameObject controlCanvas;
	private GameObject eventSystem;

	void Start () {

	}

	void OnLevelWasLoaded(int level) {
		pauseCanvas = Instantiate (pauseCanvasPrefab);
		controlCanvas = Instantiate (controlCanvasPrefab);
		eventSystem = Instantiate (eventSystemPrefab);

		pauseCanvas.SetActive (false);
		controlCanvas.SetActive (false);
	}

	void Update () {
		if (Input.GetButtonDown("Pause")) {
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
		GameObject.FindGameObjectWithTag ("GameManager").GetComponent<PauseMenu> ().openControl();
	}

	private void openControl() {
		controlCanvas.SetActive (true);
		GameObject.Find ("upButton").GetComponent<Text> ().text = GetInputButtonName("Up");
		GameObject.Find ("downButton").GetComponent<Text> ().text = GetInputButtonName("Down");
		GameObject.Find ("leftButton").GetComponent<Text> ().text = GetInputButtonName("Left");
		GameObject.Find ("rightButton").GetComponent<Text> ().text = GetInputButtonName("Right");
		GameObject.Find ("runButton").GetComponent<Text> ().text = GetInputButtonName("Run");
		GameObject.Find ("inventoryButton").GetComponent<Text> ().text = GetInputButtonName("Inventory");
		GameObject.Find ("pauseButton").GetComponent<Text> ().text = GetInputButtonName("Pause");
	}

	public void quitGame() {
		Application.Quit ();
	}

	string GetInputButtonName(string name) {
		var inputManager = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];
		SerializedObject obj = new SerializedObject(inputManager);
		SerializedProperty axisArray = obj.FindProperty("m_Axes");

		string positiveButton = "";

		for (int i = 0; i < axisArray.arraySize; i++) {
			if (axisArray.GetArrayElementAtIndex (i).displayName == name) {
				positiveButton = axisArray.GetArrayElementAtIndex (i).FindPropertyRelative ("positiveButton").stringValue;
				return positiveButton.ToUpper();
			}
		}

		return positiveButton;
	}
}
