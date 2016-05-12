using UnityEngine;
using System.Collections;

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
}
