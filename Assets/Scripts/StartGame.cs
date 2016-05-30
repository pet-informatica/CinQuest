using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

public class StartGame : MonoBehaviour {

    public string startScene = "CinParking";
    SceneChanger sceneChanger;
	
	void Awake() {
        gameObject.AddComponent<SceneChanger>();
        sceneChanger = GetComponent<SceneChanger>();
        sceneChanger.destinyScene = "CinParking";
    }

	public void startGame() {
		sceneChanger.Change();
	}

	public void quitGame() {
		Application.Quit ();
	}
}
