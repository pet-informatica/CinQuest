using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class StartGame : MonoBehaviour {

    SceneChanger sceneChanger;
	public GameObject loadGameButton;
	
	void Awake() {
        gameObject.AddComponent<SceneChanger>();
        sceneChanger = GetComponent<SceneChanger>();

		loadGameButton.GetComponent<Button> ().interactable = GameManager.Instance.CanLoadGame ();
    }

	public void startGame() {
		GameManager.Instance.DeleteSavedData ();
		sceneChanger.destinyScene = "ChooseCharacter";
		sceneChanger.Change();
	}

	public void LoadGame() {
		GameManager.Instance.LoadGame ();
		sceneChanger.destinyScene = "CinParking";
		sceneChanger.Change();
	}

	public void quitGame() {
		Application.Quit ();
	}
}
