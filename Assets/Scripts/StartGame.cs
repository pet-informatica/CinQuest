using UnityEngine;
using System.Collections.Generic;
using UnityEngine;
//using UnityEditor;
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
		/*print ("passei aqui");
		GameObject controlCanvas = GameObject.Find("InitialMenu Canvas");
		print (controlCanvas.name);
		controlCanvas.SetActive (true);
		Dictionary<int, Quest> quests = GameManager.instance.questManager.getQuests ();
		Quest x = null;
		quests.TryGetValue (1, out x);
		print (x.name);*/
		sceneChanger.Change();
	}

	string GetInputButtonName(string name) {
		/*var inputManager = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];
		SerializedObject obj = new SerializedObject(inputManager);
		SerializedProperty axisArray = obj.FindProperty("m_Axes");

		string positiveButton = "";

		for (int i = 0; i < axisArray.arraySize; i++) {
			if (axisArray.GetArrayElementAtIndex (i).displayName == name) {
				positiveButton = axisArray.GetArrayElementAtIndex (i).FindPropertyRelative ("positiveButton").stringValue;
				return positiveButton.ToUpper();
			}
		}

		return positiveButton;*/
		return "boramim";
	}

	public void quitGame() {
		Application.Quit ();
	}
}
