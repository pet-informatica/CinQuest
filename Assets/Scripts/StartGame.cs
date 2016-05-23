using UnityEngine;
using System.Collections.Generic;
using UnityEngine;
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
		Dictionary<int, Quest> quests = GameManager.instance.questManager.getQuests ();
		Quest x = null;
		quests.TryGetValue (1, out x);

		GameObject.Find ("Start").transform.GetChild(0).GetComponent<Text> ().text = x.name;
//		sceneChanger.Change();
	}

	public void quitGame() {
		Application.Quit ();
	}
}
