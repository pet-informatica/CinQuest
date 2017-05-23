using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

/// <summary>
/// Have methods for either starting a new game, or loading an previous game. Should be used in the menu scene.
/// </summary>
public class StartGame : MonoBehaviour {

    SceneChanger sceneChanger;
	public GameObject loadGameButton;
	
	void Awake() {
        gameObject.AddComponent<SceneChanger>();
        sceneChanger = GetComponent<SceneChanger>();

		loadGameButton.GetComponent<Button> ().interactable = GameManager.Instance.CanLoadGame ();
    }

	/// <summary>
	/// Erase old player data and go for ChooseCharacter scene
	/// </summary>
	public void startGame() {
		GameManager.Instance.DeleteSavedData ();
		sceneChanger.destinyScene = "ChooseCharacter";
		sceneChanger.Change();
	}

	/// <summary>
	/// Loads old player data and go for CinParking scene
	/// </summary>
	public void LoadGame() {
		GameManager.Instance.LoadGame ();
		sceneChanger.destinyScene = "CinParking";
		sceneChanger.Change();
	}

	/// <summary>
	/// Closes the game
	/// </summary>
	public void quitGame() {
		Application.Quit ();
	}
}
